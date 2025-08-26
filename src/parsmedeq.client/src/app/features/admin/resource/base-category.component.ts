import {Directive, inject, OnDestroy, OnInit} from '@angular/core';
import {UntypedFormBuilder, UntypedFormGroup, Validators} from '@angular/forms';
import {ActivatedRoute} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {ResourceCategoriesResponse, ResourceCategory} from '../../../core/models/ResourceCategoryResponse';
import {BaseComponent} from '../../../base-component';
import {BaseResult} from '../../../core/models/BaseResult';
import {CustomConstants} from '../../../core/constants/custom.constants';

@Directive()
export class BaseCategoryComponent extends BaseComponent implements OnInit, OnDestroy {
  tableId: number;
  toaster = inject(ToastrService);
  formBuilder = inject(UntypedFormBuilder);
  public myForm!: UntypedFormGroup;
  resourceCategories: ResourceCategory[] = [];
  editItem?: ResourceCategory;
  /////////////////////
  private sub: any;

  constructor(tableId: number,
              protected activatedRoute: ActivatedRoute) {
    super();
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
        });
      });
    });
  }

  onFormSubmit(values: any): void {
    if (this.myForm.valid) {
      if (this.editItem) {
        values.id = this.editItem.id;
      }
      if (this.editItem) {
        this.restApiService.editResourceCategory(values).subscribe((d: BaseResult<boolean>) => {
          this.toaster.success(CustomConstants.THE_OPERATION_WAS_SUCCESSFUL, '', {});
        });
      } else {
        values.tableId = this.tableId;
        this.restApiService.addResourceCategory(values).subscribe((d: BaseResult<boolean>) => {
          this.toaster.success(CustomConstants.THE_OPERATION_WAS_SUCCESSFUL, '', {});
        });
      }
    }
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
