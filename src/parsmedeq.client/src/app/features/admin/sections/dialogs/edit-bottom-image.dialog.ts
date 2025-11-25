import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {FormBuilder, Validators} from '@angular/forms';
import {SectionService} from '../section.service';
import {Section} from '../homepage-sections.component';
import {PureComponent} from '../../../../pure-component';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'edit-bottom-image-dialog',
  templateUrl: './edit-bottom-image.dialog.html',
  standalone: false
})
export class EditBottomImageDialog extends PureComponent {
  preview: string | null = null;
  selectedFile?: File;

  form = this.fb.group({title: ['', Validators.required]});

  constructor(@Inject(MAT_DIALOG_DATA) public data: Section,
              private dialogRef: MatDialogRef<EditBottomImageDialog>,
              private fb: FormBuilder,
              private service: SectionService,
              private toastrService: ToastrService) {
    super();
    this.form.patchValue({title: data.title || ''});
    this.preview = data.image || null;
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
    this.service.update(this.data.id, title ?? '', this.data.image, this.selectedFile).subscribe(res => {
      this.dialogRef.close({title, image: res.url});
    });
  }

  deleteImage() {
    this.preview = null;
    this.service.deleteImage(this.data.id).subscribe(res => {
      this.toastrService.success(this.getTranslateValue('THE_OPERATION_WAS_SUCCESSFUL'), '', {});
    });
  }
}
