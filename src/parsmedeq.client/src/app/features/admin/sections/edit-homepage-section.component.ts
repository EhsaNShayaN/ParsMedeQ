import {Component, OnDestroy} from '@angular/core';
import {BaseComponent} from '../../../base-component';
import {ActivatedRoute} from '@angular/router';
import {MatDialog} from '@angular/material/dialog';
import {SectionService} from './section.service';
import {SectionType} from './homepage-sections.component';
import {EditMainImageDialog} from './dialogs/edit-main-image.dialog';
import {EditServicesDialog} from './dialogs/edit-services.dialog';
import {EditAdvantagesDialog} from './dialogs/edit-advantages.dialog';
import {EditTextDialog} from './dialogs/edit-text.dialog';
import {EditBottomImageDialog} from './dialogs/edit-bottom-image.dialog';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-edit-homepage-section',
  templateUrl: 'edit-homepage-section.component.html',
  standalone: false
})
export class EditHomepageSectionComponent extends BaseComponent implements OnDestroy {
  sub: any;

  constructor(private activatedRoute: ActivatedRoute,
              private dialog: MatDialog,
              private service: SectionService,
              private toastrService: ToastrService) {
    super();
    this.sub = this.activatedRoute.params.subscribe(params => {
      if (params['id']) {
        const id = params['id'];
        this.service.getAll().subscribe(res => {
          const section = res.data.find(s => s.id == id);
          if (!section) return;
          switch (section.id) {
            case SectionType.mainImage:
              this.dialog.open(EditMainImageDialog, {width: '600px', data: section})
                .afterClosed().subscribe(res => {
                console.log('close');
                console.log('res', res);
                this.load(res);
              });
              break;
            case SectionType.centers:
              this.router.navigate(['/admin/centers']); // صفحه مدیریت سانترها
              break;
            case SectionType.services:
              this.dialog.open(EditServicesDialog, {width: '800px', data: section})
                .afterClosed().subscribe(res => {
                this.load(res);
              });
              break;
            case SectionType.advantages:
              this.dialog.open(EditAdvantagesDialog, {width: '800px', data: section})
                .afterClosed().subscribe(res => {
                this.load(res);
              });
              break;
            case SectionType.about:
            case SectionType.contact:
              this.dialog.open(EditTextDialog, {width: '600px', data: section})
                .afterClosed().subscribe(res => {
                this.load(res);
              });
              break;
            case SectionType.bottomImage:
              this.dialog.open(EditBottomImageDialog, {width: '600px', data: section})
                .afterClosed().subscribe(res => {
                this.load(res);
              });
              break;
          }
        });
      }
    });
  }

  private load(res: any) {
    const time = res ? 3000 : 0;
    if (res) {
      this.toastrService.success(this.getTranslateValue('THE_OPERATION_WAS_SUCCESSFUL'), '', {});
    }
    setTimeout(() => {
      //window.close();
    }, time);
  }

  ngOnDestroy() {
    this.sub?.unsubscribe();
  }
}
