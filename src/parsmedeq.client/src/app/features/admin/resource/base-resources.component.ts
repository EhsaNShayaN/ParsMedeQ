import {Directive, inject, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort, Sort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import {BaseReportComponent} from '../../../base-report-component';
import {Resource, ResourceResponse, ResourcesRequest} from '../../../core/models/ResourceResponse';
import {TranslateService} from '@ngx-translate/core';

@Directive()
export class BaseResourcesComponent extends BaseReportComponent implements OnInit, OnDestroy {
  languages: string[] = [];
  colors: string[] = ['warn', 'primary', 'success', 'secondary', 'info', 'danger'];
  tableId: number;
  dataSource!: MatTableDataSource<Resource>;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator | null = null;
  @ViewChild(MatSort, {static: true}) sort: MatSort | null = null;
  pageIndex = 1;
  pageSize = 5;
  totalCount = 0;

  ///////////

  constructor(tableId: number) {
    super();
    const translateService = inject(TranslateService);
    this.languages = translateService.getLangs();
    this.tableId = tableId;
  }

  ngOnInit() {
    this.setPaginationLang();
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

  remove(property: Resource) {
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
