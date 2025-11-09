import {Component, OnInit} from '@angular/core';
import {BaseComponent} from '../../../../base-component';

@Component({
  selector: 'app-admin-admin-periodic-service-list',
  templateUrl: './admin-periodic-service-list.component.html',
  styleUrls: ['./admin-periodic-service-list.component.scss'],
  standalone: false
})
export class AdminPeriodicServiceListComponent extends BaseComponent implements OnInit {
  ngOnInit(): void {
  }
}
