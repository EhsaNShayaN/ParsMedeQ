import {Directive, Injector, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {BaseComponent} from '../../../base-component';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort, Sort} from '@angular/material/sort';
import {Resource, ResourceResponse, ResourcesRequest} from '../../../../lib/models/ResourceResponse';
import {MatTableDataSource} from '@angular/material/table';

@Directive()
export class BaseResourcesComponent extends BaseComponent implements OnInit, OnDestroy {
  languages: string[] = [];
  colors: string[] = ['warn', 'primary', 'success', 'secondary', 'info', 'danger'];
  tableId: number;
  ///////////
  pinned: boolean = false;

  ///////////

  dataSource: MatTableDataSource<Resource>;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  pageIndex = 1;
  pageSize = 5;
  totalCount = 0;
  adminSort: Sort = {active: 'creationDate', direction: 'desc'};

  ///////////

  constructor(injector: Injector,
              tableId: number) {
    super(injector);
    this.languages = this.languageService.getLanguages();
    this.tableId = tableId;
  }

  ngOnInit() {
    this.appService.setPaginationLang();
    this.getItems();
  }

  getItems() {
    let model: ResourcesRequest = {
      page: this.pageIndex,
      pageSize: this.pageSize,
      sort: 0,
      adminSort: this.adminSort,
      tableId: this.tableId,
      pinned: this.pinned
    };
    /*if (this.req) {
      model = {...model, ...this.req};
    }*/
    this.restClientService.getResources(model).subscribe((res: ResourceResponse) => {
      this.initDataSource(res);
    });
  }

  initDataSource(res: ResourceResponse) {
    this.totalCount = res.totalCount;
    this.pageSize = res.pageSize;
    this.dataSource = new MatTableDataSource<Resource>(res.data);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  remove(property: Resource) {
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

  ngOnDestroy() {
  }
}
