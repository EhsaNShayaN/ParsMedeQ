import {Component, OnInit, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort, Sort} from '@angular/material/sort';
import {BaseComponent} from '../../../../base-component';
import {Comment, CommentResponse} from '../../../../../lib/models/CommentResponse';
import {MatTableDataSource} from '@angular/material/table';
import {BaseResult} from '../../../../../lib/models/BaseResult';
import {RestAdminService} from '../../../../../lib/core/services/client/rest-admin.service';
import {ToastrService} from 'ngx-toastr';
import {DialogService} from "../../../../../lib/core/services/dialog-service";

@Component({
  selector: 'app-comment-list',
  styleUrls: ['comment-list.component.scss'],
  templateUrl: 'comment-list.component.html'
})
export class CommentListComponent extends BaseComponent implements OnInit {
  dataSource: MatTableDataSource<Comment>;
  ///////////////////////
  displayedColumns: string[] = [/*'row', */'title', 'description', 'creationDate', 'status', 'actions'];
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  pageIndex = 1;
  pageSize = 10;
  totalCount = 0;
  adminSort: Sort = {active: 'creationDate', direction: 'desc'};

  constructor(private dialogService: DialogService,
              private restAdminService: RestAdminService,
              private toaster: ToastrService) {
    super();
  }

  ngOnInit() {
    this.appService.setPaginationLang();
    this.getItems();
  }

  getItems() {
    const model = {
      page: this.pageIndex,
      pageSize: this.pageSize,
      sort: 0,
      adminSort: this.adminSort,
    };
    this.restClientService.getComments(model).subscribe((res: CommentResponse) => {
      this.initDataSource(res);
    });
  }

  public initDataSource(res: any) {
    this.totalCount = res.totalCount;
    this.dataSource = new MatTableDataSource<Comment>(res.data);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  public remove(property: Comment) {
    console.log('delete');
    /*const index: number = this.dataSource.data.indexOf(property);
    if (index !== -1) {
      const message = this.appService.getTranslateValue('MESSAGE.SURE_DELETE') ?? '';
      let dialogRef = this.dialogService.openConfirmDialog('', message);
      dialogRef.afterClosed().subscribe(dialogResult => {
        if (dialogResult) {
          this.dataSource.data.splice(index, 1);
          this.initDataSource(this.dataSource.data);
        }
      });
    }*/
  }

  /*public applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }*/

  onPaginateChange(event) {
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
    this.restClientService.confirmComment(comment.id, isConfirmed).subscribe((res: BaseResult<boolean>) => {
      comment.isConfirmed = isConfirmed;
    });
  }

  setReply(element: Comment) {
    const dialogRef = this.dialogService.openConfirmDialog(this.appService.getTranslateValue('REPLY'), null, {id: element.id});
    dialogRef.afterClosed().subscribe(s => {
      if (s) {
        this.restAdminService.addCommentAnswer(element.id, s.answer).subscribe((res: BaseResult<boolean>) => {
          this.toaster.success(this.appService.getTranslateValue('THE_OPERATION_WAS_SUCCESSFUL'), '', {});
        });
      }
    });
  }
}
