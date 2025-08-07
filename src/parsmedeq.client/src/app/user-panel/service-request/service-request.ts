import {Component} from '@angular/core';

@Component({
  selector: 'app-service-request',
  standalone: false,
  templateUrl: './service-request.html',
  styleUrl: './service-request.scss'
})
export class ServiceRequest {
  model = {product: '', description: ''};

  submitRequest() {
    console.log('درخواست سرویس:', this.model);
    // ارسال به API
  }
}
