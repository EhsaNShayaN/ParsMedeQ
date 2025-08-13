import {Directive, Injector, OnDestroy, OnInit} from '@angular/core';
import {FormControl, UntypedFormBuilder, UntypedFormGroup, Validators} from '@angular/forms';
import {ActivatedRoute} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {ResourceCategoriesResponse, ResourceCategory} from '../../../core/models/ResourceCategoryResponse';
import {BaseComponent} from '../../../base-component';
import {Tables} from "../../../core/constants/server.constants";
import {BaseResult} from "../../../core/models/BaseResult";
import {CustomConstants} from "../../../core/constants/custom.constants";

@Directive()
export class BaseCategoryComponent extends BaseComponent implements OnInit, OnDestroy {
    tableId: number;
    formBuilder: UntypedFormBuilder;
    toaster: ToastrService;
    public myForm!: UntypedFormGroup;
    resourceCategories: ResourceCategory[] = [];
    editItem?: ResourceCategory;
    /////////////////////
    private sub: any;

    constructor(injector: Injector,
                tableId: number,
                protected activatedRoute: ActivatedRoute) {
        super(injector);
        this.tableId = tableId;
        this.formBuilder = injector.get(UntypedFormBuilder);
        this.toaster = injector.get(ToastrService);
    }

    ngOnInit() {
        this.sub = this.activatedRoute.params.subscribe(params => {
            this.restApiService.getResourceCategories(this.tableId).subscribe((acr: ResourceCategoriesResponse) => {
                this.resourceCategories = acr.resourceCategories;
                this.editItem = this.resourceCategories.find(s => s.id === params['id']);
                if (this.editItem) {
                    this.resourceCategories = this.resourceCategories.filter(s => s.id !== this.editItem?.id);
                }
                this.myForm = this.formBuilder.group({
                    title: [this.editItem?.title, Validators.required],
                    description: this.editItem?.description,
                });
                if (this.tableId !== Tables.Podcast) {
                    this.myForm.addControl('parentId', new FormControl(this.editItem?.parentId));
                }
            });
        });
    }

    onFormSubmit(values: any): void {
        console.log(values);
        if (this.myForm.valid) {
            if (this.editItem) {
                values.id = this.editItem.id;
            }
            values.tableId = this.tableId;
            this.restApiService.addResourceCategory(values).subscribe((d: BaseResult<boolean>) => {
                this.toaster.success(CustomConstants.THE_OPERATION_WAS_SUCCESSFUL, '', {});
            });
        }
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}
