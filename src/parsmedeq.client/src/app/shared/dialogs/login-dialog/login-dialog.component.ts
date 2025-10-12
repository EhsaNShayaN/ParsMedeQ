import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';

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

  constructor(@Inject(MAT_DIALOG_DATA) public data: LoginDialogModel) {
    this.title = data.title;
    this.message = data.message;
  }
}
