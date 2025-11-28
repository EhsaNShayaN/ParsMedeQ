import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {FormBuilder, Validators} from '@angular/forms';
import {Section} from '../homepage-sections.component';
import {SectionService} from '../section.service';
import {ToastrService} from 'ngx-toastr';
import {BaseSectionDialog} from './base-section.dialog';

@Component({
  selector: 'for-study-dialog',
  templateUrl: './for-study.dialog.html',
  styleUrl: '../homepage-sections.component.scss',
  standalone: false
})
export class ForStudyDialog extends BaseSectionDialog {
  preview: string | null = null;
  selectedFile?: File;

  form = this.fb.group({
    title: ['', Validators.required],
    description: ['', Validators.required],
  });

  constructor(@Inject(MAT_DIALOG_DATA) public data: Section,
              private dialogRef: MatDialogRef<ForStudyDialog>,
              private fb: FormBuilder,
              private service: SectionService,
              private toastrService: ToastrService) {
    super();
    this.form.patchValue({title: data.title || '', description: data.description || ''});
    this.preview = data.image || null;
    this.sectionTitle = this.mainSections.find(s => s.id === data.sectionId).title;
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
    const description = this.form.value.description;
    this.service.update(this.data.sectionId, title ?? '', description ?? '', this.data.image, this.selectedFile).subscribe(res => {
      const payload: any = {title, description, image: res.url};
      this.dialogRef.close(payload);
    });
  }

  deleteImage() {
    this.preview = null;
    this.service.deleteImage(this.data.sectionId).subscribe(res => {
      this.toastrService.success(this.getTranslateValue('THE_OPERATION_WAS_SUCCESSFUL'), '', {});
    });
  }
}
