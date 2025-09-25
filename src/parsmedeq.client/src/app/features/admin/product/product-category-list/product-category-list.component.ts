import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {BaseComponent} from '../../../../base-component';
import {ProductCategoriesResponse, ProductCategory} from '../../../../core/models/ProductCategoryResponse';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {tap} from 'rxjs';
import {Product} from '../../../../core/models/ProductResponse';

@Component({
  selector: 'app-product-category-list',
  styleUrls: ['product-category-list.component.scss'],
  templateUrl: 'product-category-list.component.html',
  standalone: false
})
export class ProductCategoryListComponent extends BaseComponent implements OnInit, AfterViewInit {
  columnsToDisplay: string[] = [/*'row', */'title', 'parentId', 'creationDate', 'actions'];
  languages: string[] = [];
  colors: string[] = ['warn', 'primary', 'success', 'secondary', 'info', 'danger'];
  ///////////
  dataSource: ProductCategory[] = [];
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator | undefined;
  @ViewChild(MatSort, {static: true}) sort: MatSort | undefined;
  totalCount = 0;

  constructor() {
    super();
    this.languages = this.translateService.getLangs();
  }

  ngOnInit(): void {
    this.getServerData();
  }

  getServerData() {
    this.restApiService.getProductCategories().subscribe((resp: ProductCategoriesResponse) => {
      if (resp?.data) {
        this.dataSource = resp.data;
        this.totalCount = resp.data.length;
        this.dataSource.forEach(s => {
          if (s.parentId) {
            s.parentTitle = this.dataSource.find(d => d.id === s.parentId)?.title ?? '';
          }
        });
      }
    });
  }

  ngAfterViewInit() {
    this.paginator?.page
      .pipe(
        tap(() => this.getServerData())
      )
      .subscribe();
  }

  remove(property: Product) {
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

  getColName(column: string) {
    column = column.toLowerCase();
    if (column === 'title') {
      column = 'عنوان';
    }
    if (column === 'parentid') {
      column = 'دسته بندی';
    }
    if (column === 'downloadcount') {
      column = 'تعداد دانلود';
    }
    if (column === 'expirationdate') {
      column = 'تاریخ انقضا';
    }
    if (column === 'creationdate') {
      column = 'تاریخ ایجاد';
    }
    if (column === 'actions') {
      column = 'عملیات';
    }
    return column.toUpperCase();
  }
}
