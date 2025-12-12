import {Component, ViewChild} from '@angular/core';
import {BaseComponent} from '../../base-component';
import {FormGroupDirective, UntypedFormBuilder, UntypedFormGroup, Validators} from '@angular/forms';
import {Profile, ProfileResponse} from '../../core/models/Login';
import {matchingPasswords} from '../../core/utils/app-validators';
import {first, Observable} from 'rxjs';
import {AddResult, BaseResult} from '../../core/models/BaseResult';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-user-panel',
  standalone: false,
  templateUrl: './user-panel.html',
  styleUrl: './user-panel.scss'
})
export class UserPanel extends BaseComponent {
  @ViewChild(FormGroupDirective) formDir!: FormGroupDirective;
  public form!: UntypedFormGroup;
  profile?: Profile;

  constructor(private formBuilder: UntypedFormBuilder,
              private toastrService: ToastrService) {
    super();
    this.restApiService.getUserInfo().subscribe((p: ProfileResponse) => {
      this.profile = p.data;
      this.form = this.formBuilder.group({
        currentPassword: ['', this.profile?.passwordMustBeSet ? null : Validators.required],
        newPassword: ['', Validators.required],
        confirmNewPassword: ['', Validators.required]
      }, {validator: matchingPasswords('newPassword', 'confirmNewPassword')});
    });
  }

  onPasswordFormSubmit(values: any): void {
    if (!this.form.valid) {
      this.form.markAllAsTouched();
      console.log(this.findInvalidControls(this.form));
      return;
    }
    let obs: Observable<any>;
    if (this.profile?.passwordMustBeSet) {
      obs = this.restApiService.setPassword(values.newPassword);
    } else {
      obs = this.restApiService.changePassword(values.currentPassword, values.newPassword);
    }
    obs.pipe(first()).subscribe((d: BaseResult<AddResult>) => {
      console.log(d);
      if (d.data?.changed) {
        this.profile!.passwordMustBeSet = false;
        this.toastrService.success(this.getTranslateValue('YOUR_PASSWORD_CHANGED_SUCCESSFULLY'), '', {});
        this.formDir.resetForm();
      } else {
        if (d.errors?.length > 0) {
          this.toastrService.error(d.errors[0].errorMessage, '', {});
        } else {
          this.toastrService.error(this.getTranslateValue('UNKNOWN_ERROR'), '', {});
        }
      }
    });
  }
}
