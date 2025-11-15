import {AfterViewInit, Directive, HostListener, inject, OnDestroy} from '@angular/core';
import {BaseComponent} from '../../base-component';
import {Resource} from '../../core/models/ResourceResponse';
import {AppSettings, Settings} from '../../app.settings';
import {ActivatedRoute} from '@angular/router';
import {BaseResult} from '../../core/models/BaseResult';
import {Tables} from '../../core/constants/server.constants';

@Directive()
export class BasePageResource extends BaseComponent implements AfterViewInit, OnDestroy {
  public readonly Tables = Tables;
  public appSettings: AppSettings;
  public settings: Settings;
  private activatedRoute: ActivatedRoute;
  private sub: any;
  public item?: Resource;
  public message: string = '';
  ltr = '';
  tabIndex: string = '';
  top = false;
  id: number = 0;

  constructor(private tableId: number) {
    super();
    this.appSettings = inject(AppSettings);
    this.activatedRoute = inject(ActivatedRoute);
    this.settings = this.appSettings.settings;
  }

  @HostListener('window:scroll') onWindowScroll() {
    const scrollTop = Math.max(window.pageYOffset, document.documentElement.scrollTop, document.body.scrollTop);
    this.top = scrollTop >= 40;
    const productItem = document.getElementById('product-item');
    if (productItem) {
      const fixTabs = (scrollTop) > (productItem?.offsetTop ?? 0);
      if (fixTabs) {
        productItem.style.marginTop = '-8px';
      } else {
        productItem.style.marginTop = '0';
      }
    }
  }

  ngAfterViewInit(): void {
    this.sub = this.activatedRoute.params.subscribe(params => {
      this.id = Number(params['id']);
      this.getResourceById();
    });
  }

  public getResourceById() {
    this.restApiService.getResource({id: this.id, tableId: this.tableId}).subscribe((d: BaseResult<Resource>) => {
      this.item = d.data;
      this.setTitle(this.item.title);
      this.setMetaDescription(this.item.abstract);
      this.ltr = this.item.language === 'انگلیسی' ? 'ltr' : '';
    });
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
