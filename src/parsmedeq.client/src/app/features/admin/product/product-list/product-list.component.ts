import {Component, OnInit, ViewChild} from '@angular/core';
import {BaseComponent} from '../../../../base-component';
import {MatTableDataSource} from '@angular/material/table';
import {Product, ProductResponse, ProductsRequest} from '../../../../core/models/ProductResponse';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort, Sort} from '@angular/material/sort';
import {Helpers} from '../../../../core/helpers';
import {animate, state, style, transition, trigger} from '@angular/animations';
import {ToastrService} from 'ngx-toastr';
import {AddResult, BaseResult} from '../../../../core/models/BaseResult';

@Component({
  selector: 'app-product-list',
  styleUrls: ['product-list.component.scss'],
  templateUrl: 'product-list.component.html',
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('750ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
  standalone: false
})
export class ProductListComponent extends BaseComponent implements OnInit {
  displayedColumns: string[] = [/*'row', */'title', 'productCategoryTitle', 'image', 'creationDate', 'actions'];
  columnsToDisplayWithExpand = [...this.displayedColumns, 'expand'];
  languages: string[] = [];
  colors: string[] = ['warn', 'primary', 'success', 'secondary', 'info', 'danger'];
  dataSource!: MatTableDataSource<Product>;
  expandedElement: any | null;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator | null = null;
  @ViewChild(MatSort, {static: true}) sort: MatSort | null = null;
  pageIndex = 1;
  pageSize = 5;
  totalCount = 0;

  constructor(private helpers: Helpers,
              private toastrService: ToastrService) {
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

  remove(item: any) {
    const index: number = this.dataSource.data.indexOf(item);
    if (index !== -1) {
      let dialogRef = this.dialogService.openConfirmDialog('', this.getTranslateValue('SURE_DELETE'));
      dialogRef.afterClosed().subscribe(dialogResult => {
        if (dialogResult) {
          this.restApiService.deleteProduct(item.id).subscribe({
            next: (response: BaseResult<AddResult>) => {
              if (response.data.changed) {
                this.dataSource.data.splice(index, 1);
                this.dataSource._updateChangeSubscription();
                this.toastrService.success(this.getTranslateValue('ITEM_DELETED_SUCCESSFULLY'), '', {});
              } else {
                this.toastrService.error(this.getTranslateValue('UNKNOWN_ERROR'), '', {});
              }
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

  getColName(column: string) {
    column = column.toLowerCase();
    if (column === 'productcategorytitle') {
      column = 'category';
    }
    if (column === 'creationdate') {
      column = 'CREATION_DATE';
    }
    return column.toUpperCase();
  }
}
