import {Component} from '@angular/core';
import {Router} from '@angular/router';
import {CustomConstants} from '../../../core/constants/custom.constants';

@Component({
  selector: 'app-admin-header',
  templateUrl: './admin-header.html',
  styleUrl: './admin-header.scss',
  standalone: false
})
export class AdminHeader {
  constructor(private router: Router) {
  }

  logout() {
    localStorage.removeItem(CustomConstants.tokenName);
    this.router.navigate(['/login']);
  }
}
