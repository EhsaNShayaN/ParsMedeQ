import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort, Sort} from '@angular/material/sort';
import {BaseComponent} from '../../../../base-component';
import {MatTableDataSource} from '@angular/material/table';
import {ToastrService} from 'ngx-toastr';
import {Helpers} from '../../../../core/helpers';
import {Order, OrderResponse, OrdersRequest} from '../../../../core/models/OrderResponse';
import {OrderService} from '../../../../core/services/rest/order-service';

@Component({
  selector: 'app-panel-order-list',
  styleUrls: ['panel-order-list.component.scss'],
  templateUrl: 'panel-order-list.component.html',
  standalone: false
})
export class PanelOrderListComponent extends BaseComponent implements OnInit {
  @Input() url: string = '';
  dataSource!: MatTableDataSource<Order>;
  ///////////////////////
  displayedColumns: string[] = [/*'row', */'title', 'description', 'creationDate', 'status', 'actions'];
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator | null = null;
  @ViewChild(MatSort, {static: true}) sort: MatSort | null = null;
  pageIndex = 1;
  pageSize = 10;
  totalCount = 0;
  adminSort: Sort = {active: 'creationDate', direction: 'desc'};

  constructor(private orderService: OrderService,
              private helpers: Helpers,
              private toastrService: ToastrService) {
    super();
  }

  ngOnInit() {
    this.helpers.setPaginationLang();
    this.getItems();
  }

  getItems() {
    const model: OrdersRequest = {
      pageIndex: this.pageIndex,
      pageSize: this.pageSize,
      sort: 0,
    };
    this.orderService.getOrders(model, this.url).subscribe((res: OrderResponse) => {
      this.initDataSource(res);
    });
  }

  public initDataSource(res: any) {
    this.totalCount = res.data.totalCount;
    this.dataSource = new MatTableDataSource<Order>(res.data.items);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  /*remove(item: any) {
    const index: number = this.dataSource.data.indexOf(item);
    if (index !== -1) {
      let dialogRef = this.dialogService.openConfirmDialog('', this.getTranslateValue('SURE_DELETE'));
      dialogRef.afterClosed().subscribe(dialogResult => {
        if (dialogResult) {
          this.orderService.deleteOrder(item.id).subscribe({
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
  }*/

  /*public applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }*/

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

  getColName(column: string) {
    column = column.toLowerCase();
    if (column === 'title') {
      column = 'FULL_NAME';
    }
    if (column === 'downloadcount') {
      column = 'download_count';
    }
    if (column === 'expirationdate') {
      column = 'expiration_date';
    }
    if (column === 'creationdate') {
      column = 'published';
    }
    return column.toUpperCase();
  }

  /*setStatus(order: Order, isConfirmed: boolean) {
    this.orderService.confirmOrder(order.id, isConfirmed).subscribe((res: BaseResult<AddResult>) => {
      order.isConfirmed = isConfirmed;
    });
  }*/

  /*setReply(element: Order) {
    const dialogRef = this.dialogService.openConfirmDialog(this.getTranslateValue('REPLY'), '');
    dialogRef.afterClosed().subscribe(s => {
      if (s) {
        this.orderService.addOrderAnswer(element.id, s.answer).subscribe((res: BaseResult<boolean>) => {
          this.toastrService.success(this.getTranslateValue('THE_OPERATION_WAS_SUCCESSFUL'), '', {});
        });
      }
    });
  }*/
}
