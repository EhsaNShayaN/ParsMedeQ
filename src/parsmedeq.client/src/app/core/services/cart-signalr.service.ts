import {Injectable} from '@angular/core';
import * as signalR from '@microsoft/signalr';
import {CartService} from './cart.service';

@Injectable({providedIn: 'root'})
export class CartSignalRService {
  private hubConnection!: signalR.HubConnection;

  constructor(private cartService: CartService) {
  }

  startConnection(userId: string) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/hubs/cart')
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start().then(() => {
      console.log('SignalR connected');
      this.hubConnection.invoke('JoinCartGroup', userId);
    }).catch(err => console.error(err));

    this.hubConnection.on('CartUpdatedDetailed', (data) => {
      // بروزرسانی BehaviorSubject
      this.cartService.loadCart(userId); // دوباره سبد رو از سرور می‌گیریم
    });
  }

  stopConnection(userId: string) {
    if (this.hubConnection) {
      this.hubConnection.invoke('LeaveCartGroup', userId);
      this.hubConnection.stop();
    }
  }

  onCartUpdatedDetailed(callback: (data: any) => void) {
    this.hubConnection.on('CartUpdatedDetailed', callback);
  }
}
