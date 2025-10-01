import {Component, OnInit} from '@angular/core';
import {Validators} from '@angular/forms';
import {ActivatedRoute} from '@angular/router';
import {Tables} from '../../../../../core/constants/server.constants';
import {BaseResourceComponent} from '../../base-resource.component';
import {getCustomEditorConfigs} from '../../../../../core/custom-editor-configs';
import {ResourceCategoriesResponse, ResourceCategory} from '../../../../../core/models/ResourceCategoryResponse';
import {Resource} from '../../../../../core/models/ResourceResponse';
import {BaseResult} from '../../../../../core/models/BaseResult';

@Component({
  selector: 'app-article-add',
  templateUrl: './article-add.component.html',
  styleUrl: './article-add.component.scss',
  standalone: false
})
export class ArticleAddComponent extends BaseResourceComponent implements OnInit {
  error?: string;
  resourceCategories: ResourceCategory[] = [];
  editorConfig = getCustomEditorConfigs();

  constructor(private activatedRoute: ActivatedRoute) {
    super(Tables.Article);
  }
  ngOnInit(): void {
    this.sub = this.activatedRoute.params.subscribe(params => {
      this.restApiService.getResourceCategories(Tables.Article).subscribe((acr: ResourceCategoriesResponse) => {
        this.resourceCategories = acr.data;
        if (params['id']) {
          this.restApiService.getResource({id: params['id'], tableId: Tables.Article}).subscribe((a: BaseResult<Resource>) => {
            this.editItem = a.data;
            this.myForm = this.formBuilder.group({
              resourceCategoryId: [this.editItem.resourceCategoryId, Validators.required],
              title: [this.editItem.title, Validators.required],
              abstract: [this.editItem.abstract, Validators.required],
              imagePath: null,
              fileId: null,
              keywords: this.editItem.keywords,
              /*expirationDate: null,
              expirationTime: '',
              language: this.editItem.language,
              publishDate: this.editItem.publishDate,*/
              anchors: this.formBuilder.array([]),
              price: this.editItem.price,
              discount: this.editItem.discount,
            });
            this.oldImagePath = this.editItem.image;
            this.oldFileId = this.editItem.fileId ?? 0;
            /*if (this.editItem.expirationDate) {
              const array = this.editItem.expirationDate.split('/').map(s => Number(s));
              this.myForm.controls['expirationDate'].setValue(new JalaliMomentDateAdapter('').createDate(array[0], array[1] - 1, array[2]));
              this.expDate = this.editItem.expirationDate;
              this.expTime = this.editItem.expirationTime;
            }*/
            this.editAnchors();
            this.hideSingleLangControls();
          });
        } else {
          this.myForm = this.formBuilder.group({
            resourceCategoryId: ['', Validators.required],
            title: ['', Validators.required],
            abstract: ['', Validators.required],
            imagePath: null,
            keywords: '',
            /*expirationDate: null,
            expirationTime: '',
            language: '',
            publishDate: '',*/
            anchors: this.formBuilder.array([]),
            fileId: null,
            price: null,
            discount: null,
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
