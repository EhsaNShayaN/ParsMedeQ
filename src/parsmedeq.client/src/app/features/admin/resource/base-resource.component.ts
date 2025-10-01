import {Directive, inject, OnDestroy, ViewChild} from '@angular/core';
import {FormArray, FormGroupDirective, UntypedFormArray, UntypedFormBuilder, UntypedFormGroup} from '@angular/forms';
import {ToastrService} from 'ngx-toastr';
import {BaseComponent} from '../../../base-component';
import {Resource} from '../../../core/models/ResourceResponse';
import {AddResult, BaseResult} from '../../../core/models/BaseResult';
import {TranslateService} from '@ngx-translate/core';
import * as uuid from 'uuid';
import * as moment from 'jalali-moment';

@Directive()
export class BaseResourceComponent extends BaseComponent implements OnDestroy {
  lang: string;
  formBuilder = inject(UntypedFormBuilder);
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
  abstractError = false;

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

  get anchorsArray() {
    return this.myForm.get('anchors') as FormArray;
  }

  createAnchor(): UntypedFormGroup {
    return this.formBuilder.group({
      id: uuid.v4().replace(/-/g, ''),
      name: null,
      desc: null,
    });
  }

  addAnchor(): void {
    const anchors = this.myForm.controls['anchors'] as UntypedFormArray;
    anchors.push(this.createAnchor());
  }

  deleteAnchor(index: number) {
    const anchors = this.myForm.controls['anchors'] as UntypedFormArray;
    anchors.removeAt(index);
  }

  handleImageInput(target: any) {
    if (target.files && target.files[0]) {
      this.image = target.files[0];
    }
  }

  handleFileInput(target: any) {
    if (target.files && target.files[0]) {
      this.file = target.files[0];
    }
  }

  toEnglish(s: string) {
    let x = s.replace(/[۰-۹]/g, d => '۰۱۲۳۴۵۶۷۸۹'.indexOf(d).toString());
    x = x.replace(/[٠-٩]/g, d => '٠١٢٣٤٥٦٧٨٩'.indexOf(d).toString());
    return x;
  }

  dateChanged(dateRangeStart: HTMLInputElement) {
    this.expDate = this.toEnglish(dateRangeStart.value);
    const startDate = this.toGeorgianDate();
  }

  toGeorgianDate() {
    const start = moment.from(this.expDate, 'fa', 'YYYY/MM/DD').locale('en').format('YYYY/MM/DD');
    const yearStart = Number(start.substring(0, 4));
    const monthStart = Number(start.substring(5, 7));
    const dayStart = Number(start.substring(8, 10));
    const startDate = new Date(yearStart, monthStart - 1, dayStart);
    return startDate;
  }

  leaveAbstract() {
    const x = this.myForm.controls['abstract'].value;
    this.abstractError = !x;
  }

  editAnchors() {
    const anchors = this.myForm.controls['anchors'] as UntypedFormArray;
    while (anchors.length) {
      anchors.removeAt(0);
    }
    this.editItem?.anchors?.forEach(plan => {
      let p = {
        id: plan.id,
        name: plan.name,
        desc: plan.desc,
      };
      anchors.push(this.formBuilder.group(p));
    });
  }

  deleteImage() {
    this.oldImagePath = '';
  }

  deleteFile() {
    this.oldFileId = 0;
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
