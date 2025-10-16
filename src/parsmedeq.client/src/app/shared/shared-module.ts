import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatToolbar, MatToolbarModule} from '@angular/material/toolbar';
import {MatButton, MatButtonModule, MatIconButton} from '@angular/material/button';
import {MatIcon, MatIconModule} from '@angular/material/icon';
import {MatSidenav, MatSidenavContainer, MatSidenavContent, MatSidenavModule} from '@angular/material/sidenav';
import {MatDivider, MatList, MatListItem, MatListModule, MatNavList} from '@angular/material/list';
import {MatCard, MatCardActions, MatCardContent, MatCardImage, MatCardModule, MatCardTitle} from '@angular/material/card';
import {MatFormField, MatInput, MatInputModule, MatLabel} from '@angular/material/input';
import {MatError, MatFormFieldModule} from '@angular/material/form-field';
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
import {Comments} from './comments/comments';
import {DefaultClassDirective, DefaultFlexDirective, DefaultLayoutAlignDirective, DefaultLayoutDirective, DefaultShowHideDirective} from 'ngx-flexible-layout';
import {ToastrModule} from 'ngx-toastr';
import {MatMenu, MatMenuItem, MatMenuTrigger} from '@angular/material/menu';
import {TranslateModule} from '@ngx-translate/core';
import {LangPackPipe} from '../core/pipes/lang-pack.pipe';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE, MatNativeDateModule, provideNativeDateAdapter} from '@angular/material/core';
import {JalaliMomentDateAdapter, PERSIAN_DATE_FORMATS} from '../core/custom-date-adapter';
import {DatePickerComponent} from './date-picker/date-picker.component';
import {MultiFileUploadComponent} from './components/multi-file-upload/multi-file-upload.component';
import {CdkDrag, CdkDropList} from '@angular/cdk/drag-drop';
import {CartItemControlComponent} from '../features/cart/cart-item-control/cart-item-control.component';
import {ConfirmDialogComponent} from './dialogs/confirm-dialog/confirm-dialog.component';
import {MatDialogActions, MatDialogContent, MatDialogTitle} from '@angular/material/dialog';
import {CommonModule} from '@angular/common';
import {TreeCategoriesComponent} from './tree-categories/tree-categories.component';
import {SwiperModule} from '../theme/components/swiper/swiper.module';
import {HeaderCarouselComponent} from './header-carousel/header-carousel.component';
import {DownloadManagementComponent} from './components/download-management/download-management.component';
import {OurArticleComponent} from '../features/resources/articles/our-article/our-article.component';
import {RouterLink} from '@angular/router';
import {OurNewsComponent} from '../features/resources/news/our-news/our-news.component';
import {OurclipComponent} from '../features/resources/clips/our-clip/our-clip.component';
import {OurProductComponent} from '../features/products/our-product/our-product.component';
import {HeaderImageComponent} from './header-image/header-image.component';
import {Login} from '../features/auth/login/login';
import {LoginDialogComponent} from './dialogs/login-dialog/login-dialog.component';
import {ClientsComponent} from './clients/clients.component';
import {CustomDialogComponent} from './dialogs/custom-dialog/custom-dialog.component';
import {MatBadge} from '@angular/material/badge';

@NgModule({
  declarations: [
    Login,
    CommentForm,
    DatePickerComponent,
    /////////////////
    LangPackPipe,
    CurrencyFormatterPipe,
    SafeHtmlPipe,
    ReplaceUrlSpacesPipe,
    MobileFormatterPipe,
    LoadMoreComponent,
    Comments,
    MultiFileUploadComponent,
    CartItemControlComponent,
    ConfirmDialogComponent,
    CustomDialogComponent,
    LoginDialogComponent,
    TreeCategoriesComponent,
    HeaderCarouselComponent,
    DownloadManagementComponent,
    HeaderImageComponent,
    OurArticleComponent,
    OurNewsComponent,
    OurclipComponent,
    OurProductComponent,
    ClientsComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    DirectivesModule,
    ToastrModule.forRoot(),
    TranslateModule,
    MatDatepickerModule,
    MatNativeDateModule,
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
    CdkDrag,
    CdkDropList,
    //////////////////////
    DefaultShowHideDirective,
    MatMenuTrigger,
    MatMenu,
    MatMenuItem,
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
    MatError,
    MatCardImage,
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    RouterLink,
    MatBadge,
    //////////////////////
    DefaultFlexDirective,
    DefaultClassDirective,
    DefaultLayoutDirective,
    DefaultLayoutAlignDirective,
    //////////////////////
    TreeViewComponent,
    SwiperModule,
  ],
  exports: [
    FormsModule,
    CdkDrag,
    CdkDropList,
    ReactiveFormsModule,
    DirectivesModule,
    LangPackPipe,
    CurrencyFormatterPipe,
    SafeHtmlPipe,
    ReplaceUrlSpacesPipe,
    MobileFormatterPipe,
    LoadMoreComponent,
    Comments,
    TranslateModule,
    MatDatepickerModule,
    MatNativeDateModule,
    DatePickerComponent,
    CartItemControlComponent,
    //////////////////////
    DefaultShowHideDirective,
    MatMenuTrigger,
    MatMenu,
    MatMenuItem,
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
    MatError,
    MatCardImage,
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    RouterLink,
    MatBadge,
    //////////////////////
    DefaultFlexDirective,
    DefaultClassDirective,
    DefaultLayoutDirective,
    DefaultLayoutAlignDirective,
    //////////////////////
    TreeViewComponent,
    CommentForm,
    MultiFileUploadComponent,
    ConfirmDialogComponent,
    CustomDialogComponent,
    LoginDialogComponent,
    TreeCategoriesComponent,
    SwiperModule,
    HeaderCarouselComponent,
    DownloadManagementComponent,
    HeaderImageComponent,
    OurArticleComponent,
    OurNewsComponent,
    OurclipComponent,
    OurProductComponent,
    ClientsComponent,
  ],
  providers: [
    CurrencyFormatterPipe,
    provideNativeDateAdapter(), // 👈 required
    {provide: MAT_DATE_LOCALE, useValue: 'fa-IR'},
    {provide: DateAdapter, useClass: JalaliMomentDateAdapter, deps: [MAT_DATE_LOCALE]},
    {provide: MAT_DATE_FORMATS, useValue: PERSIAN_DATE_FORMATS}
  ],
})
export class SharedModule {
}
