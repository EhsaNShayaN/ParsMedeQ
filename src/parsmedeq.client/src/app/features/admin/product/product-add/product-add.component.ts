import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {FormGroupDirective, UntypedFormBuilder, UntypedFormGroup, Validators} from '@angular/forms';
import {ActivatedRoute} from '@angular/router';
import {getCustomEditorConfigs} from '../../../../core/custom-editor-configs';
import {ProductCategoriesResponse, ProductCategory} from '../../../../core/models/ProductCategoryResponse';
import {Product} from '../../../../core/models/ProductResponse';
import {AddResult, BaseResult} from '../../../../core/models/BaseResult';
import {BaseComponent} from '../../../../base-component';
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
  @ViewChild(FormGroupDirective) formDir!: FormGroupDirective;
  editItem?: Product;
  oldImagePath: string = '';
  oldFileId: number = 0;
  image?: File;
  file?: File;
  //////////////////
  error?: string;
  productCategories: ProductCategory[] = [];
  editorConfig = getCustomEditorConfigs();
  descriptionError = false;

  constructor(public formBuilder: UntypedFormBuilder,
              private activatedRoute: ActivatedRoute,
              private toaster: ToastrService) {
    super();
    this.lang = this.translateService.getDefaultLang();
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
              description: [this.editItem.description, Validators.required],
              imagePath: null,
              fileId: null,
              price: this.editItem.price,
              discount: this.editItem.discount,
              gallery: [null, Validators.required]
            });
            this.oldImagePath = this.editItem.image;
            this.oldFileId = this.editItem.fileId ?? 0;
            this.hideSingleLangControls();
          });
        } else {
          this.myForm = this.formBuilder.group({
            productCategoryId: ['', Validators.required],
            title: [null, Validators.required],
            description: [null, Validators.required],
            imagePath: null,
            fileId: null,
            price: null,
            discount: null,
            gallery: [null, Validators.required]
          });
        }
      });
    });
  }

  hideSingleLangControls() {
    if (this.lang !== 'fa') {
      const validFields = ['title', 'imagePath', 'fileId', 'description'];
      Object.keys(this.myForm.controls).filter(s => !validFields.includes(s)).forEach(key => {
        this.myForm.get(key)?.disable();
      });
    }
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

  onFormSubmit(values: any): void {
    console.log('onFormSubmit', values);
    this.leaveDescription();
    const category = this.productCategories.find(s => s.id === Number(this.myForm.controls['productCategoryId'].value));
    values.productCategoryTitle = category?.title;
    if (!this.myForm.valid) {
      console.log(this.findInvalidControls(this.myForm));
      return;
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
      this.toaster.success(this.getTranslateValue('THE_OPERATION_WAS_SUCCESSFUL'), '', {});
      if (!this.editItem) {
        this.formDir.resetForm();
      }
    });
  }

  leaveDescription() {
    const x = this.myForm.controls['description'].value;
    this.descriptionError = !x;
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
