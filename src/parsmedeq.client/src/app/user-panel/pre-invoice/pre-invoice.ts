import {Component} from '@angular/core';

@Component({
  selector: 'app-pre-invoice',
  standalone: false,
  templateUrl: './pre-invoice.html',
  styleUrl: './pre-invoice.scss'
})
export class PreInvoice {
  invoices = [
    {id: 501, amount: 2500000, status: 'منتظر پرداخت'},
    {id: 502, amount: 4300000, status: 'پرداخت شده'}
  ];
}
