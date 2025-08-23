import {Component, OnInit} from '@angular/core';
import {first} from 'rxjs/operators';
import {PureComponent} from '../../../pure-component';
import {AppSettings} from '../../../app.settings';
import {AuthService} from '../../../core/services/auth.service';

@Component({
  selector: 'app-user-menu',
  templateUrl: './user-menu.html',
  styleUrl: './user-menu.scss',
  standalone: false
})
export class UserMenu extends PureComponent implements OnInit {
  profile: any;

  constructor(public appSettings: AppSettings,
              public authService: AuthService) {
    super();
  }

  ngOnInit() {
  }

  logout() {
    this.restApiService.logout().pipe(first()).subscribe((d: boolean) => {
    });
  }
}
