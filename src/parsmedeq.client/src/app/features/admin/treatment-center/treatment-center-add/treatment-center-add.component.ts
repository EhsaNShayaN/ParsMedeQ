import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {FormGroupDirective, UntypedFormBuilder, UntypedFormGroup, Validators} from '@angular/forms';
import {BaseComponent} from '../../../../base-component';
import {AddResult, BaseResult} from '../../../../core/models/BaseResult';
import {Location, LocationResponse} from '../../../../core/models/LocationResponse';
import {TreatmentCenter} from '../../../../core/models/TreatmentCenterResponse';
import {MatSelectChange} from '@angular/material/select';

@Component({
  selector: 'app-treatment-center-add',
  templateUrl: './treatment-center-add.component.html',
  styleUrls: ['./treatment-center-add.component.scss'],
  standalone: false
})
export class TreatmentCenterAddComponent extends BaseComponent implements OnInit, OnDestroy {
  lang: string;
  public myForm!: UntypedFormGroup;
  @ViewChild(FormGroupDirective) formDir!: FormGroupDirective;
  locations: Location[] = [];
  provinces: Location[] = [];
  cities: Location[] = [];
  editItem?: TreatmentCenter;
  /////////////////////
  private sub: any;
  image?: File;
  oldImagePath: string = '';
  pageIndex = 1;
  pageSize = 5;

  constructor(private toastrService: ToastrService,
              protected activatedRoute: ActivatedRoute,
              private formBuilder: UntypedFormBuilder) {
    super();
    this.lang = this.translateService.getDefaultLang();
  }

  ngOnInit() {
    this.sub = this.activatedRoute.params.subscribe(params => {
      this.restApiService.getLocations().subscribe((acr: LocationResponse) => {
        this.locations = acr.data;
        this.provinces = this.locations.filter(s => !s.parentId);
        if (params['id']) {
          this.restApiService.getTreatmentCenter(params['id']).subscribe((t: BaseResult<TreatmentCenter>) => {
            this.editItem = t.data;
            const provinceId = this.locations.find(p => p.id === this.editItem?.locationId)?.parentId;
            this.cities = this.locations.filter(s => s.parentId === provinceId);
            this.createForm(provinceId);
          });
        } else {
          this.createForm(undefined);
        }
      });
    });
  }

  createForm(provinceId: number | undefined) {
    console.log('provinceId', provinceId);
    this.oldImagePath = this.editItem?.image ?? '';
    this.myForm = this.formBuilder.group({
      title: [this.editItem?.title, Validators.required],
      description: this.editItem?.description,
      provinceId: [provinceId, Validators.required],
      cityId: [this.editItem?.locationId, Validators.required],
      imagePath: [null, this.oldImagePath ? null : Validators.required]
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

  provinceChanged($event: MatSelectChange<any>) {
    this.cities = this.locations.filter(s => s.parentId === $event.value);
  }
}
