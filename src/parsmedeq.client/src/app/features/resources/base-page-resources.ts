import {Directive, DoCheck, inject, OnInit} from '@angular/core';
import {BaseComponent} from '../../base-component';
import {ResourceCategoriesResponse, ResourceCategory} from '../../core/models/ResourceCategoryResponse';
import {createTree, Tree} from '../../core/models/MenusResponse';
import {AppSettings, Settings} from '../../app.settings';
import {Pagination} from '../../core/models/Pagination';
import {Resource, ResourceResponse, ResourcesRequest} from '../../core/models/ResourceResponse';
import {Tables} from '../../core/constants/server.constants';

@Directive()
export class BasePageResources extends BaseComponent implements OnInit, DoCheck {
  viewText = 'VIEW';
  public articleCategories: ResourceCategory[] = [];
  data: Tree[] = [];
  title?: string;
  start = 0;
  selectedId: number = 0;

  public appSettings: AppSettings;
  public settings: Settings;
  public viewType: string = 'grid';
  public viewCol: number = 25;
  public count: number = 12;
  public sort: number = 1;
  public pagination: Pagination = new Pagination(1, this.count, null, 2, 0, 0);
  public message: string | null = null;
  public items: Resource[] = [];

  constructor(private tableId: number) {
    super();
    this.appSettings = inject(AppSettings);
    this.settings = this.appSettings.settings;
  }

  ngOnInit() {
    this.restApiService.getResourceCategories(this.tableId).subscribe((s: ResourceCategoriesResponse) => {
      this.articleCategories = s.data;
      this.data = createTree(this.articleCategories);
      this.getItems();
    });
  }

  itemClicked($event: Tree) {
    if ($event.id === this.selectedId) {
      return;
    }
    this.selectedId = $event.id;
    this.resetLoadMore();
    this.items.length = 0;
    this.getItems();
  }

  getItems() {
    const model: ResourcesRequest = {
      pageIndex: this.pagination.page,
      pageSize: this.pagination.perPage,
      sort: this.sort,
      resourceCategoryId: this.selectedId,
      tableId: this.tableId,
    };
    this.title = this.articleCategories.find(s => s.id === this.selectedId)?.title;
    this.restApiService.getResources(model).subscribe((result: ResourceResponse) => {
      if (this.items && this.items.length > 0) {
        this.settings.loadMore.page++;
        this.pagination.page = this.settings.loadMore.page;
      }
      if (result.data.items.length == 0) {
        this.items = [];
        this.items.length = 0;
        this.pagination = new Pagination(1, this.count, null, 2, 0, 0);
        this.message = 'No Results Found';
        return false;
      }
      if (this.items && this.items.length > 0) {
        this.items = this.items.concat(result.data.items);
      } else {
        this.items = result.data.items;
      }
      this.pagination = {
        page: result.data.pageNumber + 1,
        perPage: result.data.pageSize,
        prePage: result.data.pageNumber - 1 ? result.data.pageNumber - 1 : null,
        nextPage: (result.data.totalPages > result.data.pageNumber) ? result.data.pageNumber + 1 : null,
        total: result.data.totalCount,
        totalPages: result.data.totalPages,
      };
      this.message = null;
      if (this.items.length == this.pagination.total) {
        this.settings.loadMore.complete = true;
        this.settings.loadMore.result = this.items.length;
      } else {
        this.settings.loadMore.complete = false;
      }
      return true;
    });
  }

  resetLoadMore() {
    this.settings.loadMore.complete = false;
    this.settings.loadMore.start = false;
    this.settings.loadMore.page = 1;
    this.pagination = new Pagination(1, this.count, null, null, this.pagination.total, this.pagination.totalPages);
  }

  changeCount(count: number) {
    this.count = count;
    this.resetLoadMore();
    this.items.length = 0;
    this.getItems();
  }

  changeSorting(sort: number) {
    this.sort = sort;
    this.resetLoadMore();
    this.items.length = 0;
    this.getItems();
  }

  changeViewType(obj: any) {
    this.viewType = obj.viewType;
    this.viewCol = obj.viewCol;
  }

  ngDoCheck() {
    if (this.settings.loadMore.load) {
      this.settings.loadMore.load = false;
      console.log(this.pagination);
      this.getItems();
    }
  }

  goToDetail(item: Resource) {
    const currentLang = this.translateService.getDefaultLang();
    let page = '';
    switch (this.tableId) {
      case Tables.Article:
        page = 'articles';
        break;
      case Tables.News:
        page = 'news';
        break;
      case Tables.Clip:
        page = 'clips';
        break;
    }
    this.navigateToLink(`/${page}/${item.id}`, currentLang);
  }
}
