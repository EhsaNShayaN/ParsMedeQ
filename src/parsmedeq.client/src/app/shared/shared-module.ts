import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatListModule} from '@angular/material/list';
import {MatCardModule} from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatCard, MatCardContent, MatCardTitle} from '@angular/material/card';
import {MatButton} from '@angular/material/button';

@NgModule({
  declarations: [],
  imports: [
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
    //////////////////////
  ],
  providers: [],
  exports: [
    //////////////////////
    MatCard,
    MatCardTitle,
    MatCardContent,
    MatButton,],
})
export class SharedModule {
}
