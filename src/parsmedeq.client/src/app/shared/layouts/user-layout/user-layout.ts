import {AfterViewInit, Component, HostListener, OnInit, ViewChild} from '@angular/core';
import {PureComponent} from '../../../pure-component';
import {NavigationEnd} from '@angular/router';
import {Profile} from '../../../core/models/Login';

@Component({
  selector: 'app-user-layout',
  standalone: false,
  templateUrl: './user-layout.html',
  styleUrl: './user-layout.scss'
})
export class UserLayout extends PureComponent implements OnInit, AfterViewInit {
  @ViewChild('sidenav') sidenav: any;
  public sidenavOpen = true;
  public links = [
    {name: 'پروفایل', href: '/user', icon: 'person'},
    {name: 'نظرات', href: 'comment', icon: 'view_list'},
    {name: 'پرداخت ها', href: 'payment', icon: 'money'},
    {name: 'سفارشات', href: 'order', icon: 'reorder'},
    {name: 'تیکت ها', href: 'ticket', icon: 'reorder'},
  ];
  profile?: Profile;
  urlArray = [];

  constructor() {
    super();
    /*this.restClientService.getUserInfo().subscribe((p: ProfileResponse) => {
      this.profile = p.data;
    });*/
  }

  ngOnInit() {
    if (window.innerWidth < 960) {
      this.sidenavOpen = false;
    }
  }

  @HostListener('window:resize')
  public onWindowResize(): void {
    (window.innerWidth < 960) ? this.sidenavOpen = false : this.sidenavOpen = true;
  }

  ngAfterViewInit() {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        if (window.innerWidth < 960) {
          this.sidenav.close();
        }
      }
    });
  }
}
