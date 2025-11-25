import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {FormBuilder, FormArray, FormControl, Validators} from '@angular/forms';
import {Section} from '../homepage-sections.component';
import {PureComponent} from '../../../../pure-component';

@Component({
  selector: 'edit-advantages-dialog',
  templateUrl: './edit-advantages.dialog.html',
  styleUrl: '../homepage-sections.component.scss',
  standalone: false
})
export class EditAdvantagesDialog extends PureComponent {
  form = this.fb.group({
    title: ['', Validators.required],
    items: this.fb.array([])
  });

  constructor(@Inject(MAT_DIALOG_DATA) public data: Section,
              private dialogRef: MatDialogRef<EditAdvantagesDialog>,
              private fb: FormBuilder) {
    super();
    this.form.patchValue({title: data.title || ''});
    const arr = this.form.get('items') as FormArray;
    (data.items || []).forEach((it: any) => arr.push(this.fb.group({text: [it.text || '', Validators.required], icon: [it.icon || '']})));
  }

  get items() {
    return this.form.get('items') as FormArray;
  }

  addItem() {
    this.items.push(this.fb.group({text: ['', Validators.required], icon: ['check_circle']}));
  }

  removeItem(i: number) {
    this.items.removeAt(i);
  }

  save() {
    this.dialogRef.close(this.form.value);
  }
}
