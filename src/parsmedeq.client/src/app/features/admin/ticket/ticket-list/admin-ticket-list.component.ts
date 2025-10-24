import {Component, OnInit} from '@angular/core';
import {BaseComponent} from '../../../../base-component';

@Component({
  selector: 'app-admin-ticket-list',
  templateUrl: './admin-ticket-list.component.html',
  styleUrls: ['./admin-ticket-list.component.scss'],
  standalone: false
})
export class AdminTicketListComponent extends BaseComponent implements OnInit {
  ngOnInit(): void {
  }
}
