import {Injectable} from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {ConfirmDialogComponent, ConfirmDialogModel} from '../../shared/dialogs/confirm-dialog/confirm-dialog.component';
import {LoginDialogComponent, LoginDialogModel} from '../../shared/dialogs/login-dialog/login-dialog.component';
import {CustomDialogComponent, CustomDialogModel} from '../../shared/dialogs/custom-dialog/custom-dialog.component';
import {Resource} from '../models/ResourceResponse';
import {ClipDialogComponent, ClipDialogModel} from '../../shared/dialogs/clip-dialog/clip-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class DialogService {
  constructor(public dialog: MatDialog) {
  }

  public openCustomDialog(title: string, description: string, image: string) {
    const dialogData = new CustomDialogModel(title, description, image);
    return this.dialog.open(CustomDialogComponent, {
      maxWidth: '600px',
      data: dialogData
    });
  }

  public openConfirmDialog(title: string, message: string) {
    const dialogData = new ConfirmDialogModel(title, message);
    return this.dialog.open(ConfirmDialogComponent, {
      maxWidth: '400px',
      data: dialogData
    });
  }

  public openLoginDialog(title: string, message: string) {
    const dialogData = new LoginDialogModel(title, message);
    return this.dialog.open(LoginDialogComponent, {
      maxWidth: '500px',
      data: dialogData
    });
  }

  public openClipDialog(clip: Resource) {
    const dialogData = new ClipDialogModel(clip);
    return this.dialog.open(ClipDialogComponent, {
      maxWidth: '600px',
      data: dialogData
    });
  }

  /*public openCustomConfirmDialog(title: string, message: string, data: any, dialogConfirmed: (data: any) => void) {
    const dialogData = new CustomConfirmDialogModel(title, message, data, dialogConfirmed);
    return this.dialog.open(CustomConfirmDialogComponent, {
      maxWidth: '400px',
      data: dialogData
    });
  }

  public openFormDialog(title: string, message: string, data: any, dialogConfirmed: (id: string, reply: any) => void) {
    const dialogData = new FormDialogModel(title, message, data, dialogConfirmed);
    return this.dialog.open(FormDialogComponent, {
      maxWidth: '620px',
      data: dialogData
    });
  }

  public openAlertDialog(message: string) {
    return this.dialog.open(AlertDialogComponent, {
      maxWidth: '400px',
      data: message
    });
  }

  public openBasketDialog(message: string) {
    return this.dialog.open(BasketDialogComponent, {
      maxWidth: '400px',
      data: message
    });
  }

  public openFactoryMapDialog(factories: any[], dialogConfirmed: (factory: any) => void) {
    const dialogData = new FactoryMapDialogModel(factories, dialogConfirmed);
    return this.dialog.open(FactoryMapDialogComponent, {
      width: '1200px',
      maxWidth: '1200px',
      data: dialogData
    });
  }

  public openFactoryGravelDialog(items: any[]) {
    const dialogData = new FactoryGravelDialogModel(items);
    return this.dialog.open(FactoryGravelDialogComponent, {
      width: '1200px',
      maxWidth: '1200px',
      data: dialogData
    });
  }*/
}
