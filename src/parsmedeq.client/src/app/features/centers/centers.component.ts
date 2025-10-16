import {Component, OnInit} from '@angular/core';
import {BaseComponent} from '../../../base-component';
import {DialogService} from "../../../../lib/core/services/dialog-service";

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.scss']
})
export class ProjectsComponent extends BaseComponent implements OnInit {
  array = [];

  constructor(private dialogService: DialogService) {
    super();
  }

  ngOnInit(): void {
    this.jsonService.getAbout('projects').subscribe(res => {
      this.array = res.filter(s => !s.deleted);
    });
  }

  openDialog(item: any) {
    this.dialogService.openCustomDialog(item.name, item.description, `/assets/images/projects/${item.id}.jpg`);
  }
}
