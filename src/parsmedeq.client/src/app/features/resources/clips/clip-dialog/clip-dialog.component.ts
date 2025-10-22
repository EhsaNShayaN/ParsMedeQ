import {Component, Inject, OnDestroy, AfterViewInit, ViewChild, ElementRef} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {Clip} from '../models/clip.model';

@Component({
  selector: 'app-clip-dialog',
  templateUrl: './clip-dialog.component.html',
  styleUrls: ['./clip-dialog.component.scss']
})
export class ClipDialogComponent implements AfterViewInit, OnDestroy {
  @ViewChild('videoPlayer') videoPlayer!: ElementRef<HTMLVideoElement>;

  constructor(
    public dialogRef: MatDialogRef<ClipDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { clip: Clip }
  ) {
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
