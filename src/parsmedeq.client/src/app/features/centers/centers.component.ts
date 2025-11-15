import {Component, OnInit} from '@angular/core';
import {BaseComponent} from '../../base-component';
import {JsonService} from '../../core/json.service';
import {TreatmentCenter, TreatmentCenterResponse} from '../../core/models/TreatmentCenterResponse';
import {Location, LocationResponse} from '../../core/models/LocationResponse';
import {MatSelectChange} from '@angular/material/select';

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
  locations: Location[] = [];
  provinces: Location[] = [];
  cities: Location[] = [];
  query: string = '';
  provinceId: number = 0;
  cityId: number = 0;

  constructor(private jsonService: JsonService) {
    super();
  }

  ngOnInit(): void {
    this.restApiService.getLocations().subscribe((loc: LocationResponse) => {
      this.locations = loc.data;
      this.provinces = this.locations.filter(s => !s.parentId);
      this.getItems();
    });
  }

  getItems() {
    let model = {
      pageIndex: this.pageIndex,
      pageSize: this.pageSize,
      sort: 0,
      query: this.query,
      provinceId: this.provinceId,
      cityId: this.cityId,
    };
    this.restApiService.getTreatmentCenters(model).subscribe((res: TreatmentCenterResponse) => {
      res.data.items.forEach(item => {
        item.city = this.locations.find(s => s.id === item.cityId)?.title ?? '';
        item.province = this.locations.find(s => s.id === item.provinceId)?.title ?? '';
      })
      this.array = res.data.items;
    });
  }

  openDialog(item: any) {
    this.dialogService.openCustomDialog(item.title, item.description, item.image);
  }

  provinceChanged($event: MatSelectChange<any>) {
    this.cities = this.locations.filter(s => s.parentId === $event.value);
    this.provinceId = $event.value;
    this.cityId = 0;
    this.getItems();
  }

  cityChanged($event: MatSelectChange<any>) {
    this.cityId = $event.value;
    this.getItems();
  }

  reset() {
    this.query = '';
    this.provinceId = 0;
    this.cityId = 0;
    this.cities = [];
    this.getItems();
  }
}
