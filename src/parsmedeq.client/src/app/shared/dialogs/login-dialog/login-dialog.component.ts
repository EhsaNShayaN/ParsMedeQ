import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {AuthService} from '../../../core/services/auth.service';

export class LoginDialogModel {
  constructor(public title: string, public message: string) {
  }
}

@Component({
  selector: 'app-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrl: './login-dialog.component.scss',
  standalone: false
})
export class LoginDialogComponent {
  public title: string;
  public message: string;

  constructor(public dialogRef: MatDialogRef<LoginDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: LoginDialogModel,
              private authService: AuthService) {
    this.title = data.title;
    this.message = data.message;

    this.authService.login$.subscribe(isLogin => {
      if (isLogin) {
        this.dialogRef.close(true);
      }
    });
  }
}
