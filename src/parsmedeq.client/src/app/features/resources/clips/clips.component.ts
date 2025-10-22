import {AfterViewInit, Component, ElementRef} from '@angular/core';
import {BasePageResources} from '../base-page-resources';
import {Tables} from '../../../core/constants/server.constants';
import {ClipDialogComponent} from './clip-dialog/clip-dialog.component';
import {MatDialog} from '@angular/material/dialog';
import {Resource} from '../../../core/models/ResourceResponse';

@Component({
  selector: 'app-clips',
  templateUrl: './clips.component.html',
  styleUrls: ['./clips.component.scss'],
  standalone: false,
})
export class ClipsComponent extends BasePageResources implements AfterViewInit {
  constructor(private dialog: MatDialog,
              private el: ElementRef) {
    super(Tables.Clip);
  }

  openDialog(clip: Resource, event?: MouseEvent) {
    console.log(clip);
    if (event) event.stopPropagation();
    this.dialog.open(ClipDialogComponent, {
      data: {clip},
      width: 'min(980px, 95vw)',
      panelClass: 'custom-clip-dialog',
      hasBackdrop: true,
      backdropClass: 'dark-backdrop'
    });
  }

  ngAfterViewInit() {
    const io = new IntersectionObserver((entries) => {
      entries.forEach(e => {
        if (e.isIntersecting) {
          e.target.classList.add('visible');
          io.unobserve(e.target);
        }
      });
    }, {threshold: 0.1});
    this.el.nativeElement.querySelectorAll('.clip-card').forEach((n: HTMLElement) => io.observe(n));
  }
}
