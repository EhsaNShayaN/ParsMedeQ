import {Directive, inject, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort, Sort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import {Resource, ResourceResponse, ResourcesRequest} from '../../../core/models/ResourceResponse';
import {TranslateService} from '@ngx-translate/core';
import {Helpers} from '../../../core/helpers';
import {DialogService} from '../../../core/services/dialog-service';
import {ToastrService} from 'ngx-toastr';
import {BaseComponent} from '../../../base-component';

@Directive()
export class BaseResourcesComponent extends BaseComponent implements OnInit, OnDestroy {
  languages: string[] = [];
  colors: string[] = ['warn', 'primary', 'success', 'secondary', 'info', 'danger'];
  tableId: number;
  dataSource!: MatTableDataSource<Resource>;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator | null = null;
  @ViewChild(MatSort, {static: true}) sort: MatSort | null = null;
  pageIndex = 1;
  pageSize = 5;
  totalCount = 0;
  helpers = inject(Helpers);
  dialogService = inject(DialogService);
  toaster = inject(ToastrService);

  constructor(tableId: number) {
    super();
    const translateService = inject(TranslateService);
    this.languages = translateService.getLangs();
    this.tableId = tableId;
  }

  ngOnInit() {
    this.helpers.setPaginationLang();
    this.getItems();
  }

  getItems() {
    let model: ResourcesRequest = {
      pageIndex: this.pageIndex,
      pageSize: this.pageSize,
      sort: 0,
      tableId: this.tableId
    };
    this.restApiService.getResources(model).subscribe((res: ResourceResponse) => {
      this.initDataSource(res);
    });
  }

  initDataSource(res: ResourceResponse) {
    this.totalCount = res.data.totalCount;
    this.pageSize = res.data.pageSize;
    this.dataSource = new MatTableDataSource<Resource>(res.data.items);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  remove(item: any) {
    const index: number = this.dataSource.data.indexOf(item);
    if (index !== -1) {
      let dialogRef = this.dialogService.openConfirmDialog('', this.getTranslateValue('SURE_DELETE'));
      dialogRef.afterClosed().subscribe(dialogResult => {
        if (dialogResult) {
          this.restApiService.deleteResource(item.id).subscribe({
            next: (response) => {
              this.dataSource.data.splice(index, 1);
              this.dataSource._updateChangeSubscription();
              this.toaster.success(this.getTranslateValue('ITEM_DELETED_SUCCESSFULLY'), '', {});
            }
          });
        }
      });
    }
  }

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
    this.getItems();
  }

  ngOnDestroy() {
  }
}
