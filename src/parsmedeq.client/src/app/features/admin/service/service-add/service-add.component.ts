import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {FormGroupDirective, UntypedFormBuilder, UntypedFormGroup, Validators} from '@angular/forms';
import {BaseComponent} from '../../../../base-component';
import {AddResult, BaseResult} from '../../../../core/models/BaseResult';
import {TreatmentCenter} from '../../../../core/models/TreatmentCenterResponse';

@Component({
  selector: 'app-service-add',
  templateUrl: './service-add.component.html',
  styleUrls: ['./service-add.component.scss'],
  standalone: false
})
export class ServiceAddComponent extends BaseComponent implements OnInit, OnDestroy {
  lang: string;
  public myForm!: UntypedFormGroup;
  @ViewChild(FormGroupDirective) formDir!: FormGroupDirective;
  editItem?: TreatmentCenter;
  /////////////////////
  private sub: any;
  image?: File;
  oldImagePath: string = '';

  constructor(private toastrService: ToastrService,
              protected activatedRoute: ActivatedRoute,
              private formBuilder: UntypedFormBuilder) {
    super();
    this.lang = this.translateService.getDefaultLang();
  }

  ngOnInit() {
    this.sub = this.activatedRoute.params.subscribe(params => {
      if (params['id']) {
        this.restApiService.getTreatmentCenter(params['id']).subscribe((t: BaseResult<TreatmentCenter>) => {
          this.editItem = t.data;
          this.createForm();
        });
      } else {
        this.createForm();
      }
    });
  }

  createForm() {
    this.oldImagePath = this.editItem?.image ?? '';
    this.myForm = this.formBuilder.group({
      title: [this.editItem?.title, Validators.required],
      description: this.editItem?.description,
      imagePath: null
    });
    if (this.editItem) {
      this.hideSingleLangControls();
    }
  }

  handleImageInput(target: any) {
    if (target.files && target.files[0]) {
      this.image = target.files[0];
    }
  }

  deleteImage() {
    this.oldImagePath = '';
  }

  hideSingleLangControls() {
    if (this.lang !== 'fa') {
      const validFields = ['imagePath', 'title', 'description'];
      Object.keys(this.myForm.controls).filter(s => !validFields.includes(s)).forEach(key => {
        this.myForm.get(key)?.disable();
      });
    }
  }

  onFormSubmit(values: any): void {
    if (!this.myForm.valid) {
      this.myForm.markAllAsTouched();
      console.log(this.findInvalidControls(this.myForm));
      return;
    }
    if (this.editItem) {
      values.id = this.editItem.id;
      if (!this.image) {
        values.oldImagePath = this.oldImagePath;
      }
    }
    delete values.imagePath;
    this.restApiService.addTreatmentCenter(values, this.image).subscribe((d: BaseResult<AddResult>) => {
      if (d.data.changed) {
        this.toastrService.success(this.getTranslateValue('THE_OPERATION_WAS_SUCCESSFUL'), '', {});
        if (!this.editItem) {
          this.formDir.resetForm();
        }
      }
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
