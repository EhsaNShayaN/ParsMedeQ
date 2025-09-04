import {Directive, inject, OnDestroy} from '@angular/core';
import {UntypedFormGroup} from '@angular/forms';
import {ToastrService} from 'ngx-toastr';
import {BaseComponent} from '../../../base-component';
import {Resource} from '../../../core/models/ResourceResponse';
import {CustomConstants} from '../../../core/constants/custom.constants';
import {AddResult, BaseResult} from '../../../core/models/BaseResult';
import {TranslateService} from '@ngx-translate/core';

@Directive()
export class BaseResourceComponent extends BaseComponent implements OnDestroy {
  lang: string;
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
    const translateService = inject(TranslateService);
    this.lang = translateService.getDefaultLang();
    this.tableId = tableId;
  }

  hideSingleLangControls() {
    if (this.lang !== 'fa') {
      const validFields = ['title', 'abstract', 'keywords', 'anchors', 'description'];
      Object.keys(this.myForm.controls).filter(s => !validFields.includes(s)).forEach(key => {
        this.myForm.get(key)?.disable();
      });
    }
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
      this.restApiService.addResource(values, this.image, this.file).subscribe((d: BaseResult<AddResult>) => {
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
