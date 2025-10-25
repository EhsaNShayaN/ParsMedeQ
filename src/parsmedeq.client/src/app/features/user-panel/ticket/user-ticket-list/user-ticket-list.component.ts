import {Component, OnInit} from '@angular/core';
import {BaseComponent} from '../../../../base-component';

@Component({
  selector: 'app-user-user-ticket-list',
  templateUrl: './user-ticket-list.component.html',
  styleUrls: ['./user-ticket-list.component.scss'],
  standalone: false
})
export class UserTicketListComponent extends BaseComponent implements OnInit {
  ngOnInit(): void {
  }
}
