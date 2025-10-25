import {Component, OnDestroy, OnInit} from '@angular/core';
import {BaseComponent} from '../../../../base-component';
import {ActivatedRoute} from '@angular/router';
import {ProductMedia, ProductMediaListResponse} from '../../../../core/models/ProductMediaResponse';
import {AddResult, BaseResult} from '../../../../core/models/BaseResult';
import {ToastrService} from 'ngx-toastr';
import {UntypedFormBuilder, UntypedFormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-product-media-list',
  styleUrls: ['product-media-list.component.scss'],
  templateUrl: 'product-media-list.component.html',
  standalone: false
})
export class ProductMediaListComponent extends BaseComponent implements OnInit, OnDestroy {
  sub: any;
  items: ProductMedia[] = [];
  form!: UntypedFormGroup;
  productId: number = 0;

  constructor(public formBuilder: UntypedFormBuilder,
              private activatedRoute: ActivatedRoute,
              private toastrService: ToastrService) {
    super();
    this.form = this.formBuilder.group({
      gallery: [null, Validators.required]
    });
  }

  ngOnInit() {
    this.sub = this.activatedRoute.params.subscribe(params => {
      this.productId = Number(params['id']);
      this.getItems();
    });
  }

  getItems() {
    this.restApiService.getProductMediaList(this.productId).subscribe((res: ProductMediaListResponse) => {
      this.items = res.data;
    });
  }

  ngOnDestroy() {
    this.sub?.unsubscribe();
  }

  deleteImage(productId: number, mediaId: number) {
    this.restApiService.deleteProductMedia({productId, mediaId}).subscribe((res: BaseResult<AddResult>) => {
      if (res.data.changed) {
        this.toastrService.success(this.getTranslateValue('THE_OPERATION_WAS_SUCCESSFUL'), '', {});
        this.getItems();
      } else {
        this.toastrService.error(this.getTranslateValue('UNKNOWN_ERROR'), '', {});
      }
    });
  }

  onFormSubmit(values: any): void {
    console.log(values);
    if (!this.form.valid) {
      this.form.markAllAsTouched();
      console.log(this.findInvalidControls(this.form));
      return;
    }
    this.restApiService.addProductMedia(this.productId, values.gallery.new).subscribe((res: BaseResult<AddResult>) => {
      if (res.data.changed) {
        this.toastrService.success(this.getTranslateValue('THE_OPERATION_WAS_SUCCESSFUL'), '', {});
        this.form.reset();
        this.form.markAsPristine();
        this.form.markAsUntouched();
        this.getItems();
      } else {
        this.toastrService.error(this.getTranslateValue('UNKNOWN_ERROR'), '', {});
      }
    });
  }
}
