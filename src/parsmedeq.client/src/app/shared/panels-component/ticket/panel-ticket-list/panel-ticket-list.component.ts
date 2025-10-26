import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort, Sort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import {BaseComponent} from '../../../../base-component';
import {Ticket, TicketResponse} from '../../../../core/models/TicketResponse';
import {MatSelectChange} from '@angular/material/select';
import {BaseResult} from '../../../../core/models/BaseResult';
import {ToastrService} from 'ngx-toastr';
import {Helpers} from '../../../../core/helpers';
import {TicketService} from '../../../../core/services/rest/ticket-service';
import {AuthService} from '../../../../core/services/auth.service';

@Component({
  selector: 'app-panel-ticket-list',
  styleUrls: ['panel-ticket-list.component.scss'],
  templateUrl: 'panel-ticket-list.component.html',
  standalone: false
})
export class PanelTicketListComponent extends BaseComponent implements OnInit {
  @Input() url: string = '';
  displayedColumns: string[] = [/*'row', */'code', 'title', 'profile', 'statusText', 'creationDate', 'actions'];
  dataSource?: MatTableDataSource<Ticket>;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator | null = null;
  @ViewChild(MatSort, {static: true}) sort: MatSort | null = null;
  pageIndex = 1;
  pageSize = 20;
  totalCount = 0;
  fromDate: string | null = null;
  toDate: string | null = null;
  query: string | null = null;
  statuses: any[] = [];

  constructor(private ticketService: TicketService,
              protected auth: AuthService,
              private toaster: ToastrService,
              private helpers: Helpers) {
    super();
  }

  ngOnInit() {
    this.helpers.setPaginationLang();
    this.statuses = this.helpers.getTicketStatuses();
    this.getItems();
  }

  getItems() {
    const model = {
      pageIndex: this.pageIndex,
      pageSize: this.pageSize,
      sort: 0,
      fromDate: this.fromDate,
      toDate: this.toDate,
      query: this.query,
    };
    this.ticketService.getTickets(model, this.url).subscribe((res: TicketResponse) => {
      /*if (res.data.length) {
        res.data.forEach(s => s.statusText = this.helpers.getTicketStatus(s.status));
      }*/
      this.initDataSource(res);
    });
  }

  public initDataSource(res: any) {
    this.totalCount = res.totalCount;
    this.pageSize = res.pageSize;
    this.dataSource = new MatTableDataSource<Ticket>(res.data);
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
    if (column === 'statustext') {
      column = 'STATUS';
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

  changeStatus($event: MatSelectChange, element: Ticket) {
    this.ticketService.updateTicketStatus(element.id, $event.value).subscribe((t: BaseResult<boolean>) => {
      this.toaster.success(this.getTranslateValue('THE_OPERATION_WAS_SUCCESSFUL'), '', {});
    });
  }
}
