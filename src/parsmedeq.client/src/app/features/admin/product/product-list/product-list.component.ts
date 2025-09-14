import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {BaseComponent} from '../../../../base-component';
import {MatTableDataSource} from '@angular/material/table';
import {Product, ProductResponse, ProductsRequest} from '../../../../core/models/ProductResponse';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort, Sort} from '@angular/material/sort';
import {Helpers} from '../../../../core/helpers';
import {TranslateService} from '@ngx-translate/core';

@Component({
  selector: 'app-product-list',
  styleUrls: ['product-list.component.scss'],
  templateUrl: 'product-list.component.html',
  standalone: false
})
export class ProductListComponent extends BaseComponent implements OnInit, OnDestroy {
  displayedColumns: string[] = [/*'row', */'title', 'productCategoryTitle', 'downloadCount', 'image', 'creationDate', 'actions'];
  languages: string[] = [];
  colors: string[] = ['warn', 'primary', 'success', 'secondary', 'info', 'danger'];
  dataSource!: MatTableDataSource<Product>;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator | null = null;
  @ViewChild(MatSort, {static: true}) sort: MatSort | null = null;
  pageIndex = 1;
  pageSize = 5;
  totalCount = 0;

  constructor(private helpers: Helpers,
              private translateService: TranslateService) {
    super();
    this.languages = this.translateService.getLangs();
  }

  ngOnInit() {
    this.helpers.setPaginationLang();
    this.getItems();
  }

  getItems() {
    let model: ProductsRequest = {
      pageIndex: this.pageIndex,
      pageSize: this.pageSize,
      sort: 0,
    };
    this.restApiService.getProducts(model).subscribe((res: ProductResponse) => {
      this.initDataSource(res);
    });
  }

  initDataSource(res: ProductResponse) {
    this.totalCount = res.data.totalCount;
    this.pageSize = res.data.pageSize;
    this.dataSource = new MatTableDataSource<Product>(res.data.items);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
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

  getColName(column: string) {
    column = column.toLowerCase();
    if (column === 'title') {
      column = 'عنوان';
    }
    if (column === 'productcategorytitle') {
      column = 'دسته بندی';
    }
    if (column === 'downloadcount') {
      column = 'تعداد دانلود';
    }
    if (column === 'expirationdate') {
      column = 'تاریخ انقضا';
    }
    if (column === 'image') {
      column = 'تصویر';
    }
    if (column === 'creationdate') {
      column = 'تاریخ ایجاد';
    }
    if (column === 'actions') {
      column = 'عملیات';
    }
    return column.toUpperCase();
  }

  ngOnDestroy() {
  }
}
