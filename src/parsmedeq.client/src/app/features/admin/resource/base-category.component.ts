import {Directive, inject, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {FormGroupDirective, UntypedFormBuilder, UntypedFormGroup, Validators} from '@angular/forms';
import {ActivatedRoute} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {ResourceCategoriesResponse, ResourceCategory} from '../../../core/models/ResourceCategoryResponse';
import {BaseComponent} from '../../../base-component';
import {AddResult, BaseResult} from '../../../core/models/BaseResult';
import {CustomConstants} from '../../../core/constants/custom.constants';
import {TranslateService} from '@ngx-translate/core';

@Directive()
export class BaseCategoryComponent extends BaseComponent implements OnInit, OnDestroy {
  lang: string;
  tableId: number;
  toaster = inject(ToastrService);
  formBuilder = inject(UntypedFormBuilder);
  public myForm!: UntypedFormGroup;
  @ViewChild(FormGroupDirective) formDir!: FormGroupDirective;
  resourceCategories: ResourceCategory[] = [];
  editItem?: ResourceCategory;
  /////////////////////
  private sub: any;

  constructor(tableId: number,
              protected activatedRoute: ActivatedRoute) {
    super();
    const translateService = inject(TranslateService);
    this.lang = translateService.getDefaultLang();
    this.tableId = tableId;
  }

  ngOnInit() {
    this.sub = this.activatedRoute.params.subscribe(params => {
      this.restApiService.getResourceCategories(this.tableId).subscribe((acr: ResourceCategoriesResponse) => {
        this.resourceCategories = acr.data;
        if (params['id']) {
          this.editItem = this.resourceCategories.find(s => s.id === Number(params['id']));
          if (this.editItem) {
            this.resourceCategories = this.resourceCategories.filter(s => s.id !== this.editItem!.id);
          }
        }
        this.myForm = this.formBuilder.group({
          title: [this.editItem?.title, Validators.required],
          description: this.editItem?.description,
          parentId: this.editItem?.parentId,
          tableId: this.tableId,
        });
        if (this.editItem) {
          this.hideSingleLangControls();
        }
      });
    });
  }

  hideSingleLangControls() {
    if (this.lang !== 'fa') {
      const validFields = ['title', 'description'];
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
    }
    this.restApiService.addResourceCategory(values).subscribe((d: BaseResult<AddResult>) => {
      this.toaster.success(this.getTranslateValue('THE_OPERATION_WAS_SUCCESSFUL'), '', {});
      if (!this.editItem) {
        this.formDir.resetForm();
      }
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
