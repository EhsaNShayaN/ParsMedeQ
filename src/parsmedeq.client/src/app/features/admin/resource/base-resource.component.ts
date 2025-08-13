import {Directive, Injector, OnDestroy} from '@angular/core';
import {UntypedFormGroup} from '@angular/forms';
import {Resource} from '../../../../lib/models/ResourceResponse';
import {ToastrService} from 'ngx-toastr';
import {BaseComponent} from "../../../base-component";
import {BaseResult} from "../../../../lib/models/BaseResult";

@Directive()
export class BaseResourceComponent extends BaseComponent implements OnDestroy {
  tableId: number;
  sub: any;
  //////////////////////
  toaster: ToastrService;
  myForm: UntypedFormGroup;
  expDate: any;
  expTime: any;
  pubDate: any;
  editItem: Resource;
  image: any;
  file: any;
  lang: string;

  constructor(injector: Injector,
              tableId: number) {
    super(injector);
    this.tableId = tableId;
    this.toaster = injector.get(ToastrService);
    this.lang = this.languageService.getLang();
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
      values.languageCode = this.lang;
      this.restClientService.addResource(values, this.image, this.file).subscribe((d: BaseResult<boolean>) => {
        this.toaster.success(this.appService.getTranslateValue('THE_OPERATION_WAS_SUCCESSFUL'), '', {});
      });
    } else {
      console.log(this.findInvalidControls(this.myForm));
    }
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
