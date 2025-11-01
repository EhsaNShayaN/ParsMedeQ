import {Component, OnInit} from '@angular/core';
import {BaseComponent} from '../../base-component';
import {JsonService} from '../../core/json.service';
import {TreatmentCenter, TreatmentCenterResponse} from '../../core/models/TreatmentCenterResponse';
import {LocationResponse} from '../../core/models/LocationResponse';

@Component({
  selector: 'app-centers',
  templateUrl: './centers.component.html',
  styleUrl: './centers.component.scss',
  standalone: false
})
export class CentersComponent extends BaseComponent implements OnInit {
  array: TreatmentCenter[] = [];
  pageIndex = 1;
  pageSize = 50;

  constructor(private jsonService: JsonService) {
    super();
  }

  ngOnInit(): void {
    let model = {
      pageIndex: this.pageIndex,
      pageSize: this.pageSize,
      sort: 0,
    };
    this.restApiService.getLocations().subscribe((loc: LocationResponse) => {
      this.restApiService.getTreatmentCenters(model).subscribe((res: TreatmentCenterResponse) => {
        res.data.items.forEach(item => {
          const city = loc.data.find(s => s.id === item.locationId);
          item.city = loc.data.find(s => s.id === item.locationId)?.title ?? '';
          item.province = loc.data.find(s => s.id === city?.parentId)?.title ?? '';
        })
        this.array = res.data.items;
      });
    });
  }

  openDialog(item: any) {
    this.dialogService.openCustomDialog(item.title, item.description, item.image);
  }
}
