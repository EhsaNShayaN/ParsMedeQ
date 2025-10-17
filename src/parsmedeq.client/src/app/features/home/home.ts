import {Component} from '@angular/core';
import {
  trigger, transition, style, animate, query, stagger
} from '@angular/animations';
import {BaseComponent} from '../../base-component';
import {Product, ProductResponse, ProductsRequest} from '../../core/models/ProductResponse';
import {Resource, ResourceResponse, ResourcesRequest} from '../../core/models/ResourceResponse';
import {Tables} from '../../core/constants/server.constants';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-home',
  templateUrl: './home.html',
  styleUrls: ['./home.scss'],
  animations: [
    trigger('heroAnimation', [
      transition(':enter', [
        style({opacity: 0, transform: 'translateY(8px)'}),
        animate('500ms ease-out', style({opacity: 1, transform: 'translateY(0)'}))
      ])
    ]),
    // Products Animation
    trigger('productsAnimation', [
      transition('* => *', [
        query(':enter', [
          style({opacity: 0, transform: 'scale(0.9)'}),
          stagger(100, [
            animate('400ms ease-out', style({opacity: 1, transform: 'scale(1)'}))
          ])
        ], {optional: true})
      ])
    ]),
    // Articles Animation
    trigger('articlesAnimation', [
      transition('* => *', [
        query(':enter', [
          style({opacity: 0, transform: 'translateX(-20px)'}),
          stagger(100, [animate('400ms ease-out', style({opacity: 1, transform: 'translateX(0)'}))])
        ], {optional: true})
      ])
    ]),
    // Clips Animation
    trigger('clipsAnimation', [
      transition('* => *', [
        query(':enter', [
          style({opacity: 0, transform: 'scale(0.85)'}),
          stagger(100, [animate('400ms ease-out', style({opacity: 1, transform: 'scale(1)'}))])
        ], {optional: true})
      ])
    ]),
    // News Animation
    trigger('newsAnimation', [
      transition('* => *', [
        query(':enter', [
          style({opacity: 0}),
          stagger(150, [animate('400ms ease-out', style({opacity: 1}))])
        ], {optional: true})
      ])
    ]),
    trigger('fadeInSimple', [
      transition(':enter', [
        style({opacity: 0, transform: 'translateY(12px)'}),
        animate('420ms ease-out', style({opacity: 1, transform: 'translateY(0)'}))
      ])
    ])
  ],
  standalone: false
})
export class Home extends BaseComponent {

  /*products = [
    {id: 1, name: 'مانیتور علائم حیاتی', shortDescription: 'پیشرفته و دقیق', imageUrl: 'assets/images/p1.jpg'},
    {id: 2, name: 'دستگاه نوار قلب', shortDescription: 'طراحی ارگونومیک', imageUrl: 'assets/images/p2.jpg'},
    {id: 3, name: 'پالس اکسیمتر', shortDescription: 'سبک و قابل حمل', imageUrl: 'assets/images/p3.jpg'},
    // ... می‌تونی با API جایگزین کنی
  ];

  articles = [
    {id: 1, title: 'نکاتی در انتخاب تجهیزات پزشکی', summary: 'راهنمای خرید دستگاه‌های حیاتی', imageUrl: 'assets/images/a1.jpg'},
    {id: 2, title: 'ایمنی بیمار در اتاق عمل', summary: 'اصول نگهداری تجهیزات حیاتی', imageUrl: 'assets/images/a2.jpg'},
  ];

  clips = [
    {id: 1, title: 'آموزش کار با ECG', thumbnail: 'assets/images/c1.jpg'},
    {id: 2, title: 'کالیبراسیون فشارسنج', thumbnail: 'assets/images/c2.jpg'},
  ];

  newsList = [
    {id: 1, title: 'ورود دستگاه جدید اکسیژن‌ساز', summary: 'تجهیز جدید به انبار شرکت اضافه شد', imageUrl: 'assets/images/n1.jpg'},
    {id: 2, title: 'قرارداد همکاری با بیمارستان میلاد', summary: 'تفاهم‌نامه جدید برای تأمین تجهیزات', imageUrl: 'assets/images/n2.jpg'},
  ];*/

  products: Product[] = [];
  articles: Resource[] = [];
  clips: Resource[] = [];
  news: Resource[] = [];

  constructor(private toastr: ToastrService) {
    super();
    this.getProducts();
    this.getResources(Tables.Article);
    this.getResources(Tables.Clip);
    this.getResources(Tables.News);
  }

  getProducts() {
    let model: ProductsRequest = {
      pageIndex: 1,
      pageSize: 10,
      sort: 1,
    };
    this.restApiService.getProducts(model).subscribe((d: ProductResponse) => {
      this.products = d.data.items.slice(0, 4);
    });
  }

  getResources(tableId: number) {
    let model: ResourcesRequest = {
      pageIndex: 1,
      pageSize: 10,
      sort: 1,
      tableId: tableId
    };
    this.restApiService.getResources(model).subscribe((d: ResourceResponse) => {
      switch (tableId) {
        case Tables.Article:
          this.articles = d.data.items.slice(0, 4);
          break;
        case Tables.Clip:
          this.clips = d.data.items.slice(0, 4);
          break;
        case Tables.News:
          this.news = d.data.items.slice(0, 4);
          break;
      }
    });
  }

  sendContact() {
    this.toastr.success(this.getTranslateValue('پیام شما با موفقیت ارسال گردید.'), '', {});
  }
}
