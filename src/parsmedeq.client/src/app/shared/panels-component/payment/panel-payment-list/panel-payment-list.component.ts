import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort, Sort} from '@angular/material/sort';
import {BaseComponent} from '../../../../base-component';
import {MatTableDataSource} from '@angular/material/table';
import {ToastrService} from 'ngx-toastr';
import {Helpers} from '../../../../core/helpers';
import {Payment, PaymentResponse, PaymentsRequest} from '../../../../core/models/PaymentResponse';
import {PaymentService} from '../../../../core/services/rest/payment-service';

@Component({
  selector: 'app-panel-payment-list',
  styleUrls: ['panel-payment-list.component.scss'],
  templateUrl: 'panel-payment-list.component.html',
  standalone: false
})
export class PanelPaymentListComponent extends BaseComponent implements OnInit {
  @Input() url: string = '';
  dataSource!: MatTableDataSource<Payment>;
  ///////////////////////
  displayedColumns: string[] = [/*'row', */'fullName', 'paidDate', 'statusText', 'actions'];
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator | null = null;
  @ViewChild(MatSort, {static: true}) sort: MatSort | null = null;
  pageIndex = 1;
  pageSize = 10;
  totalCount = 0;
  adminSort: Sort = {active: 'creationDate', direction: 'desc'};

  constructor(private paymentService: PaymentService,
              protected helpers: Helpers,
              private toastrService: ToastrService) {
    super();
  }

  ngOnInit() {
    this.helpers.setPaginationLang();
    this.getItems();
  }

  getItems() {
    const model: PaymentsRequest = {
      pageIndex: this.pageIndex,
      pageSize: this.pageSize,
      sort: 0,
    };
    this.paymentService.getPayments(model, this.url).subscribe((res: PaymentResponse) => {
      this.initDataSource(res);
    });
  }

  public initDataSource(res: any) {
    this.totalCount = res.data.totalCount;
    this.dataSource = new MatTableDataSource<Payment>(res.data.items);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  /*remove(item: any) {
    const index: number = this.dataSource.data.indexOf(item);
    if (index !== -1) {
      let dialogRef = this.dialogService.openConfirmDialog('', this.getTranslateValue('SURE_DELETE'));
      dialogRef.afterClosed().subscribe(dialogResult => {
        if (dialogResult) {
          this.paymentService.deletePayment(item.id).subscribe({
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
    if (column === 'fullname') {
      column = 'full_name';
    }
    if (column === 'downloadcount') {
      column = 'download_count';
    }
    if (column === 'expirationdate') {
      column = 'expiration_date';
    }
    if (column === 'paiddate') {
      column = 'payment_time';
    }
    if (column === 'statustext') {
      column = 'status';
    }
    return column.toUpperCase();
  }

  /*setStatus(payment: Payment, isConfirmed: boolean) {
    this.paymentService.confirmPayment(payment.id, isConfirmed).subscribe((res: BaseResult<AddResult>) => {
      payment.isConfirmed = isConfirmed;
    });
  }*/

  /*setReply(element: Payment) {
    const dialogRef = this.dialogService.openConfirmDialog(this.getTranslateValue('REPLY'), '');
    dialogRef.afterClosed().subscribe(s => {
      if (s) {
        this.paymentService.addPaymentAnswer(element.id, s.answer).subscribe((res: BaseResult<boolean>) => {
          this.toastrService.success(this.getTranslateValue('THE_OPERATION_WAS_SUCCESSFUL'), '', {});
        });
      }
    });
  }*/
}
