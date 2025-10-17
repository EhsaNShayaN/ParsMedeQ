import {Component, OnInit} from '@angular/core';
import {BaseComponent} from '../../base-component';
import {JsonService} from '../../core/json.service';

@Component({
  selector: 'app-centers',
  templateUrl: './centers.component.html',
  styleUrl: './centers.component.scss',
  standalone: false
})
export class CentersComponent extends BaseComponent implements OnInit {
  array: any[] = [];

  constructor(private jsonService: JsonService) {
    super();
  }

  ngOnInit(): void {
    this.jsonService.getCenters().subscribe(res => {
      this.array = res;
    });
  }

  openDialog(item: any) {
    this.dialogService.openCustomDialog(item.title, item.description, item.image);
  }
}
