import {Component, Injector, OnInit} from '@angular/core';
import {UntypedFormBuilder, Validators} from '@angular/forms';
import {getCustomEditorConfigs} from '../../../../../core/custom-editor-configs';
import {ActivatedRoute} from '@angular/router';
import {JalaliMomentDateAdapter} from '../../../../../core/custom-date-adapter';
import {Tables} from '../../../../../core/constants/server.constants';
import {BaseResourceComponent} from '../../base-resource.component';
import {Resource} from '../../../../../core/models/ResourceResponse';
import {ResourceCategoriesResponse, ResourceCategory} from '../../../../../core/models/ResourceCategoryResponse';

@Component({
  selector: 'app-notice-add',
  templateUrl: './notice-add.component.html',
  styleUrls: ['./notice-add.component.scss'],
  standalone: false
})
export class NoticeAddComponent extends BaseResourceComponent implements OnInit {
  error?: string;
  resourceCategories: ResourceCategory[] = [];
  editorConfig = getCustomEditorConfigs();
  abstractError = false;


  constructor(public formBuilder: UntypedFormBuilder,
              private activatedRoute: ActivatedRoute,
              injector: Injector) {
    super(injector, Tables.Notice);
  }

  ngOnInit(): void {
    this.sub = this.activatedRoute.params.subscribe(params => {
      this.restApiService.getResourceCategories(Tables.Notice).subscribe((acr: ResourceCategoriesResponse) => {
        this.resourceCategories = acr.resourceCategories;
        if (params['id']) {
          this.restApiService.getResource({id: params['id'], tableId: Tables.Notice}).subscribe((a: Resource) => {
            this.editItem = a;
            this.myForm = this.formBuilder.group({
              category: [this.resourceCategories.find(s => s.id === a.categoryId), Validators.required],
              title: [a.title, Validators.required],
              abstract: [a.abstract, Validators.required],
              description: [a.description, Validators.required],
              image: null,
              keywords: a.keywords,
              expirationDate: null,
              expirationTime: '',
              anchors: this.formBuilder.array([]),
              language: a.language,
              file: null,
              publishDate: null,
              isVip: a.isVip,
            });
            if (a.expirationDate) {
              const array = a.expirationDate.split('/').map(s => Number(s));
              this.myForm.controls['expirationDate'].setValue(new JalaliMomentDateAdapter('').createDate(array[0], array[1] - 1, array[2]));
              this.expDate = a.expirationDate;
              this.expTime = a.expirationTime;
            }
            if (a.publishDate) {
              const array = a.publishDate.split('/').map(s => Number(s));
              this.myForm.controls['publishDate'].setValue(new JalaliMomentDateAdapter('').createDate(array[0], array[1] - 1, array[2]));
              this.pubDate = a.publishDate;
            }
          });
        } else {
          this.myForm = this.formBuilder.group({
            category: ['', Validators.required],
            title: ['', Validators.required],
            abstract: ['', Validators.required],
            description: ['', Validators.required],
            image: '',
            keywords: '',
            journalId: '',
            expirationDate: null,
            expirationTime: '',
            anchors: this.formBuilder.array([]),
            language: '',
            file: '',
            publishDate: null,
            isVip: false,
            showInChem: true,
            showInAcademy: true,
          });
        }
      });
    });
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

  toPersian(s: string): string {
    return s.replace(/\d/g, (d) => '۰۱۲۳۴۵۶۷۸۹'[Number(d)]);
  };

  dateChanged(dateRangeStart: HTMLInputElement, index: number) {
    if (index === 1) {
      this.expDate = this.toEnglish(dateRangeStart.value);
    } else {
      this.pubDate = this.toEnglish(dateRangeStart.value);
    }
  }

  override onFormSubmit(values: any): void {
    this.leaveAbstract();
    super.onFormSubmit(values);
  }

  leaveAbstract() {
    const x = this.myForm.controls['abstract'].value;
    this.abstractError = !x;
  }
}
