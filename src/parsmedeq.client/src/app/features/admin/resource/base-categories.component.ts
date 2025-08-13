import {Directive, Injector, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {BaseComponent} from '../../../base-component';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {ResourceCategoriesResponse, ResourceCategory} from '../../../../lib/models/ResourceCategoryResponse';
import {tap} from 'rxjs';
import {Tables} from '../../../../lib/core/constants/server.constants';
import {Resource} from '../../../../lib/models/ResourceResponse';

@Directive()
export class BaseCategoriesComponent extends BaseComponent implements OnInit, OnDestroy {
  languages: string[] = [];
  colors: string[] = ['warn', 'primary', 'success', 'secondary', 'info', 'danger'];
  tableId: number;
  ///////////
  dataSource: ResourceCategory[];
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  totalCount = 0;

  constructor(injector: Injector,
              tableId: number) {
    super(injector);
    this.languages = this.languageService.getLanguages();
    this.tableId = tableId;
  }

  ngOnInit(): void {
    this.getServerData();
  }

  getServerData() {
    this.restClientService.getResourceCategories(this.tableId).subscribe((resp: ResourceCategoriesResponse) => {
      if (resp?.resourceCategories) {
        this.dataSource = resp.resourceCategories;
        this.totalCount = resp.resourceCategories.length;
        if (this.tableId === Tables.Article || this.tableId === Tables.Notice || this.tableId === Tables.Standard) {
          this.dataSource.forEach(s => {
            if (s.parentId) {
              s.parentId = this.dataSource.find(d => d.id === s.parentId).title;
            }
          });
        }
      }
    });
  }

  ngAfterViewInit() {
    this.paginator.page
      .pipe(
        tap(() => this.getServerData())
      )
      .subscribe();
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
}
