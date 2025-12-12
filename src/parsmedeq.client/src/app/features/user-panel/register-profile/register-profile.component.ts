import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import {UntypedFormGroup, UntypedFormBuilder, Validators} from '@angular/forms';
import {MatSnackBar} from '@angular/material/snack-bar';
import {emailValidator} from '../../../core/utils/app-validators';
import {Profile} from '../../../core/models/Login';
import {PureComponent} from '../../../pure-component';
import {first} from 'rxjs';
import {AddResult, BaseResult} from '../../../core/models/BaseResult';

@Component({
  selector: 'app-register-profile',
  templateUrl: './register-profile.component.html',
  styleUrls: ['./register-profile.component.scss'],
  standalone: false
})
export class RegisterProfileComponent extends PureComponent implements OnInit, OnDestroy {
  @Input() profile?: Profile;
  @Output() onSaved = new EventEmitter<any>();
  private sub: any;
  public myForm?: UntypedFormGroup;
  public hide = true;
  error = '';

  constructor(public fb: UntypedFormBuilder,
              public snackBar: MatSnackBar) {
    super();
  }

  ngOnInit() {
    this.myForm = this.fb.group({
      email: [this.profile?.email, Validators.compose([Validators.required, emailValidator])],
      firstName: [this.profile?.firstName, Validators.required],
      lastName: [this.profile?.lastName, Validators.required],
      nationalCode: [this.profile?.nationalCode, Validators.compose([Validators.required, Validators.minLength(10), Validators.maxLength(10)])],
    });
  }


  public onRegisterFormSubmit(values: any): void {
    this.error = '';
    if (this.myForm?.valid) {
      if (this.profile) {
        values.mobile = this.profile.mobile;
      }
      this.restApiService.updateProfile(values).pipe(first()).subscribe((d: BaseResult<AddResult>) => {
        if (!d) {
          this.error = this.getTranslateValue('UNKNOWN_ERROR');
          return;
        }
        const msg = this.getTranslateValue('THE_OPERATION_WAS_SUCCESSFUL');
        this.snackBar.open(msg, '×', {
          panelClass: 'success',
          verticalPosition: 'top',
          duration: 3000
        });
        this.onSaved.next(null);
      });
    }
  }

  ngOnDestroy() {
    this.sub?.unsubscribe();
  }
}
