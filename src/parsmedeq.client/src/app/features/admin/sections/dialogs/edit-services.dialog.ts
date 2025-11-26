import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {FormArray, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Section} from '../homepage-sections.component';
import {BaseSectionDialog} from './base-section.dialog';
import {SectionService, UpdateListItem, UpdateListRequest} from '../section.service';

@Component({
  selector: 'edit-services-dialog',
  templateUrl: './edit-services.dialog.html',
  styleUrl: '../homepage-sections.component.scss',
  standalone: false
})
export class EditServicesDialog extends BaseSectionDialog {
  form = this.fb.group({
    items: this.fb.array<FormGroup>([])
  });

  constructor(@Inject(MAT_DIALOG_DATA) public data: Section[],
              private dialogRef: MatDialogRef<EditServicesDialog>,
              private fb: FormBuilder,
              private service: SectionService) {
    super();

    const arr = this.form.get('items') as FormArray<FormGroup>;
    (data || []).forEach(it => arr.push(this.createItemGroup(it)));
    this.sectionTitle = this.mainSections.find(s => s.id === data[0].sectionId)?.title || '';
  }

  get items() {
    return this.form.get('items') as FormArray;
  }

  get itemControls(): FormGroup[] {
    return this.items.controls as FormGroup[];
  }

  addItem() {
    this.items.push(this.createItemGroup());
  }

  removeItem(i: number) {
    this.items.removeAt(i);
  }

  save() {
    console.log(this.form.value.items);
    this.dialogRef.close(this.form.value);

    const items: UpdateListItem[] = this.form.value.items!.map(x => ({
      title: x.title ?? '',
      description: x.description ?? '',
      image: x.image ?? ''
    }));

    const model: UpdateListRequest = {id: this.data[0].sectionId, items};
    this.service.updateByList(model).subscribe(res => {
      this.dialogRef.close(model);
    });
  }

  private createItemGroup(item?: Section) {
    return this.fb.group({
      title: [item?.title || '', Validators.required],
      description: [item?.description || ''],
      image: [item?.image || ''],
    });
  }
}
