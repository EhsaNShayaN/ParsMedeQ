import {Component} from '@angular/core';
import {BaseComponent} from '../../base-component';
import {SectionService} from '../admin/sections/section.service';
import {Section} from '../admin/sections/homepage-sections.component';
import {SectionType} from '../../core/constants/server.constants';

@Component({
  selector: 'app-for-study',
  standalone: false,
  templateUrl: './for-study.html',
  styleUrl: './for-study.scss'
})
export class ForStudy extends BaseComponent {
  section?: Section;

  constructor(private service: SectionService) {
    super();
    this.service.getAllItems().subscribe(res => {
      this.section = res.data.find(s => s.sectionId === SectionType.ForStudy);
      console.log(this.section);
    });
  }
}
