import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {Resource} from '../../../core/models/ResourceResponse';
import {endpoint} from '../../../core/services/cookie-utils';
import {DomSanitizer, SafeResourceUrl} from '@angular/platform-browser';

export class ClipDialogModel {
  constructor(public clip: Resource) {
  }
}

@Component({
  selector: 'app-clip-dialog',
  templateUrl: './clip-dialog.component.html',
  styleUrl: './clip-dialog.component.scss',
  standalone: false
})
export class ClipDialogComponent {
  public clip: Resource;
  filePath: SafeResourceUrl | null = null;

  constructor(public dialogRef: MatDialogRef<ClipDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: ClipDialogModel,
              private sanitizer: DomSanitizer) {
    this.clip = data.clip;
    this.filePath = this.getFilePath();
  }

  private getFilePath() {
    if (!this.data.clip.fileId) {
      return null;
    }
    const url = `${endpoint()}general/download?id=${this.clip.fileId}`;
    return this.sanitizer.bypassSecurityTrustResourceUrl(url);
  }

  onDismiss(): void {
    this.dialogRef.close(false);
  }

  onConfirm() {
    this.dialogRef.close(true);
  }
}
