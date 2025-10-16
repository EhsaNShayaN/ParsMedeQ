import {Component, Inject, OnInit} from '@angular/core';
import {MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import {UntypedFormBuilder} from '@angular/forms';
import {BaseDialog} from "../alert-dialog/base-dialog";

export class CustomDialogModel {
  constructor(public title: string, public message: string, public image: string, public dialogData: any, public dialogConfirmed: (id: string, reply: any) => void) {
  }
}

@Component({
  selector: 'app-custom-dialog',
  templateUrl: './custom-dialog.component.html',
  styleUrls: ['./custom-dialog.component.scss']
})
export class CustomDialogComponent extends BaseDialog implements OnInit {
  public title: string;
  public message: string;
  public image: string;
  public dialogData: any;
  public dialogConfirmed: (id: string, reply: any) => void;

  constructor(public dialogRef: MatDialogRef<CustomDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: CustomDialogModel,
              public formBuilder: UntypedFormBuilder) {
    super();
    this.title = data.title;
    this.message = data.message;
    this.image = data.image;
    this.dialogData = data.dialogData;
    this.dialogConfirmed = data.dialogConfirmed;
  }

  ngOnInit(): void {
  }

  onDismiss(): void {
    this.dialogRef.close(false);
  }

  onConfirm() {
    this.dialogRef.close(true);
    this.dialogConfirmed(this.dialogData.id, {});
  }
}
