import {AfterViewInit, Directive, OnInit, ViewChild} from '@angular/core';
import {BaseComponent} from '../../../base-component';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {tap} from 'rxjs';
import {ResourceCategoriesResponse, ResourceCategory} from '../../../core/models/ResourceCategoryResponse';
import {Tables} from '../../../core/constants/server.constants';
import {Resource} from '../../../core/models/ResourceResponse';

@Directive()
export class BaseCategoriesComponent extends BaseComponent implements OnInit, AfterViewInit {
  tableId: number;
  ///////////
  dataSource: ResourceCategory[] = [];
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator | undefined;
  @ViewChild(MatSort, {static: true}) sort: MatSort | undefined;
  totalCount = 0;

  constructor(tableId: number) {
    super();
    this.tableId = tableId;
  }

  ngOnInit(): void {
    this.getServerData();
  }

  getServerData() {
    this.restApiService.getResourceCategories(this.tableId).subscribe((resp: ResourceCategoriesResponse) => {
      if (resp?.data) {
        this.dataSource = resp.data;
        this.totalCount = resp.data.length;
        if (this.tableId === Tables.Article || this.tableId === Tables.News) {
          this.dataSource.forEach(s => {
            if (s.parentId) {
              s.parentTitle = this.dataSource.find(d => d.id === s.parentId)?.title ?? '';
            }
          });
        }
      }
      console.log('dataSource', this.dataSource);
    });
  }

  ngAfterViewInit() {
    this.paginator?.page
      .pipe(
        tap(() => this.getServerData())
      )
      .subscribe();
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
}
