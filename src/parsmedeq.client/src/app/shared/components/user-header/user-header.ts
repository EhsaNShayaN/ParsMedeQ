import {Component} from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-user-header',
  templateUrl: './user-header.html',
  styleUrls: ['./user-header.scss']
})
export class UserHeader {
  constructor(private router: Router) {
  }

  logout() {
    // TODO: حذف توکن از localStorage یا سرویس Auth
    localStorage.removeItem('token');
    this.router.navigate(['/auth/login']);
  }
}
