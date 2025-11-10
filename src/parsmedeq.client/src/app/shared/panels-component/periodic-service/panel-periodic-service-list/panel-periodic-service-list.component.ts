import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort, Sort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import {BaseComponent} from '../../../../base-component';
import {MatSelectChange} from '@angular/material/select';
import {ToastrService} from 'ngx-toastr';
import {Helpers} from '../../../../core/helpers';
import {AuthService} from '../../../../core/services/auth.service';
import {PeriodicService, PeriodicServiceResponse} from '../../../../core/models/ProductResponse';
import {PagingRequest} from '../../../../core/models/Pagination';

@Component({
  selector: 'app-panel-periodic-service-list',
  styleUrls: ['panel-periodic-service-list.component.scss'],
  templateUrl: 'panel-periodic-service-list.component.html',
  standalone: false
})
export class PanelPeriodicServiceListComponent extends BaseComponent implements OnInit {
  @Input() url: string = '';
  displayedColumns: string[] = [/*'row', */'code', 'title', 'profile', 'serviceDate', 'status', 'actions'];
  dataSource?: MatTableDataSource<PeriodicService>;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator | null = null;
  @ViewChild(MatSort, {static: true}) sort: MatSort | null = null;
  pageIndex = 1;
  pageSize = 20;
  totalCount = 0;
  fromDate?: string;
  toDate?: string;
  query: string | null = null;

  constructor(protected auth: AuthService,
              private toaster: ToastrService,
              private helpers: Helpers) {
    super();
  }

  ngOnInit() {
    this.helpers.setPaginationLang();
    this.getItems();
  }

  getItems() {
    const model: PagingRequest = {
      pageIndex: this.pageIndex,
      pageSize: this.pageSize,
      sort: 0,
      fromDate: this.fromDate,
      toDate: this.toDate,
      //query: this.query,
    };
    this.restApiService.getPeriodicServices(model, this.url).subscribe((res: PeriodicServiceResponse) => {
      this.initDataSource(res);
    });
  }

  public initDataSource(res: PeriodicServiceResponse) {
    this.totalCount = res.data.totalCount;
    this.pageSize = res.data.pageSize;
    this.dataSource = new MatTableDataSource<PeriodicService>(res.data.items);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
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
    this.getItems();
  }

  getColName(column: string) {
    column = column.toLowerCase();
    if (column === 'code') {
      column = 'TICKET_NUMBER';
    }
    if (column === 'userscount') {
      column = 'USERS_COUNT';
    }
    if (column === 'downloadcount') {
      column = 'DOWNLOAD_COUNT';
    }
    if (column === 'expirationdate') {
      column = 'EXPIRATION_DATE';
    }
    if (column === 'creationdate') {
      column = 'PUBLISHED';
    }
    if (column === 'servicedate') {
      column = 'SERVICE_DATE';
    }
    return column.toUpperCase();
  }

  fromDateChanged(date: string) {
    this.fromDate = date;
    this.getItems();
  }

  toDateChanged(date: string) {
    this.toDate = date;
    this.getItems();
  }

  public applyFilter($event: EventTarget | null) {
    const filterValue = ($event as HTMLInputElement).value;
    this.resetPaging();
    this.query = filterValue;
    this.getItems();
  }

  resetPaging() {
    if (this.dataSource?.paginator) {
      this.dataSource.paginator.firstPage();
    }
    this.pageIndex = 1;
    this.pageSize = 20;
  }

  changeStatus($event: MatSelectChange, element: PeriodicService) {
    /*this.ticketService.updateTicketStatus(element.id, $event.value).subscribe((t: BaseResult<boolean>) => {
      this.toaster.success(this.getTranslateValue('THE_OPERATION_WAS_SUCCESSFUL'), '', {});
    });*/
  }

  done(item: PeriodicService) {
    item.done = true;
  }

  createService(item: PeriodicService) {

  }
}
