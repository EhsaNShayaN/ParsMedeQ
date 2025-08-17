import {Directive, Injector, OnDestroy} from '@angular/core';
import {UntypedFormGroup} from '@angular/forms';
import {ToastrService} from 'ngx-toastr';
import {BaseComponent} from "../../../base-component";
import {Resource} from "../../../core/models/ResourceResponse";

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
      values.tableId = this.tableId;
      console.log(values);
      /*if (this.editItem) {
        values.id = this.editItem.id;
        if (!this.image) {
          values.image = this.editItem.image;
        }
        if (!this.file) {
          values.fileId = this.editItem.fileId;
        }
        this.restApiService.editResource(values, this.image, this.file).subscribe((d: BaseResult<boolean>) => {
          this.toaster.success(CustomConstants.THE_OPERATION_WAS_SUCCESSFUL, '', {});
        });
      } else {
        this.restApiService.addResource(values, this.image, this.file).subscribe((d: BaseResult<boolean>) => {
          this.toaster.success(CustomConstants.THE_OPERATION_WAS_SUCCESSFUL, '', {});
        });
      }*/
    } else {
      console.log(this.findInvalidControls(this.myForm));
    }
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
