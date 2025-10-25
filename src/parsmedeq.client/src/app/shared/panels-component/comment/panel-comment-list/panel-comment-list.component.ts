import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort, Sort} from '@angular/material/sort';
import {BaseComponent} from '../../../../base-component';
import {MatTableDataSource} from '@angular/material/table';
import {ToastrService} from 'ngx-toastr';
import {Helpers} from '../../../../core/helpers';
import {Comment, CommentResponse, CommentsRequest} from '../../../../core/models/CommentResponse';
import {AddResult, BaseResult} from '../../../../core/models/BaseResult';
import {CommentService} from '../../../../core/services/rest/comment-service';

@Component({
  selector: 'app-panel-comment-list',
  styleUrls: ['panel-comment-list.component.scss'],
  templateUrl: 'panel-comment-list.component.html',
  standalone: false
})
export class PanelCommentListComponent extends BaseComponent implements OnInit {
  @Input() url: string = '';
  dataSource!: MatTableDataSource<Comment>;
  ///////////////////////
  displayedColumns: string[] = [/*'row', */'title', 'description', 'creationDate', 'status', 'actions'];
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator | null = null;
  @ViewChild(MatSort, {static: true}) sort: MatSort | null = null;
  pageIndex = 1;
  pageSize = 10;
  totalCount = 0;
  adminSort: Sort = {active: 'creationDate', direction: 'desc'};

  constructor(private commentService: CommentService,
              private helpers: Helpers,
              private toastrService: ToastrService) {
    super();
  }

  ngOnInit() {
    this.helpers.setPaginationLang();
    this.getItems();
  }

  getItems() {
    const model: CommentsRequest = {
      pageIndex: this.pageIndex,
      pageSize: this.pageSize,
      sort: 0,
    };
    this.commentService.getComments(model, this.url).subscribe((res: CommentResponse) => {
      this.initDataSource(res);
    });
  }

  public initDataSource(res: any) {
    this.totalCount = res.totalCount;
    this.dataSource = new MatTableDataSource<Comment>(res.data);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  remove(item: any) {
    const index: number = this.dataSource.data.indexOf(item);
    if (index !== -1) {
      let dialogRef = this.dialogService.openConfirmDialog('', this.getTranslateValue('SURE_DELETE'));
      dialogRef.afterClosed().subscribe(dialogResult => {
        if (dialogResult) {
          this.commentService.deleteComment(item.id).subscribe({
            next: (response: BaseResult<AddResult>) => {
              if (response.data.changed) {
                this.dataSource.data.splice(index, 1);
                this.dataSource._updateChangeSubscription();
                this.toastrService.success(this.getTranslateValue('ITEM_DELETED_SUCCESSFULLY'), '', {});
              } else {
                this.toastrService.error(this.getTranslateValue('UNKNOWN_ERROR'), '', {});
              }
            }
          });
        }
      });
    }
  }

  /*public applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }*/

  onPaginateChange(event: any) {
    if (event.previousPageIndex !== event.pageIndex) {
      this.pageIndex = event.pageIndex + 1;
    }
    if (this.pageSize !== event.pageSize) {
      this.pageSize = event.pageSize;
      this.pageIndex = 1;
    }
    this.getItems();
  }

  sortChanged($event: Sort) {
    console.log($event);
    this.adminSort = $event;
    this.getItems();
  }

  getColName(column: string) {
    column = column.toLowerCase();
    if (column === 'title') {
      column = 'FULL_NAME';
    }
    if (column === 'description') {
      column = 'COMMENT_TEXT';
    }
    if (column === 'downloadcount') {
      column = 'download_count';
    }
    if (column === 'expirationdate') {
      column = 'expiration_date';
    }
    if (column === 'creationdate') {
      column = 'published';
    }
    return column.toUpperCase();
  }

  setStatus(comment: Comment, isConfirmed: boolean) {
    this.commentService.confirmComment(comment.id, isConfirmed).subscribe((res: BaseResult<AddResult>) => {
      comment.isConfirmed = isConfirmed;
    });
  }

  setReply(element: Comment) {
    const dialogRef = this.dialogService.openConfirmDialog(this.getTranslateValue('REPLY'), '');
    dialogRef.afterClosed().subscribe(s => {
      if (s) {
        this.commentService.addCommentAnswer(element.id, s.answer).subscribe((res: BaseResult<boolean>) => {
          this.toastrService.success(this.getTranslateValue('THE_OPERATION_WAS_SUCCESSFUL'), '', {});
        });
      }
    });
  }
}
