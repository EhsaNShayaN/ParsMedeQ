import {Component, HostListener, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {AppSettings, Settings} from '../../../../app.settings';
import {Clip} from '../../../../../lib/models/ClipResponse';
import {BaseComponent} from '../../../../base-component';
import {Tables} from '../../../../../lib/core/constants/server.constants';

@Component({
  selector: 'app-clip',
  templateUrl: './clip.component.html',
  styleUrls: ['./clip.component.scss']
})
export class ClipComponent extends BaseComponent implements OnInit, OnDestroy {
  private sub: any;
  private sub2: any;
  public item: any;
  public itemId: any;
  public message: string;
  public settings: Settings;
  top = false;

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
      this.getClipById(params.id);
    });
  }

  public getClipById(id) {
    this.restClientService.getResource({id: id, tableId: Tables.Clip}).subscribe((d: Clip) => {
      this.item = d;
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
