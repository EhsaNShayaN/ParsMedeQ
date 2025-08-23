import {Directive, inject, OnDestroy} from '@angular/core';
import {UntypedFormGroup} from '@angular/forms';
import {ToastrService} from 'ngx-toastr';
import {BaseComponent} from "../../../base-component";
import {Resource} from "../../../core/models/ResourceResponse";
import {CustomConstants} from '../../../core/constants/custom.constants';
import {BaseResult} from '../../../core/models/BaseResult';

@Directive()
export class BaseResourceComponent extends BaseComponent implements OnDestroy {
  toaster = inject(ToastrService);
  tableId: number;
  sub: any;
  //////////////////////
  myForm!: UntypedFormGroup;
  expDate: any;
  expTime: any;
  pubDate: any;
  editItem?: Resource;
  image?: File;
  file?: File;

  constructor(tableId: number) {
    super();
    this.tableId = tableId;
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
      if (this.editItem) {
        values.id = this.editItem.id;
        if (!this.image) {
          values.image = this.editItem.image;
        }
        if (!this.file) {
          values.fileId = this.editItem.fileId;
        }
      }
      delete values.imagePath;
      delete values.fileId;
      console.log(values);
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
