import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatListModule} from '@angular/material/list';
import {MatCardModule} from '@angular/material/card';
import {MatFormField, MatInput, MatInputModule, MatLabel} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatCard, MatCardContent, MatCardTitle} from '@angular/material/card';
import {MatButton} from '@angular/material/button';
import {CommentForm} from './components/comment-form/comment-form';

@NgModule({
  declarations: [
    CommentForm,
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatSidenavModule,
    MatListModule,
    MatCardModule,
    MatInputModule,
    MatFormFieldModule,
    FormsModule,
    //////////////////////
    MatCard,
    MatCardTitle,
    MatCardContent,
    MatButton,
    MatFormField,
    MatLabel,
    MatInput,
    //////////////////////
  ],
  providers: [],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    //////////////////////
    MatCard,
    MatCardTitle,
    MatCardContent,
    MatButton,
    MatFormField,
    MatLabel,
    MatInput,
    //////////////////////
    CommentForm,
  ],
})
export class SharedModule {
}
