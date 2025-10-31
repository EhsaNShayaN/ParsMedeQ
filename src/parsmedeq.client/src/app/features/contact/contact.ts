import {Component} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {AppSettings} from '../../app.settings';
import {Router} from '@angular/router';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.html',
  styleUrl: './contact.scss',
  standalone: false
})
export class Contact {
  form: FormGroup;
  address: string;

  constructor(private fb: FormBuilder,
              protected appSettings: AppSettings) {
    this.form = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      message: ['', Validators.required]
    });
    this.address = appSettings.settings.address.replace('<br>', '');
  }

  onSubmit(): void {
    if (this.form.valid) {
      alert('پیام شما با موفقیت ارسال گردید.');
      this.form.reset();
    }
  }
}
