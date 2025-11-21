import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {FormBuilder, Validators} from '@angular/forms';
import {Section} from '../homepage-sections.component';
import {SectionService} from '../section.service';

@Component({
  selector: 'edit-main-image-dialog',
  templateUrl: './edit-main-image.dialog.html',
  standalone: false
})
export class EditMainImageDialog {
  preview: string | null = null;
  selectedFile?: File;

  form = this.fb.group({
    title: ['', Validators.required]
  });

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: Section,
    private dialogRef: MatDialogRef<EditMainImageDialog>,
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

    // preview
    const reader = new FileReader();
    reader.onload = () => this.preview = reader.result as string;
    reader.readAsDataURL(this.selectedFile);
  }

  save() {
    const title = this.form.value.title;
    if (this.selectedFile) {
      // upload file first (multipart)
      this.service.uploadImage(this.selectedFile).subscribe(res => {
        const payload: any = {title, imageUrl: res.url};
        this.dialogRef.close(payload);
      });
    } else {
      this.dialogRef.close({title});
    }
  }
}
