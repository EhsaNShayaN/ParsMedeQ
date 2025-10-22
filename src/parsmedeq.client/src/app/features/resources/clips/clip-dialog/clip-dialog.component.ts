import {AfterViewInit, Component, ElementRef, Inject, OnDestroy, ViewChild} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {endpoint} from '../../../../core/services/cookie-utils';
import {DomSanitizer} from '@angular/platform-browser';
import {Resource} from '../../../../core/models/ResourceResponse';

@Component({
  selector: 'app-clip-dialog',
  templateUrl: './clip-dialog.component.html',
  styleUrls: ['./clip-dialog.component.scss'],
  standalone: false
})
export class ClipDialogComponent implements AfterViewInit, OnDestroy {
  @ViewChild('videoPlayer') videoPlayer!: ElementRef<HTMLVideoElement>;

  constructor(public dialogRef: MatDialogRef<ClipDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: { clip: Resource },
              private sanitizer: DomSanitizer) {
  }

  getFilePath(videoId?: number) {
    if (!videoId) {
      return null;
    }
    const url = `${endpoint()}general/download?id=${videoId}`;
    return this.sanitizer.bypassSecurityTrustResourceUrl(url);
  }

  ngAfterViewInit() {
    // autoplay when opened (muted autoplay to avoid browser block)
    try {
      const video = this.videoPlayer.nativeElement;
      video.muted = false;
      video.play().catch(() => { /* ignore autoplay blocking */
      });
    } catch {
    }
  }

  close() {
    try {
      this.videoPlayer.nativeElement.pause();
    } catch {
    }
    this.dialogRef.close();
  }

  ngOnDestroy() {
    try {
      this.videoPlayer.nativeElement.pause();
    } catch {
    }
  }
}
