import {Component, Inject, OnInit} from '@angular/core';
import {MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

export class CustomDialogModel {
  constructor(public title: string, public description: string, public image: string) {
  }
}

@Component({
  selector: 'app-custom-dialog',
  templateUrl: './custom-dialog.component.html',
  styleUrl: './custom-dialog.component.scss',
  standalone: false
})
export class CustomDialogComponent implements OnInit {
  public title: string;
  public description: string;
  public image: string;

  constructor(public dialogRef: MatDialogRef<CustomDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: CustomDialogModel) {
    this.title = data.title;
    this.description = data.description;
    this.image = data.image;
  }

  ngOnInit(): void {
  }

  onDismiss(): void {
    this.dialogRef.close(false);
  }

  onConfirm() {
    this.dialogRef.close(true);
  }
}
