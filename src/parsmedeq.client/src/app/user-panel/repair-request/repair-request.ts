import {Component} from '@angular/core';

@Component({
  selector: 'app-repair-request',
  standalone: false,
  templateUrl: './repair-request.html',
  styleUrl: './repair-request.scss'
})
export class RepairRequest {
  model = {serial: '', problem: ''};

  submitRequest() {
    console.log('درخواست تعمیر:', this.model);
    // ارسال به API
  }
}
