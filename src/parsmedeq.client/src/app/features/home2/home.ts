import {Component} from '@angular/core';
import {
  trigger, transition, style, animate, query, stagger
} from '@angular/animations';
import {BaseComponent} from '../../base-component';
import {Product, ProductResponse, ProductsRequest} from '../../core/models/ProductResponse';
import {Resource, ResourceResponse, ResourcesRequest} from '../../core/models/ResourceResponse';
import {Tables} from '../../core/constants/server.constants';
import {ToastrService} from 'ngx-toastr';
import {CenterModel, JsonService} from '../../core/json.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.html',
  styleUrls: ['./home.scss'],
  animations: [
    trigger('centersAnimation', [
      transition('* => *', [
        query(':enter', [
          style({opacity: 0, transform: 'translateY(20px)'}),
          stagger(100, [animate('400ms ease-out', style({opacity: 1, transform: 'translateY(0)'}))])
        ], {optional: true})
      ])
    ]),
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

  products: Product[] = [];
  articles: Resource[] = [];
  clips: Resource[] = [];
  news: Resource[] = [];
  centers: CenterModel[] = [];

  constructor(private toastr: ToastrService,
              private jsonService: JsonService) {
    super();
    this.getProducts();
    this.getResources(Tables.Article);
    this.getResources(Tables.Clip);
    this.getResources(Tables.News);

    this.jsonService.getCenters().subscribe(res => {
      this.centers = res;
    });
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

  openDialog(item: CenterModel) {
    this.dialogService.openCustomDialog(item.title, item.city + '<br/>' + item.description, item.image);
  }
}
