import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {FormGroupDirective, UntypedFormBuilder, UntypedFormGroup, Validators} from '@angular/forms';
import {ProductCategoriesResponse, ProductCategory} from '../../../../core/models/ProductCategoryResponse';
import {BaseComponent} from '../../../../base-component';
import {AddResult, BaseResult} from '../../../../core/models/BaseResult';
import {CustomConstants} from '../../../../core/constants/custom.constants';

@Component({
  selector: 'app-product-category-add',
  templateUrl: './product-category-add.component.html',
  styleUrls: ['./product-category-add.component.scss'],
  standalone: false
})
export class ProductCategoryAddComponent extends BaseComponent implements OnInit, OnDestroy {
  lang: string;
  public myForm!: UntypedFormGroup;
  @ViewChild(FormGroupDirective) formDir!: FormGroupDirective;
  productCategories: ProductCategory[] = [];
  editItem?: ProductCategory;
  /////////////////////
  private sub: any;
  image?: File;
  oldImagePath: string = '';

  constructor(private toastr: ToastrService,
              protected activatedRoute: ActivatedRoute,
              private formBuilder: UntypedFormBuilder) {
    super();
    this.lang = this.translateService.getDefaultLang();
  }

  ngOnInit() {
    this.sub = this.activatedRoute.params.subscribe(params => {
      this.restApiService.getProductCategories().subscribe((acr: ProductCategoriesResponse) => {
        this.productCategories = acr.data;
        if (params['id']) {
          this.editItem = this.productCategories.find(s => s.id === Number(params['id']));
          if (this.editItem) {
            this.productCategories = this.productCategories.filter(s => s.id !== this.editItem!.id);
          }
        }
        this.myForm = this.formBuilder.group({
          title: [this.editItem?.title, Validators.required],
          description: this.editItem?.description,
          parentId: this.editItem?.parentId,
          imagePath: null
        });
        if (this.editItem) {
          this.oldImagePath = this.editItem.image;
          this.hideSingleLangControls();
        }
      });
    });
  }

  handleImageInput(target: any) {
    if (target.files && target.files[0]) {
      this.image = target.files[0];
    }
  }

  deleteImage() {
    this.oldImagePath = '';
  }

  hideSingleLangControls() {
    if (this.lang !== 'fa') {
      const validFields = ['imagePath', 'title', 'description'];
      Object.keys(this.myForm.controls).filter(s => !validFields.includes(s)).forEach(key => {
        this.myForm.get(key)?.disable();
      });
    }
  }

  onFormSubmit(values: any): void {
    if (!this.myForm.valid) {
      console.log(this.findInvalidControls(this.myForm));
      return;
    }
    if (this.editItem) {
      values.id = this.editItem.id;
      if (!this.image) {
        values.oldImagePath = this.oldImagePath;
      }
    }
    delete values.imagePath;
    this.restApiService.addProductCategory(values, this.image).subscribe((d: BaseResult<AddResult>) => {
      if (d.data.changed) {
        this.toastr.success(this.getTranslateValue('THE_OPERATION_WAS_SUCCESSFUL'), '', {});
        if (!this.editItem) {
          this.formDir.resetForm();
        }
      }
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
