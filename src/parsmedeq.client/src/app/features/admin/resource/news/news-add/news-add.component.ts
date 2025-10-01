import {Component, OnInit} from '@angular/core';
import {Validators} from '@angular/forms';
import {getCustomEditorConfigs} from '../../../../../core/custom-editor-configs';
import {ActivatedRoute} from '@angular/router';
import {Tables} from '../../../../../core/constants/server.constants';
import {BaseResourceComponent} from '../../base-resource.component';
import {Resource} from '../../../../../core/models/ResourceResponse';
import {ResourceCategoriesResponse, ResourceCategory} from '../../../../../core/models/ResourceCategoryResponse';
import {BaseResult} from '../../../../../core/models/BaseResult';

@Component({
  selector: 'app-news-add',
  templateUrl: './news-add.component.html',
  styleUrls: ['./news-add.component.scss'],
  standalone: false
})
export class NewsAddComponent extends BaseResourceComponent implements OnInit {
  error?: string;
  resourceCategories: ResourceCategory[] = [];
  editorConfig = getCustomEditorConfigs();

  constructor(private activatedRoute: ActivatedRoute) {
    super(Tables.News);
  }

  ngOnInit(): void {
    this.sub = this.activatedRoute.params.subscribe(params => {
      this.restApiService.getResourceCategories(Tables.News).subscribe((acr: ResourceCategoriesResponse) => {
        this.resourceCategories = acr.data;
        if (params['id']) {
          this.restApiService.getResource({id: params['id'], tableId: Tables.News}).subscribe((a: BaseResult<Resource>) => {
            this.editItem = a.data;
            console.log('editItem', this.editItem);
            this.myForm = this.formBuilder.group({
              resourceCategoryId: [this.editItem.resourceCategoryId, Validators.required],
              title: [this.editItem.title, Validators.required],
              abstract: [this.editItem.abstract, Validators.required],
              description: [this.editItem.description, Validators.required],
              imagePath: null,
              fileId: null,
              keywords: this.editItem.keywords,
              /*expirationDate: null,
              expirationTime: '',
              language: a.language,
              publishDate: null,*/
              anchors: this.formBuilder.array([]),
            });
            this.oldImagePath = this.editItem.image;
            this.oldFileId = this.editItem.fileId ?? 0;
            /*if (a.expirationDate) {
              const array = a.expirationDate.split('/').map(s => Number(s));
              this.myForm.controls['expirationDate'].setValue(new JalaliMomentDateAdapter('').createDate(array[0], array[1] - 1, array[2]));
              this.expDate = a.expirationDate;
              this.expTime = a.expirationTime;
            }*/
            /*if (a.publishDate) {
              const array = a.publishDate.split('/').map(s => Number(s));
              this.myForm.controls['publishDate'].setValue(new JalaliMomentDateAdapter('').createDate(array[0], array[1] - 1, array[2]));
              this.pubDate = a.publishDate;
            }*/
          });
        } else {
          this.myForm = this.formBuilder.group({
            resourceCategoryId: ['', Validators.required],
            title: ['', Validators.required],
            abstract: ['', Validators.required],
            description: ['', Validators.required],
            imagePath: '',
            fileId: '',
            keywords: '',
            /*expirationDate: null,
            expirationTime: '',
            language: '',
            publishDate: null,*/
            anchors: this.formBuilder.array([]),
          });
        }
      });
    });
  }

  override onFormSubmit(values: any): void {
    this.leaveAbstract();
    const category = this.resourceCategories.find(s => s.id === Number(this.myForm.controls['resourceCategoryId'].value));
    values.resourceCategoryTitle = category?.title;
    super.onFormSubmit(values);
  }
}
