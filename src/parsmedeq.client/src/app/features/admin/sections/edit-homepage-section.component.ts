import {Component, OnDestroy} from '@angular/core';
import {BaseComponent} from '../../../base-component';
import {ActivatedRoute} from '@angular/router';
import {MatDialog, MatDialogConfig} from '@angular/material/dialog';
import {SectionService} from './section.service';
import {EditMainImageDialog} from './dialogs/edit-main-image.dialog';
import {EditServicesDialog} from './dialogs/edit-services.dialog';
import {EditAdvantagesDialog} from './dialogs/edit-advantages.dialog';
import {EditAboutDialog} from './dialogs/edit-about.dialog';
import {EditBottomImageDialog} from './dialogs/edit-bottom-image.dialog';
import {ToastrService} from 'ngx-toastr';
import {MainSections, SectionType} from '../../../core/constants/server.constants';
import {ForStudyDialog} from './dialogs/for-study.dialog';

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
        const id = Number(params['id']);
        this.service.getAllItems().subscribe(res => {
          let sections = res.data.filter(s => s.sectionId === id);
          let section = res.data.find(s => s.sectionId === id);
          if (!section) {
            const mainSection = MainSections.find(s => s.id === id);
            section = {description: '', hidden: false, image: '', items: [], title: '', type: mainSection.type, id: id, sectionId: id};
            sections = [{description: '', hidden: false, image: '', items: [], title: '', type: mainSection.type, id: id, sectionId: id}];
          }
          const config: MatDialogConfig = {
            width: window.outerWidth + 'px',
            height: window.outerHeight + 'px',
            data: section
          };
          const listConfig: MatDialogConfig = {
            width: window.outerWidth + 'px',
            height: window.outerHeight + 'px',
            data: sections
          };
          switch (id) {
            case SectionType.mainImage:
              this.dialog.open(EditMainImageDialog, config).afterClosed().subscribe(res => {
                this.load(res);
              });
              break;
            case SectionType.centers:
              this.router.navigate([`/${this.translateService.getDefaultLang()}/admin/treatment-center/list`]); // صفحه مدیریت سانترها
              break;
            case SectionType.services:
              this.dialog.open(EditServicesDialog, listConfig).afterClosed().subscribe(res => {
                this.load(res);
              });
              break;
            case SectionType.advantages:
              this.dialog.open(EditAdvantagesDialog, listConfig).afterClosed().subscribe(res => {
                this.load(res);
              });
              break;
            case SectionType.about:
              this.dialog.open(EditAboutDialog, config).afterClosed().subscribe(res => {
                this.load(res);
              });
              break;
            case SectionType.contact:
              break;
            case SectionType.bottomImage:
              this.dialog.open(EditBottomImageDialog, config).afterClosed().subscribe(res => {
                this.load(res);
              });
              break;
            case SectionType.ForStudy:
              this.dialog.open(ForStudyDialog, config).afterClosed().subscribe(res => {
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
