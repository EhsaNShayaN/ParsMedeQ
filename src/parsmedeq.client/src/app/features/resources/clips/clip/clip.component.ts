import {AfterViewInit, Component, ElementRef} from '@angular/core';
import {BasePageResource} from '../../base-page-resource';
import {Tables} from '../../../../core/constants/server.constants';

@Component({
  selector: 'app-clip',
  templateUrl: './clip.component.html',
  styleUrls: ['./clip.component.scss'],
  standalone: false,
})
export class ClipComponent extends BasePageResource implements AfterViewInit {
  constructor(private el: ElementRef) {
    super(Tables.Clip);
  }

  override ngAfterViewInit() {
    super.ngAfterViewInit();
    setTimeout(() => {
      this.el.nativeElement.querySelectorAll('.fade-in').forEach((s: HTMLElement) => s.classList.add('visible'));
    }, 80);
  }

  openPlayer() {
    if (!this.item) return;
    this.dialogService.openClipDialog(this.item);
  }
}
