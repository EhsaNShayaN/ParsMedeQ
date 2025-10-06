import {Component} from '@angular/core';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.html',
  styleUrl: './home.scss'
})
export class Home {
  slides = [
    {image: '/assets/slider/slide1.jpg', title: 'عنوان اول', description: 'توضیح کوتاه درباره اسلاید اول'},
    {image: '/assets/slider/slide2.jpg', title: 'عنوان دوم', description: 'توضیح دوم'},
    {image: '/assets/slider/slide3.jpg', title: 'عنوان سوم', description: 'توضیح سوم'},
  ];


  isRtl = true; // یا false
}
