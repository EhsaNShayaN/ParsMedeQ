import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatToolbar, MatToolbarModule} from '@angular/material/toolbar';
import {MatButton, MatButtonModule, MatIconButton} from '@angular/material/button';
import {MatIcon, MatIconModule} from '@angular/material/icon';
import {MatSidenav, MatSidenavContainer, MatSidenavContent, MatSidenavModule} from '@angular/material/sidenav';
import {MatDivider, MatList, MatListItem, MatListModule, MatNavList} from '@angular/material/list';
import {MatCard, MatCardActions, MatCardContent, MatCardModule, MatCardTitle} from '@angular/material/card';
import {MatFormField, MatInput, MatInputModule, MatLabel} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {CommentForm} from './components/comment-form/comment-form';
import {TreeViewComponent} from './tree-view/tree-view.component';
import {MatTooltip} from '@angular/material/tooltip';
import {CurrencyFormatterPipe} from '../core/pipes/currency-formatter.pipe';
import {SafeHtmlPipe} from '../core/pipes/safe-html.pipe';
import {ReplaceUrlSpacesPipe} from '../core/pipes/replace-url-spaces.pipe';
import {DirectivesModule} from '../core/directives/directives.module';
import {LoadMoreComponent} from './load-more/load-more.component';
import {MatProgressSpinner} from '@angular/material/progress-spinner';
import {MatChipListbox, MatChipOption} from '@angular/material/chips';
import {MobileFormatterPipe} from '../core/pipes/mobile-formatter.pipe';
import {CommentsComponent} from './comments/comments.component';
import {DefaultClassDirective, DefaultFlexDirective, DefaultLayoutAlignDirective, DefaultLayoutDirective} from 'ngx-flexible-layout';

@NgModule({
  declarations: [
    CommentForm,
    /////////////////
    CurrencyFormatterPipe,
    SafeHtmlPipe,
    ReplaceUrlSpacesPipe,
    MobileFormatterPipe,
    LoadMoreComponent,
    CommentsComponent,
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    DirectivesModule,
    /////////////////
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
    MatSidenavContainer,
    MatNavList,
    MatSidenav,
    MatToolbar,
    MatIcon,
    MatIconButton,
    MatListItem,
    MatSidenavContent,
    MatDivider,
    MatTooltip,
    MatCardActions,
    MatProgressSpinner,
    MatChipListbox,
    MatChipOption,
    MatList,
    //////////////////////
    DefaultFlexDirective,
    DefaultClassDirective,
    DefaultLayoutDirective,
    DefaultLayoutAlignDirective,
    //////////////////////
    TreeViewComponent,
  ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    DirectivesModule,
    CurrencyFormatterPipe,
    SafeHtmlPipe,
    ReplaceUrlSpacesPipe,
    MobileFormatterPipe,
    LoadMoreComponent,
    CommentsComponent,
    //////////////////////
    MatCard,
    MatCardTitle,
    MatCardContent,
    MatButton,
    MatFormField,
    MatLabel,
    MatInput,
    MatSidenavContainer,
    MatNavList,
    MatSidenav,
    MatToolbar,
    MatIcon,
    MatIconButton,
    MatListItem,
    MatSidenavContent,
    MatDivider,
    MatTooltip,
    MatCardActions,
    MatProgressSpinner,
    MatChipListbox,
    MatChipOption,
    MatList,
    //////////////////////
    DefaultFlexDirective,
    DefaultClassDirective,
    DefaultLayoutDirective,
    DefaultLayoutAlignDirective,
    //////////////////////
    TreeViewComponent,
    CommentForm,
  ],
  providers: [
    CurrencyFormatterPipe,
  ],
})
export class SharedModule {
}
