import {Component} from '@angular/core';
import {BaseComponent} from '../../base-component';

@Component({
  selector: 'app-home',
  templateUrl: './home.html',
  styleUrls: ['./home.scss'],
  standalone: false
})
export class Home extends BaseComponent {
  products = [
    {name: 'مانیتور علائم حیاتی', shortDescription: 'پیشرفته و دقیق', imageUrl: 'assets/images/p1.jpg'},
    {name: 'دستگاه نوار قلب', shortDescription: 'طراحی ارگونومیک', imageUrl: 'assets/images/p2.jpg'},
    {name: 'پالس اکسیمتر', shortDescription: 'سبک و قابل حمل', imageUrl: 'assets/images/p3.jpg'},
  ];

  articles = [
    {title: 'نکاتی در انتخاب تجهیزات پزشکی', summary: 'راهنمای خرید دستگاه‌های حیاتی', imageUrl: 'assets/images/a1.jpg'},
    {title: 'ایمنی بیمار در اتاق عمل', summary: 'اصول نگهداری تجهیزات حیاتی', imageUrl: 'assets/images/a2.jpg'},
  ];

  clips = [
    {title: 'آموزش کار با ECG', thumbnail: 'assets/images/c1.jpg'},
    {title: 'کالیبراسیون فشارسنج', thumbnail: 'assets/images/c2.jpg'},
  ];

  centers = [
    {name: 'سانتر تهران', city: 'تهران', specialty: 'قلب و عروق', imageUrl: 'assets/images/center1.jpg'},
    {name: 'سانتر شیراز', city: 'شیراز', specialty: 'بیهوشی', imageUrl: 'assets/images/center2.jpg'},
  ];

  newsList = [
    {title: 'ورود دستگاه جدید اکسیژن‌ساز', summary: 'تجهیز جدید به انبار شرکت اضافه شد', imageUrl: 'assets/images/n1.jpg'},
    {title: 'قرارداد همکاری با بیمارستان میلاد', summary: 'تفاهم‌نامه جدید برای تأمین تجهیزات', imageUrl: 'assets/images/n2.jpg'},
  ];
}
