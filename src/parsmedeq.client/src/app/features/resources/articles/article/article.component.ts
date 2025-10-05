import {Component, HostListener, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {AppSettings, Settings} from '../../../../app.settings';
import {Article} from '../../../../../lib/models/ArticleResponse';
import {BaseComponent} from '../../../../base-component';
import {Tables} from '../../../../../lib/core/constants/server.constants';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.scss']
})
export class ArticleComponent extends BaseComponent implements OnInit, OnDestroy {
  private sub: any;
  public item: Article;
  public message: string;
  public settings: Settings;
  ltr = '';
  tabIndex: string;

  constructor(public appSettings: AppSettings,
              private activatedRoute: ActivatedRoute) {
    super();
    this.settings = this.appSettings.settings;
  }

  @HostListener('window:scroll') onWindowScroll() {
    const scrollTop = Math.max(window.pageYOffset, document.documentElement.scrollTop, document.body.scrollTop);
    const productItem = document.getElementById('product-item');
    const fixTabs = (scrollTop) > productItem?.offsetTop;
    if (fixTabs) {
      productItem.style.marginTop = '-8px';
    } else {
      productItem.style.marginTop = '0';
    }
  }

  ngOnInit() {
  }

  ngAfterViewInit(): void {
    this.sub = this.activatedRoute.params.subscribe(params => {
      const title = Number(params.id).toString() === params.id ? Number(params.id) : null;
      const id = !!title ? null : params.id;
      this.getArticleById(id, title);
    });
  }

  public getArticleById(id: string, secondId: number) {
    this.restClientService.getResource({id, secondId, tableId: Tables.Thesis}).subscribe((d: Article) => {
      this.item = d;
      this.setTitle(this.item.title);
      this.setMetaDescription(this.item.abstract);
      this.ltr = this.item.language === 'انگلیسی' ? 'ltr' : '';
    });
  }

  scrollToItem(str: string) {
    this.tabIndex = str;
    const x = document.getElementById(str);
    const f: ScrollToOptions = {behavior: 'smooth', top: x.offsetTop - 80};
    window.scrollTo(f);
  }

  ngOnDestroy() {
    this.sub?.unsubscribe();
  }

  protected readonly Tables = Tables;
}
