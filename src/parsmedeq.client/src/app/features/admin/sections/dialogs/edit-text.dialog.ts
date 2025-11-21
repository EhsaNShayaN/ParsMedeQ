import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {FormBuilder, Validators} from '@angular/forms';
import {Section} from '../homepage-sections.component';

@Component({
  selector: 'edit-text-dialog',
  templateUrl: './edit-text.dialog.html',
  standalone: false
})
export class EditTextDialog {
  form = this.fb.group({text: ['', Validators.required]});

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: Section,
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<EditTextDialog>
  ) {
    this.form.patchValue({text: data.description || ''});
  }

  save() {
    this.dialogRef.close({description: this.form.value.text});
  }
}
