import {Component, HostListener, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {AppSettings, Settings} from '../../../../app.settings';
import {Notice} from '../../../../../lib/models/NoticeResponse';
import {BaseComponent} from '../../../../base-component';
import {Tables} from '../../../../../lib/core/constants/server.constants';

@Component({
  selector: 'app-notice',
  templateUrl: './notice.component.html',
  styleUrls: ['./notice.component.scss']
})
export class NoticeComponent extends BaseComponent implements OnInit, OnDestroy {
  private sub: any;
  private sub2: any;
  public item: any;
  public itemId: any;
  public message: string;
  public settings: Settings;
  top = false;
  ltr = '';

  constructor(public appSettings: AppSettings,
              private activatedRoute: ActivatedRoute) {
    super();
    this.settings = this.appSettings.settings;
  }

  @HostListener('window:scroll') onWindowScroll() {
    const scrollTop = Math.max(window.pageYOffset, document.documentElement.scrollTop, document.body.scrollTop);
    this.top = scrollTop >= 40;
  }

  ngOnInit() {
  }

  ngAfterViewInit(): void {
    this.sub = this.activatedRoute.params.subscribe(params => {
      this.itemId = params.id;
      this.getNoticeById(params.id);
    });
  }

  public getNoticeById(id) {
    this.restClientService.getResource({id, tableId: Tables.Notice}).subscribe((d: Notice) => {
      this.item = d;
      this.ltr = this.item.language === 'انگلیسی' ? 'ltr' : '';
      this.sub2 = this.activatedRoute.fragment.subscribe((fragment: string) => {
        /*console.log('a: ' + fragment);
        this.scrollToItem(fragment);*/
      });
    });
  }

  scrollToItem(str: string) {
    const x = document.getElementById(str);
    const f: ScrollToOptions = {behavior: 'smooth', top: x.offsetTop};
    window.scrollTo(f);
  }

  ngOnDestroy() {
    this.sub?.unsubscribe();
    this.sub2?.unsubscribe();
  }

  protected readonly Tables = Tables;
}
