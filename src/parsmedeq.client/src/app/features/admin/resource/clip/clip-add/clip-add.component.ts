import {Component, ElementRef, Injector, OnInit, ViewChild} from '@angular/core';
import {FormControl, UntypedFormBuilder, Validators} from '@angular/forms';
import {ActivatedRoute} from '@angular/router';
import {ProfileResponse, ProfilesResponse} from '../../../../../../lib/models/UserResponse';
import {RestAdminService} from '../../../../../../lib/core/services/client/rest-admin.service';
import {getCustomEditorConfigs} from '../../../../../../lib/core/custom-editor-configs';
import {Tables} from '../../../../../../lib/core/constants/server.constants';
import {ResourceCategoriesResponse} from '../../../../../../lib/models/ResourceCategoryResponse';
import {BaseResourceComponent} from '../../base-resource.component';
import {Resource} from '../../../../../../lib/models/ResourceResponse';

@Component({
  selector: 'app-clip-add',
  templateUrl: './clip-add.component.html',
  styleUrls: ['./clip-add.component.scss'],
  standalone: false
})
export class ClipAddComponent extends BaseResourceComponent implements OnInit {
  selectedAuthors = [];
  clipCategories = [];
  editorConfig = getCustomEditorConfigs();
  ////////////////////////
  @ViewChild('multiUserSearch') searchElem: ElementRef;
  providers = new FormControl();
  allProviders: any[] = [];
  filteredProviders: any[] = this.allProviders;

  constructor(public formBuilder: UntypedFormBuilder,
              private activatedRoute: ActivatedRoute,
              private restAdminService: RestAdminService,
              injector: Injector) {
    super(injector, Tables.Clip);
  }


  ngOnInit() {
    this.sub = this.activatedRoute.params.subscribe(params => {
      this.restAdminService.getUsers().subscribe((m: ProfilesResponse) => {
        this.allProviders = m.data;
        this.filteredProviders = this.allProviders;
        this.restClientService.getResourceCategories(Tables.Clip).subscribe((bc: ResourceCategoriesResponse) => {
          this.clipCategories = bc.resourceCategories;
          if (params.id) {
            this.restClientService.getResource({id: params.id, languageCode: this.lang, tableId: Tables.Clip}).subscribe((j: Resource) => {
              this.editItem = j;
              this.selectedAuthors = this.editItem.authors?.map(s => s.id);
              this.myForm = this.formBuilder.group({
                title: [this.editItem?.title, Validators.required],
                description: this.editItem?.description,
                image: null,
                file: null,
                authors: null,
                categoryId: this.editItem?.categoryId,
                isVip: this.editItem?.isVip,
                price: this.editItem?.price,
                discount: this.editItem?.discount,
                showInChem: this.editItem?.showInChem,
                showInAcademy: this.editItem?.showInAcademy,
              });
              this.hideSingleLangControls();
            });
          } else {
            this.myForm = this.formBuilder.group({
              title: [null, Validators.required],
              description: null,
              image: null,
              file: null,
              authors: null,
              categoryId: null,
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
  }

  /****************************/
  onInputChange(event: any) {
    const searchInput = event.target.value.toLowerCase();
    this.filter(searchInput);
  }

  filter(searchInput: string) {
    this.filteredProviders = this.allProviders.filter(s => {
      const prov = (s.firstName + ' ' + s.lastName).toLowerCase();
      return prov.includes(searchInput);
    });
  }

  add() {
    this.restAdminService.addInitialUser(this.searchElem.nativeElement.value).subscribe((d: ProfileResponse) => {
      this.allProviders.push(d.data);
      this.filter(this.searchElem.nativeElement.value);
      // this.filteredProviders = this.allProviders;
    });
  }

  onOpenChange(searchInput: any) {
    searchInput.value = '';
    this.filteredProviders = this.allProviders;
  }

  /****************************/

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
}
