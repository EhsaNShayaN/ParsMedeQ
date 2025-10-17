import {Component} from '@angular/core';
import {
  trigger, transition, style, animate, query, stagger
} from '@angular/animations';
import {BaseComponent} from '../../base-component';

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
  cartCount = 0;
  toastMessage = '';

  products = [
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
  ];

  // ساده‌ترین نمونه‌ی افزودن به سبد (تو اینجا فقط شمارش و نمایش toast است)
  addToCart(item: any) {
    this.cartCount++;
    this.showToast(`${item.name} به سبد اضافه شد.`);
    // TODO: در عمل اینجا باید سرویس CartService را صدا بزنی تا در localStorage یا سرور ذخیره شود
  }

  showToast(msg: string) {
    this.toastMessage = msg;
    setTimeout(() => this.toastMessage = '', 2500);
  }

  openCart() {
    // TODO: باز کردن پنل سبد خرید یا روت به صفحه cart
    console.log('open cart');
  }

  viewProduct(p: any) {
    console.log('view product', p); /* navigate to product page */
  }

  readArticle(a: any) {
    console.log('read article', a); /* navigate to article */
  }

  playClip(c: any) {
    console.log('play clip', c); /* open modal player */
  }

  readNews(n: any) {
    console.log('read news', n);
  }

  onLogin() {
    console.log('open login');
  }

  sendContact() {
    this.showToast('پیام شما ارسال شد.');
  }
}
