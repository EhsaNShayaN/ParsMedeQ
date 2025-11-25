import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {FormBuilder, Validators} from '@angular/forms';
import {Section} from '../homepage-sections.component';
import {PureComponent} from '../../../../pure-component';

@Component({
  selector: 'edit-text-dialog',
  templateUrl: './edit-about.dialog.html',
  styleUrl: '../homepage-sections.component.scss',
  standalone: false
})
export class EditAboutDialog extends PureComponent {
  form = this.fb.group({text: ['', Validators.required]});

  constructor(@Inject(MAT_DIALOG_DATA) public data: Section,
              private fb: FormBuilder,
              private dialogRef: MatDialogRef<EditAboutDialog>) {
    super();
    this.form.patchValue({text: data.description || ''});
  }

  save() {
    this.dialogRef.close({description: this.form.value.text});
  }
}
