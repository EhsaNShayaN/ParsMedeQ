import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormArray, UntypedFormArray, UntypedFormBuilder, UntypedFormGroup, Validators} from '@angular/forms';
import {ActivatedRoute} from '@angular/router';
import * as uuid from 'uuid';
import * as moment from 'jalali-moment';
import {Tables} from '../../../../core/constants/server.constants';
import {getCustomEditorConfigs} from '../../../../core/custom-editor-configs';
import {ProductCategoriesResponse, ProductCategory} from '../../../../core/models/ProductCategoryResponse';
import {Product} from '../../../../core/models/ProductResponse';
import {JalaliMomentDateAdapter} from '../../../../core/custom-date-adapter';
import {AddResult, BaseResult} from '../../../../core/models/BaseResult';
import {BaseComponent} from '../../../../base-component';
import {TranslateService} from '@ngx-translate/core';
import {CustomConstants} from '../../../../core/constants/custom.constants';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-product-add',
  templateUrl: './product-add.component.html',
  styleUrl: './product-add.component.scss',
  standalone: false
})
export class ProductAddComponent extends BaseComponent implements OnInit, OnDestroy {
  lang: string;
  sub: any;
  myForm!: UntypedFormGroup;
  expDate: any;
  expTime: any;
  editItem?: Product;
  oldImagePath: string = '';
  oldFileId: number = 0;
  image?: File;
  file?: File;
  //////////////////
  error?: string;
  productCategories: ProductCategory[] = [];
  editorConfig = getCustomEditorConfigs();
  abstractError = false;

  constructor(public formBuilder: UntypedFormBuilder,
              private activatedRoute: ActivatedRoute,
              private toaster: ToastrService,
              private translateService: TranslateService) {
    super();
    this.lang = this.translateService.getDefaultLang();
  }

  get anchorsArray() {
    return this.myForm.get('anchors') as FormArray;
  }

  ngOnInit(): void {
    this.sub = this.activatedRoute.params.subscribe(params => {
      this.restApiService.getProductCategories().subscribe((acr: ProductCategoriesResponse) => {
        this.productCategories = acr.data;
        if (params['id']) {
          this.restApiService.getProduct({id: params['id']}).subscribe((a: BaseResult<Product>) => {
            this.editItem = a.data;
            this.myForm = this.formBuilder.group({
              productCategoryId: [this.editItem.productCategoryId, Validators.required],
              title: [this.editItem.title, Validators.required],
              abstract: [this.editItem.abstract, Validators.required],
              imagePath: null,
              keywords: this.editItem.keywords,
              expirationDate: null,
              expirationTime: '',
              anchors: this.formBuilder.array([]),
              language: this.editItem.language,
              fileId: null,
              publishDate: this.editItem.publishDate,
              isVip: this.editItem.isVip,
              price: this.editItem.price,
              discount: this.editItem.discount,
            });
            this.oldImagePath = this.editItem.image;
            this.oldFileId = this.editItem.fileId ?? 0;
            if (this.editItem.expirationDate) {
              const array = this.editItem.expirationDate.split('/').map(s => Number(s));
              this.myForm.controls['expirationDate'].setValue(new JalaliMomentDateAdapter('').createDate(array[0], array[1] - 1, array[2]));
              this.expDate = this.editItem.expirationDate;
              this.expTime = this.editItem.expirationTime;
            }
            this.editAnchors();
            this.hideSingleLangControls();
          });
        } else {
          this.myForm = this.formBuilder.group({
            productCategoryId: ['', Validators.required],
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

  hideSingleLangControls() {
    if (this.lang !== 'fa') {
      const validFields = ['title', 'abstract', 'keywords', 'anchors', 'description'];
      Object.keys(this.myForm.controls).filter(s => !validFields.includes(s)).forEach(key => {
        this.myForm.get(key)?.disable();
      });
    }
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

  deleteImage() {
    this.oldImagePath = '';
  }

  deleteFile() {
    this.oldFileId = 0;
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
    const startDate = this.toGeorgianDate();
  }

  onFormSubmit(values: any): void {
    this.leaveAbstract();
    const category = this.productCategories.find(s => s.id === Number(this.myForm.controls['productCategoryId'].value));
    values.productCategoryTitle = category?.title;
    if (!this.myForm.valid) {
      console.log(this.findInvalidControls(this.myForm));
      return;
    }
    if (this.expDate) {
      values.expirationDate = this.expDate;
    }
    if (this.editItem) {
      values.id = this.editItem.id;
      if (!this.image) {
        values.oldImagePath = this.oldImagePath;
      }
      if (!this.file) {
        values.oldFileId = this.oldFileId;
      }
    }
    delete values.imagePath;
    delete values.fileId;
    this.restApiService.addProduct(values, this.image, this.file).subscribe((d: BaseResult<AddResult>) => {
      this.toaster.success(CustomConstants.THE_OPERATION_WAS_SUCCESSFUL, '', {});
    });
  }

  toGeorgianDate() {
    const start = moment.from(this.expDate, 'fa', 'YYYY/MM/DD').locale('en').format('YYYY/MM/DD');
    const yearStart = Number(start.substring(0, 4));
    const monthStart = Number(start.substring(5, 7));
    const dayStart = Number(start.substring(8, 10));
    const startDate = new Date(yearStart, monthStart - 1, dayStart);
    return startDate;
  }

  leaveAbstract() {
    const x = this.myForm.controls['abstract'].value;
    this.abstractError = !x;
  }

  editAnchors() {
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

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
