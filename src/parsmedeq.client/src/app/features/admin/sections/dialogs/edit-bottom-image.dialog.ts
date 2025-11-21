import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {FormBuilder, Validators} from '@angular/forms';
import {SectionService} from '../section.service';
import {Section} from '../homepage-sections.component';

@Component({
  selector: 'edit-bottom-image-dialog',
  templateUrl: './edit-bottom-image.dialog.html',
  standalone: false
})
export class EditBottomImageDialog {
  preview: string | null = null;
  selectedFile?: File;

  form = this.fb.group({title: ['', Validators.required]});

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: Section,
    private dialogRef: MatDialogRef<EditBottomImageDialog>,
    private fb: FormBuilder,
    private service: SectionService
  ) {
    this.form.patchValue({title: data.title || ''});
    this.preview = data.imageUrl || null;
  }

  onFileSelected(e: Event) {
    const input = e.target as HTMLInputElement;
    if (!input.files || input.files.length === 0) return;
    this.selectedFile = input.files[0];
    const reader = new FileReader();
    reader.onload = () => this.preview = reader.result as string;
    reader.readAsDataURL(this.selectedFile);
  }

  save() {
    const title = this.form.value.title;
    if (this.selectedFile) {
      this.service.uploadImage(this.selectedFile).subscribe(res => {
        this.dialogRef.close({title, imageUrl: res.url});
      });
    } else {
      this.dialogRef.close({title});
    }
  }
}
