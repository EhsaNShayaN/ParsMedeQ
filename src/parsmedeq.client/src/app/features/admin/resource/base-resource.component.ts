import {Directive, Injector, OnDestroy} from '@angular/core';
import {UntypedFormGroup} from '@angular/forms';
import {ToastrService} from 'ngx-toastr';
import {BaseComponent} from "../../../base-component";
import {Resource} from "../../../core/models/ResourceResponse";
import {BaseResult} from "../../../core/models/BaseResult";
import {CustomConstants} from "../../../core/constants/custom.constants";

@Directive()
export class BaseResourceComponent extends BaseComponent implements OnDestroy {
    tableId: number;
    sub: any;
    //////////////////////
    toaster: ToastrService;
    myForm!: UntypedFormGroup;
    expDate: any;
    expTime: any;
    pubDate: any;
    editItem?: Resource;
    image: any;
    file: any;

    constructor(injector: Injector,
                tableId: number) {
        super(injector);
        this.tableId = tableId;
        this.toaster = injector.get(ToastrService);
    }

    isAvailable(item: any, filteredProviders: any[]) {
        return filteredProviders.some(s => s === item);
    }

    onFormSubmit(values: any): void {
        if (this.myForm.valid) {
            if (this.expDate) {
                values.expirationDate = this.expDate;
            }
            if (this.pubDate) {
                values.publishDate = this.pubDate;
            }
            if (this.editItem) {
                values.id = this.editItem.id;
                if (!this.image) {
                    values.image = this.editItem.image;
                    values.mimeType = this.editItem.mimeType;
                }
                if (!this.file) {
                    values.doc = this.editItem.doc;
                }
            }
            values.tableId = this.tableId;
            this.restApiService.addResource(values, this.image, this.file).subscribe((d: BaseResult<boolean>) => {
                this.toaster.success(CustomConstants.THE_OPERATION_WAS_SUCCESSFUL, '', {});
            });
        } else {
            console.log(this.findInvalidControls(this.myForm));
        }
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}
