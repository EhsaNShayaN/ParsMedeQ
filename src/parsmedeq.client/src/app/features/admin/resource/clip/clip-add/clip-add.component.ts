import {Component, OnInit} from '@angular/core';
import {Validators} from '@angular/forms';
import {ActivatedRoute} from '@angular/router';
import {Tables} from '../../../../../core/constants/server.constants';
import {ResourceCategoriesResponse, ResourceCategory} from '../../../../../core/models/ResourceCategoryResponse';
import {BaseResourceComponent} from '../../base-resource.component';
import {Resource} from '../../../../../core/models/ResourceResponse';
import {getCustomEditorConfigs} from '../../../../../core/custom-editor-configs';
import {BaseResult} from '../../../../../core/models/BaseResult';

@Component({
  selector: 'app-clip-add',
  templateUrl: './clip-add.component.html',
  styleUrls: ['./clip-add.component.scss'],
  standalone: false
})
export class ClipAddComponent extends BaseResourceComponent implements OnInit {
  clipCategories: ResourceCategory[] = [];
  editorConfig = getCustomEditorConfigs();

  constructor(private activatedRoute: ActivatedRoute) {
    super(Tables.Clip);
  }

  ngOnInit() {
    this.sub = this.activatedRoute.params.subscribe(params => {
      this.restApiService.getResourceCategories(Tables.Clip).subscribe((bc: ResourceCategoriesResponse) => {
        this.clipCategories = bc.data;
        if (params['id']) {
          this.restApiService.getResource({id: params['id'], tableId: Tables.Clip}).subscribe((j: BaseResult<Resource>) => {
            this.editItem = j.data;
            this.myForm = this.formBuilder.group({
              title: [this.editItem?.title, Validators.required],
              description: this.editItem?.description,
              imagePath: null,
              fileId: null,
              authors: null,
              resourceCategoryId: this.editItem?.resourceCategoryId,
              price: this.editItem?.price,
              discount: this.editItem?.discount,
            });
            this.oldImagePath = this.editItem.image;
            this.oldFileId = this.editItem.fileId ?? 0;
          });
        } else {
          this.myForm = this.formBuilder.group({
            title: [null, Validators.required],
            description: null,
            imagePath: null,
            fileId: null,
            authors: null,
            resourceCategoryId: null,
            price: '',
            discount: '',
          });
        }
      });
    });
  }
}
