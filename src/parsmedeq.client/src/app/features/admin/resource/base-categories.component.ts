import {AfterViewInit, Directive, inject, OnInit, ViewChild} from '@angular/core';
import {BaseComponent} from '../../../base-component';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {tap} from 'rxjs';
import {ResourceCategoriesResponse, ResourceCategory} from '../../../core/models/ResourceCategoryResponse';
import {TranslateService} from '@ngx-translate/core';
import {Helpers} from '../../../core/helpers';
import {MatTableDataSource} from '@angular/material/table';
import {DialogService} from '../../../core/services/dialog-service';
import {ToastrService} from 'ngx-toastr';
import {AddResult, BaseResult} from '../../../core/models/BaseResult';

@Directive()
export class BaseCategoriesComponent extends BaseComponent implements OnInit, AfterViewInit {
  helpers = inject(Helpers);
  toastr = inject(ToastrService);
  languages: string[] = [];
  colors: string[] = ['warn', 'primary', 'success', 'secondary', 'info', 'danger'];
  tableId: number;
  ///////////
  dataSource!: MatTableDataSource<ResourceCategory>;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator | null = null;
  @ViewChild(MatSort, {static: true}) sort: MatSort | null = null;
  totalCount = 0;

  constructor(tableId: number) {
    super();
    const translateService = inject(TranslateService);
    this.languages = translateService.getLangs();
    this.tableId = tableId;
  }

  ngOnInit(): void {
    this.helpers.setPaginationLang();
    this.getServerData();
  }

  getServerData() {
    this.restApiService.getResourceCategories(this.tableId).subscribe((res: ResourceCategoriesResponse) => {
      if (res?.data) {
        this.initDataSource(res);
        /*if (this.tableId === Tables.Article || this.tableId === Tables.News) {
          this.dataSource.forEach(s => {
            if (s.parentId) {
              s.parentTitle = this.dataSource.find(d => d.id === s.parentId)?.title ?? '';
            }
          });
        }*/
      }
    });
  }

  initDataSource(res: ResourceCategoriesResponse) {
    this.totalCount = res.data.length;
    this.dataSource = new MatTableDataSource<ResourceCategory>(res.data);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngAfterViewInit() {
    this.paginator?.page
      .pipe(
        tap(() => this.getServerData())
      )
      .subscribe();
  }

  remove(item: any) {
    const index: number = this.dataSource.data.indexOf(item);
    if (index !== -1) {
      let dialogRef = this.dialogService.openConfirmDialog('', this.getTranslateValue('SURE_DELETE'));
      dialogRef.afterClosed().subscribe(dialogResult => {
        if (dialogResult) {
          this.restApiService.deleteResourceCategory(item.id).subscribe({
            next: (response: BaseResult<AddResult>) => {
              if (response.data.changed) {
                this.dataSource.data.splice(index, 1);
                this.dataSource._updateChangeSubscription();
                this.toastr.success(this.getTranslateValue('ITEM_DELETED_SUCCESSFULLY'), '', {});
              } else {
                this.toastr.error(this.getTranslateValue('THIS_ITEM_CANNOT_BE_DELETED_PLEASE_DELETE_ITS_SUBCATEGORIES_FIRST'), '', {});
              }
            }
          });
        }
      });
    }
  }
}
