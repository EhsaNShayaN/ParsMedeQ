import {Directive, Injector, OnDestroy, OnInit} from '@angular/core';
import {FormControl, UntypedFormBuilder, UntypedFormGroup, Validators} from '@angular/forms';
import {BaseResult} from '../../../../lib/models/BaseResult';
import {ActivatedRoute} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {ResourceCategoriesResponse, ResourceCategory} from '../../../../lib/models/ResourceCategoryResponse';
import {BaseComponent} from '../../../base-component';
import {Tables} from '../../../../lib/core/constants/server.constants';

@Directive()
export class BaseCategoryComponent extends BaseComponent implements OnInit, OnDestroy {
  tableId: number;
  formBuilder: UntypedFormBuilder;
  toaster: ToastrService;
  public myForm: UntypedFormGroup;
  resourceCategories = [];
  editItem: ResourceCategory;
  lang: string;
  /////////////////////
  private sub: any;

  constructor(injector: Injector,
              tableId: number,
              protected activatedRoute: ActivatedRoute) {
    super(injector);
    this.tableId = tableId;
    this.formBuilder = injector.get(UntypedFormBuilder);
    this.toaster = injector.get(ToastrService);
    this.lang = this.languageService.getLang();
  }

  ngOnInit() {
    this.sub = this.activatedRoute.params.subscribe(params => {
      this.restClientService.getResourceCategories(this.tableId).subscribe((acr: ResourceCategoriesResponse) => {
        this.resourceCategories = acr.resourceCategories;
        this.editItem = this.resourceCategories.find(s => s.id === params.id);
        if (this.editItem) {
          this.resourceCategories = this.resourceCategories.filter(s => s.id !== this.editItem?.id);
        }
        this.myForm = this.formBuilder.group({
          title: [this.editItem?.title, Validators.required],
          //parentId: this.editItem?.parentId,
          description: this.editItem?.description,
        });
        if (this.tableId !== Tables.Podcast) {
          this.myForm.addControl('parentId', new FormControl(this.editItem?.parentId));
        }
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
    console.log(values);
    if (this.myForm.valid) {
      if (this.editItem) {
        values.id = this.editItem.id;
      }
      values.tableId = this.tableId;
      values.languageCode = this.lang;
      this.restClientService.addResourceCategory(values).subscribe((d: BaseResult<boolean>) => {
        this.toaster.success(this.appService.getTranslateValue('THE_OPERATION_WAS_SUCCESSFUL'), '', {});
      });
    }
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
