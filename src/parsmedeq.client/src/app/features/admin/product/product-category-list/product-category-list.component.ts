import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {BaseComponent} from '../../../../base-component';
import {ProductCategoriesResponse, ProductCategory} from '../../../../core/models/ProductCategoryResponse';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {tap} from 'rxjs';
import {Helpers} from '../../../../core/helpers';
import {AddResult, BaseResult} from '../../../../core/models/BaseResult';
import {ToastrService} from 'ngx-toastr';
import {MatTableDataSource} from '@angular/material/table';

@Component({
  selector: 'app-product-category-list',
  styleUrls: ['product-category-list.component.scss'],
  templateUrl: 'product-category-list.component.html',
  standalone: false
})
export class ProductCategoryListComponent extends BaseComponent implements OnInit, AfterViewInit {
  columnsToDisplay: string[] = [/*'row', */'title', 'parentId', 'image', 'creationDate', 'actions'];
  languages: string[] = [];
  colors: string[] = ['warn', 'primary', 'success', 'secondary', 'info', 'danger'];
  ///////////
  dataSource!: MatTableDataSource<ProductCategory>;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator | null = null;
  @ViewChild(MatSort, {static: true}) sort: MatSort | null = null;
  totalCount = 0;

  constructor(private helpers: Helpers,
              private toastrService: ToastrService,) {
    super();
    this.languages = this.translateService.getLangs();
  }

  ngOnInit(): void {
    this.helpers.setPaginationLang();
    this.getServerData();
  }

  getServerData() {
    this.restApiService.getProductCategories().subscribe((res: ProductCategoriesResponse) => {
      if (res?.data) {
        this.initDataSource(res);
        /*this.dataSource.forEach(s => {
          if (s.parentId) {
            s.parentTitle = this.dataSource.find(d => d.id === s.parentId)?.title ?? '';
          }
        });*/
      }
    });
  }

  initDataSource(res: ProductCategoriesResponse) {
    this.totalCount = res.data.length;
    this.dataSource = new MatTableDataSource<ProductCategory>(res.data);
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
          this.restApiService.deleteProductCategory(item.id).subscribe({
            next: (response: BaseResult<AddResult>) => {
              if (response.data.changed) {
                this.dataSource.data.splice(index, 1);
                this.dataSource._updateChangeSubscription();
                this.toastrService.success(this.getTranslateValue('ITEM_DELETED_SUCCESSFULLY'), '', {});
              } else {
                this.toastrService.error(this.getTranslateValue('THIS_ITEM_CANNOT_BE_DELETED_PLEASE_DELETE_ITS_SUBCATEGORIES_FIRST'), '', {});
              }
            }
          });
        }
      });
    }
  }

  getColName(column: string) {
    column = column.toUpperCase();
    if (column === 'PARENTID') {
      column = 'CATEGORY';
    }
    if (column === 'CREATIONDATE') {
      column = 'CREATION_DATE';
    }
    return this.getTranslateValue(column);
  }
}
