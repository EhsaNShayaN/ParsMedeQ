import {Component, Injector, OnInit} from '@angular/core';
import {FormArray, UntypedFormArray, UntypedFormBuilder, UntypedFormGroup, Validators} from '@angular/forms';
import {ActivatedRoute} from '@angular/router';
import * as uuid from 'uuid';
import * as moment from 'jalali-moment';
import {Tables} from '../../../../../core/constants/server.constants';
import {BaseResourceComponent} from '../../base-resource.component';
import {getCustomEditorConfigs} from '../../../../../core/custom-editor-configs';
import {ResourceCategoriesResponse, ResourceCategory} from '../../../../../core/models/ResourceCategoryResponse';
import {Resource} from '../../../../../core/models/ResourceResponse';
import {JalaliMomentDateAdapter} from '../../../../../core/custom-date-adapter';

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
  abstractError = false;

  constructor(public formBuilder: UntypedFormBuilder,
              private activatedRoute: ActivatedRoute,
              injector: Injector) {
    super(injector, Tables.Article);
  }

  get anchorsArray() {
    return this.myForm.get('anchors') as FormArray;
  }

  ngOnInit(): void {
    this.sub = this.activatedRoute.params.subscribe(params => {
      this.restApiService.getResourceCategories(Tables.Article).subscribe((acr: ResourceCategoriesResponse) => {
        this.resourceCategories = acr.data;
        if (params['id']) {
          this.restApiService.getResource({id: params['id'], tableId: Tables.Article}).subscribe((a: Resource) => {
            this.editItem = a;
            this.myForm = this.formBuilder.group({
              resourceCategoryId: [this.resourceCategories.find(s => s.id === a.resourceCategoryId)?.id, Validators.required],
              title: [a.title, Validators.required],
              abstract: [a.abstract, Validators.required],
              imagePath: null,
              keywords: a.keywords,
              expirationDate: null,
              expirationTime: '',
              anchors: this.formBuilder.array([]),
              language: a.language,
              fileId: null,
              publishDate: a.publishDate,
              isVip: a.isVip,
              price: a.price,
              discount: a.discount,
            });
            if (a.expirationDate) {
              const array = a.expirationDate.split('/').map(s => Number(s));
              this.myForm.controls['expirationDate'].setValue(new JalaliMomentDateAdapter('').createDate(array[0], array[1] - 1, array[2]));
              this.expDate = a.expirationDate;
              this.expTime = a.expirationTime;
            }
            this.editAnchors();
          });
        } else {
          this.myForm = this.formBuilder.group({
            resourceCategoryId: ['', Validators.required],
            title: ['flkgjfd', Validators.required],
            abstract: ['sdfsdfds', Validators.required],
            imagePath: null,
            keywords: 'sdfsdfs',
            expirationDate: null,
            expirationTime: '',
            anchors: this.formBuilder.array([]),
            language: 'sdfdsfds',
            fileId: null,
            publishDate: '1401/01/12',
            isVip: false,
            price: 12340,
            discount: 10,
          });
        }
      });
    });
  }

  createAnchor(): UntypedFormGroup {
    return this.formBuilder.group({
      id: uuid.v4().replace(/-/g, ''),
      name: null,
      desc: null,
    });
  }

  addAnchor(): void {
    const anchors = this.myForm.controls['anchors'] as UntypedFormArray;
    anchors.push(this.createAnchor());
  }

  deleteAnchor(index: number) {
    const anchors = this.myForm.controls['anchors'] as UntypedFormArray;
    anchors.removeAt(index);
  }

  handleImageInput(target: any) {
    if (target.files && target.files[0]) {
      this.image = target.files[0];
    }
  }

  handleFileInput(target: any) {
    if (target.files && target.files[0]) {
      this.file = target.files[0];
    }
  }

  toEnglish(s: string) {
    let x = s.replace(/[۰-۹]/g, d => '۰۱۲۳۴۵۶۷۸۹'.indexOf(d).toString());
    x = x.replace(/[٠-٩]/g, d => '٠١٢٣٤٥٦٧٨٩'.indexOf(d).toString());
    return x;
  }

  /*toPersian(s: string): string {
    return s.replace(/\d/g, (d) => '۰۱۲۳۴۵۶۷۸۹'[Number(d)]);
  };*/

  dateChanged(dateRangeStart: HTMLInputElement) {
    this.expDate = this.toEnglish(dateRangeStart.value);
    console.log(this.expDate);
    const startDate = this.toGeorgianDate();
    console.log(startDate);
  }

  override onFormSubmit(values: any): void {
    this.leaveAbstract();
    const category = this.resourceCategories.find(s => s.id === Number(this.myForm.controls['resourceCategoryId'].value));
    values.resourceCategoryTitle = category?.title;
    super.onFormSubmit(values);
  }

  toGeorgianDate() {
    const start = moment.from(this.expDate, 'fa', 'YYYY/MM/DD').locale('en').format('YYYY/MM/DD');
    const yearStart = Number(start.substring(0, 4));
    const monthStart = Number(start.substring(5, 7));
    const dayStart = Number(start.substring(8, 10));
    const startDate = new Date(yearStart, monthStart - 1, dayStart);
    console.log(startDate);
    return startDate;
  }

  leaveAbstract() {
    const x = this.myForm.controls['abstract'].value;
    this.abstractError = !x;
  }

  private editAnchors() {
    const anchors = this.myForm.controls['anchors'] as UntypedFormArray;
    while (anchors.length) {
      anchors.removeAt(0);
    }
    this.editItem?.anchors?.forEach(plan => {
      let p = {
        id: plan.id,
        name: plan.name,
        desc: plan.desc,
      };
      anchors.push(this.formBuilder.group(p));
    });
  }
}
