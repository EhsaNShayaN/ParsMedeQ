import {Directive, inject, OnDestroy, ViewChild} from '@angular/core';
import {FormGroupDirective, UntypedFormGroup} from '@angular/forms';
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
  @ViewChild(FormGroupDirective) formDir!: FormGroupDirective;
  expDate: any;
  expTime: any;
  pubDate: any;
  editItem?: Resource;
  oldImagePath: string = '';
  oldFileId: number = 0;
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
    if (!this.myForm.valid) {
      console.log(this.findInvalidControls(this.myForm));
      return;
    }
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
        values.oldImagePath = this.oldImagePath;
      }
      if (!this.file) {
        values.oldFileId = this.oldFileId;
      }
    }
    delete values.imagePath;
    delete values.fileId;
    this.restApiService.addResource(values, this.image, this.file).subscribe((d: BaseResult<AddResult>) => {
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
