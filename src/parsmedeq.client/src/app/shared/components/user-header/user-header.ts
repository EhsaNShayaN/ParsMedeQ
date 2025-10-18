import {Component} from '@angular/core';
import {Router} from '@angular/router';
import {CustomConstants} from '../../../core/constants/custom.constants';

@Component({
  selector: 'app-user-header',
  templateUrl: './user-header.html',
  styleUrl: './user-header.scss',
  standalone: false
})
export class UserHeader {
  constructor(private router: Router) {
  }

  logout() {
    // TODO: حذف توکن از localStorage یا سرویس Auth
    localStorage.removeItem(CustomConstants.tokenName);
    this.router.navigate(['/login']);
  }
}
