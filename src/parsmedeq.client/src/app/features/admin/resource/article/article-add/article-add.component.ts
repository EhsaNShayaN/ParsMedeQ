import {Component, ElementRef, Injector, OnInit, ViewChild} from '@angular/core';
import {FormControl, UntypedFormArray, UntypedFormBuilder, UntypedFormGroup, Validators} from '@angular/forms';
import {getCustomEditorConfigs} from '../../../../../../lib/core/custom-editor-configs';
import {ActivatedRoute} from '@angular/router';
import * as uuid from 'uuid';
import {Journal, JournalResponse} from '../../../../../../lib/models/JournalResponse';
import * as moment from 'jalali-moment';
import {JalaliMomentDateAdapter} from '../../../../../../lib/core/custom-date-adapter';
import {ProfileResponse, ProfilesResponse} from '../../../../../../lib/models/UserResponse';
import {RestAdminService} from '../../../../../../lib/core/services/client/rest-admin.service';
import {Tables} from '../../../../../../lib/core/constants/server.constants';
import {ResourceCategoriesResponse} from '../../../../../../lib/models/ResourceCategoryResponse';
import {Resource, ResourcesRequest} from '../../../../../../lib/models/ResourceResponse';
import {BaseResourceComponent} from '../../base-resource.component';

@Component({
  selector: 'app-article-add',
  templateUrl: './article-add.component.html',
  styleUrls: ['./article-add.component.scss'],
  standalone: false
})
export class ArticleAddComponent extends BaseResourceComponent implements OnInit {
  error: string;
  articleCategories = [];
  editorConfig = getCustomEditorConfigs();
  journals: Journal[] = [];
  selectedMentors = [];
  abstractError = false;
  @ViewChild('multiUserSearch') searchElem: ElementRef;
  providers = new FormControl();
  allProviders: any[] = [];
  filteredProviders: any[] = this.allProviders;

  constructor(public formBuilder: UntypedFormBuilder,
              private activatedRoute: ActivatedRoute,
              private restAdminService: RestAdminService,
              injector: Injector) {
    super(injector, Tables.Article);
  }

  ngOnInit(): void {
    this.sub = this.activatedRoute.params.subscribe(params => {
      this.restAdminService.getUsers().subscribe((m: ProfilesResponse) => {
        this.allProviders = m.data;
        this.filteredProviders = this.allProviders;
        const model: ResourcesRequest = {page: 0, pageSize: -1, tableId: Tables.Journal};
        this.restClientService.getResources(model).subscribe((j: JournalResponse) => {
          this.journals = j.data;
          this.restClientService.getResourceCategories(Tables.Article).subscribe((acr: ResourceCategoriesResponse) => {
            this.articleCategories = acr.resourceCategories;
            if (params.id) {
              this.restClientService.getResource({id: params.id, languageCode: this.lang, tableId: Tables.Article}).subscribe((a: Resource) => {
                this.selectedMentors = a.authors.map(s => s.id);
                this.editItem = a;
                this.myForm = this.formBuilder.group({
                  category: [this.articleCategories.find(s => s.id === a.categoryId), Validators.required],
                  title: [a.title, Validators.required],
                  abstract: [a.abstract, Validators.required],
                  image: null,
                  keywords: a.keywords,
                  authors: [null, Validators.required],
                  journalId: a.journalId,
                  expirationDate: null,
                  expirationTime: '',
                  anchors: this.formBuilder.array([]),
                  language: a.language,
                  file: null,
                  publishDate: a.publishDate,
                  isVip: a.isVip,
                  price: a.price,
                  discount: a.discount,
                  showInChem: this.editItem?.showInChem,
                  showInAcademy: this.editItem?.showInAcademy,
                });
                if (a.expirationDate) {
                  const array = a.expirationDate.split('/').map(s => Number(s));
                  this.myForm.controls.expirationDate.setValue(new JalaliMomentDateAdapter(null).createDate(array[0], array[1] - 1, array[2]));
                  this.expDate = a.expirationDate;
                  this.expTime = a.expirationTime;
                }
                this.editAnchors();
                this.hideSingleLangControls();
              });
            } else {
              this.myForm = this.formBuilder.group({
                category: ['', Validators.required],
                title: ['', Validators.required],
                abstract: ['', Validators.required],
                image: '',
                keywords: '',
                authors: [null, Validators.required],
                journalId: '',
                expirationDate: null,
                expirationTime: '',
                anchors: this.formBuilder.array([]),
                language: '',
                file: '',
                publishDate: '',
                isVip: false,
                price: '',
                discount: '',
                showInChem: true,
                showInAcademy: true,
              });
            }
          });
        });
      });
    });
  }

  /****************************/
  onInputChange(event: any) {
    const searchInput = event.target.value.toLowerCase();
    this.filter(searchInput);
  }

  filter(searchInput: string) {
    // Set selected values to retain the selected checkbox state
    this.setSelectedValues();
    this.providers.patchValue(this.selectedMentors);
    this.filteredProviders = this.allProviders.filter(s => {
      const prov = (s.firstName + ' ' + s.lastName).toLowerCase();
      return prov.includes(searchInput);
    });
  }

  setSelectedValues() {
    if (this.providers.value && this.providers.value.length > 0) {
      this.providers.value.forEach((e) => {
        if (this.selectedMentors.indexOf(e) === -1) {
          this.selectedMentors.push(e);
        }
      });
    }
  }

  add() {
    this.restAdminService.addInitialUser(this.searchElem.nativeElement.value).subscribe((d: ProfileResponse) => {
      this.allProviders.push(d.data);
      this.filter(this.searchElem.nativeElement.value);
    });
  }

  onOpenChange(searchInput: any) {
    searchInput.value = '';
    this.filteredProviders = this.allProviders;
  }

  /****************************/

  public createAnchor(): UntypedFormGroup {
    return this.formBuilder.group({
      id: uuid.v4().replace(/-/g, ''),
      name: null,
      desc: null,
    });
  }

  public addAnchor(): void {
    const anchors = this.myForm.controls.anchors as UntypedFormArray;
    anchors.push(this.createAnchor());
  }

  public deleteAnchor(index) {
    const anchors = this.myForm.controls.anchors as UntypedFormArray;
    anchors.removeAt(index);
  }

  handleImageInput(target: any) {
    if (target.files && target.files[0]) {
      this.image = target.files[0];
    }
  }

  handleFileInput(target: any) {
    if (target.files && target.files[0]) {
      this.file = target.files[0];
    }
  }

  toEnglish(s: string) {
    let x = s.replace(/[۰-۹]/g, d => '۰۱۲۳۴۵۶۷۸۹'.indexOf(d).toString());
    x = x.replace(/[٠-٩]/g, d => '٠١٢٣٤٥٦٧٨٩'.indexOf(d).toString());
    return x;
  }

  toPersian(s: string): string {
    return s.replace(/\d/g, (d) => '۰۱۲۳۴۵۶۷۸۹'[Number(d)]);
  };

  dateChanged(dateRangeStart: HTMLInputElement) {
    this.expDate = this.toEnglish(dateRangeStart.value);
    console.log(this.expDate);
    const startDate = this.toGeorgianDate();
    console.log(startDate);
  }

  onFormSubmit(values: any): void {
    this.leaveAbstract();
    super.onFormSubmit(values);
  }

  toGeorgianDate() {
    const start = moment.from(this.expDate, 'fa', 'YYYY/MM/DD').locale('en').format('YYYY/MM/DD');
    const yearStart = Number(start.substring(0, 4));
    const monthStart = Number(start.substring(5, 7));
    const dayStart = Number(start.substring(8, 10));
    const startDate = new Date(yearStart, monthStart - 1, dayStart);
    console.log(startDate);
  }

  leaveAbstract() {
    const x = this.myForm.controls.abstract.value;
    this.abstractError = !x;
  }

  private editAnchors() {
    const anchors = this.myForm.controls.anchors as UntypedFormArray;
    while (anchors.length) {
      anchors.removeAt(0);
    }
    this.editItem.anchors?.forEach(plan => {
      let p = {
        id: plan.id,
        name: plan.name,
        desc: plan.desc,
      };
      anchors.push(this.formBuilder.group(p));
    });
  }
}
