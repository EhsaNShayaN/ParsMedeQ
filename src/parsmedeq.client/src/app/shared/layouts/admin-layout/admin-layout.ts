import {AfterViewInit, Component, HostListener, OnInit, ViewChild} from '@angular/core';
import {PureComponent} from '../../../pure-component';
import {NavigationEnd, Router} from '@angular/router';

@Component({
  selector: 'app-admin-layout',
  standalone: false,
  templateUrl: './admin-layout.html',
  styleUrl: './admin-layout.scss'
})
export class AdminLayout extends PureComponent implements OnInit, AfterViewInit {
  @ViewChild('sidenav') sidenav: any;
  public sidenavOpen = true;
  public links = [
    {name: 'پروفایل', href: '/user', icon: 'person'},
    {name: 'کاربران', href: 'users', icon: 'view_list'},
    {name: 'محصولات', href: 'product', icon: 'view_list'},
    {name: 'مقالات', href: 'article', icon: 'view_list'},
    {name: 'اخبار', href: 'news', icon: 'view_list'},
    {name: 'کلیپ ها', href: 'clip', icon: 'view_list'},
    {name: 'نظرات', href: 'comment', icon: 'view_list'},
    {name: 'پرداخت ها', href: 'payment', icon: 'money'},
    {name: 'سفارشات', href: 'order', icon: 'reorder'},
    {name: 'تیکت ها', href: 'ticket', icon: 'reorder'},
    {name: 'سانترهای درمانی', href: 'treatment-center', icon: 'reorder'},
    {name: 'خدمات', href: 'service', icon: 'reorder'},
    {name: 'سرویس های دوره ای', href: 'periodic-service', icon: 'reorder'},
    {name: 'ویرایش صفحه اصلی', href: 'sections', icon: 'reorder'},
  ];
  profile: any;
  //profile: Profile;
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
