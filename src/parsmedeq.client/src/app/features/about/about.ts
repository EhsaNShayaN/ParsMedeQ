import {AfterViewInit, Component, ElementRef, ViewChild} from '@angular/core';
import {TreatmentCenterResponse} from '../../core/models/TreatmentCenterResponse';
import {BaseComponent} from '../../base-component';

@Component({
  selector: 'app-about',
  standalone: false,
  templateUrl: './about.html',
  styleUrl: './about.scss'
})
export class About extends BaseComponent implements AfterViewInit {
  @ViewChild('rewards') rewards!: ElementRef<HTMLDivElement>;
  centersCount: number = 0;
  years: number = new Date().getFullYear() - 2023;
  rewardsCount: number = 0;

  constructor() {
    super();
    let model = {
      pageIndex: 0,
      pageSize: 1,
      sort: 0,
      query: null,
      provinceId: 0,
      cityId: 0,
    };
    this.restApiService.getTreatmentCenters(model).subscribe((res: TreatmentCenterResponse) => {
      this.centersCount = res.data.totalCount;
    });
  }

  ngAfterViewInit() {
    this.rewardsCount = this.rewards.nativeElement.children.length;
  }
}
