import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {FormBuilder, FormArray, FormControl, Validators} from '@angular/forms';
import {Section} from '../homepage-sections.component';
import {PureComponent} from '../../../../pure-component';

@Component({
  selector: 'edit-services-dialog',
  templateUrl: './edit-services.dialog.html',
  styleUrl: '../homepage-sections.component.scss',
  standalone: false
})
export class EditServicesDialog extends PureComponent {
  form = this.fb.group({
    title: ['', Validators.required],
    description: [''],
    items: this.fb.array<string>([])
  });

  constructor(@Inject(MAT_DIALOG_DATA) public data: Section,
              private dialogRef: MatDialogRef<EditServicesDialog>,
              private fb: FormBuilder) {
    super();
    this.form.patchValue({title: data.title || '', description: data.description || ''});

    const arr = this.form.get('items') as FormArray;
    (data.items || []).forEach(it => arr.push(this.fb.control(it, Validators.required)));
  }

  get items() {
    return this.form.get('items') as FormArray;
  }

  get itemControls(): FormControl[] {
    return this.items.controls as FormControl[];
  }

  addItem() {
    this.items.push(this.fb.control('', Validators.required));
  }

  removeItem(i: number) {
    this.items.removeAt(i);
  }

  save() {
    this.dialogRef.close(this.form.value);
  }
}
