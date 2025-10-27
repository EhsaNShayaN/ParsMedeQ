import {Component, OnInit} from '@angular/core';
import {BaseComponent} from '../../base-component';
import {UntypedFormBuilder, UntypedFormGroup, Validators} from '@angular/forms';
import {Profile} from '../../core/models/Login';
import {matchingPasswords} from '../../core/utils/app-validators';
import {first} from 'rxjs';
import {AddResult, BaseResult} from '../../core/models/BaseResult';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-user-panel',
  standalone: false,
  templateUrl: './user-panel.html',
  styleUrl: './user-panel.scss'
})
export class UserPanel extends BaseComponent implements OnInit {
  public passwordForm: UntypedFormGroup;
  profile: Profile;

  constructor(private formBuilder: UntypedFormBuilder,
              private toastrService: ToastrService) {
    super();
  }

  ngOnInit() {
    this.restClientService.getUserInfo().subscribe((p: ProfileResponse) => {
      this.profile = p.data;
      this.passwordForm = this.formBuilder.group({
        currentPassword: ['', Validators.required],
        newPassword: ['', Validators.required],
        confirmNewPassword: ['', Validators.required]
      }, {validator: matchingPasswords('newPassword', 'confirmNewPassword')});
    });
  }

  public onPasswordFormSubmit(values: any): void {
    if (this.passwordForm.valid) {
      this.baseRestService.changePassword(values.currentPassword, values.newPassword).pipe(first()).subscribe((d: BaseResult<AddResult>) => {
        if (d.data) {
          const msg = this.getTranslateValue('YOUR_PASSWORD_CHANGED_SUCCESSFULLY');
          this.toastrService.success(msg, '', {});
        } else {
          const msg = this.getTranslateValue('CURRENT_PASSWORD_IS_WRONG');
          this.toastrService.error(msg, '', {});
        }
      });
    }
  }
}
