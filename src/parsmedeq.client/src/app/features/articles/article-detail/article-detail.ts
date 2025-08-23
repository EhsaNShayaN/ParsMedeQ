import {Component, HostListener, OnDestroy, OnInit, Injector} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {BaseComponent} from '../../../base-component';
import {AppSettings, Settings} from '../../../app.settings';
import {Tables} from '../../../core/constants/server.constants';
import {Resource} from '../../../core/models/ResourceResponse';

@Component({
  selector: 'app-article',
  templateUrl: './article-detail.html',
  styleUrl: './article-detail.scss',
  standalone: false
})
export class ArticleDetail extends BaseComponent implements OnInit, OnDestroy {
  private sub: any;
  public item: Resource | undefined;
  public message: string | undefined;
  public settings: Settings;
  ltr = '';
  tabIndex: string | undefined;
  isLoading = true;
  protected readonly Tables = Tables;

  constructor(public appSettings: AppSettings,
              private activatedRoute: ActivatedRoute) {
    super();
    this.settings = this.appSettings.settings;
  }

  @HostListener('window:scroll') onWindowScroll() {
    const scrollTop = Math.max(window.pageYOffset, document.documentElement.scrollTop, document.body.scrollTop);
    const productItem = document.getElementById('product-item');
    const fixTabs = (scrollTop) > (productItem?.offsetTop ?? 0);
    if (productItem) {
      if (fixTabs) {
        productItem.style.marginTop = '-8px';
      } else {
        productItem.style.marginTop = '0';
      }
    }
  }

  ngOnInit() {
  }

  ngAfterViewInit(): void {
    this.sub = this.activatedRoute.params.subscribe(params => {
      const title = Number(params['id']).toString() === params['id'] ? Number(params['id']) : null;
      const id = !!title ? null : params['id'];
      this.getArticleById(id, title);
    });
  }

  public getArticleById(id: string, secondId: number | null) {
    /*this.restClientService.getResource({id, secondId, tableId: this.Tables.Thesis}).subscribe((d: Article) => {
      this.item = d;
      this.setTitle(this.item.title);
      this.setMetaDescription(this.item.abstract);
      this.ltr = this.item.language === 'انگلیسی' ? 'ltr' : '';
    });*/
  }

  scrollToItem(str: string) {
    this.tabIndex = str;
    const x = document.getElementById(str);
    const f: ScrollToOptions = {behavior: 'smooth', top: (x?.offsetTop ?? 0) - 80};
    window.scrollTo(f);
  }

  ngOnDestroy() {
    this.sub?.unsubscribe();
  }
}
