import {Component, DoCheck, OnInit} from '@angular/core';
import {BaseComponent} from '../../../base-component';
import {Resource, ResourceResponse, ResourcesRequest} from '../../../core/models/ResourceResponse';
import {AppSettings, Settings} from '../../../app.settings';
import {Pagination} from '../../../core/models/Pagination';
import {createTree, Tree} from '../../../core/models/MenusResponse';
import {ResourceCategoriesResponse, ResourceCategory} from '../../../core/models/ResourceCategoryResponse';
import {Tables} from '../../../core/constants/server.constants';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.html',
  styleUrls: ['./articles.scss'],
  standalone: false
})
export class Articles extends BaseComponent implements OnInit, DoCheck {
  resourceCategories: ResourceCategory[] = [];
  data: Tree[] = [];
  title: string | undefined;
  start = 0;
  selectedId: number = 0;
  ///////////////////////////////

  public settings: Settings;
  public viewType: string = 'grid';
  public viewCol: number = 25;
  public count: number = 12;
  public sort: number = 1;
  public pagination: Pagination = new Pagination(1, this.count, null, 2, 0, 0);
  public message: string | null = null;
  public items: Resource[] = [];

  ///////////////////////////////

  constructor(public appSettings: AppSettings) {
    super();
    this.settings = appSettings.settings;
  }

  ngOnInit() {
    this.restApiService.getResourceCategories(Tables.Article).subscribe((acr: ResourceCategoriesResponse) => {
      this.resourceCategories = acr.data;
      this.data = createTree(this.resourceCategories);
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

  public getItems() {
    const model: ResourcesRequest = {
      pageIndex: this.pagination.page,
      pageSize: this.pagination.perPage,
      sort: this.sort,
      resourceCategoryId: this.selectedId,
      tableId: Tables.Article,
    };
    this.title = this.resourceCategories.find(s => s.id === this.selectedId)?.title;
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

  public resetLoadMore() {
    this.settings.loadMore.complete = false;
    this.settings.loadMore.start = false;
    this.settings.loadMore.page = 1;
    this.pagination = new Pagination(1, this.count, null, null, this.pagination.total, this.pagination.totalPages);
  }

  public changeCount(count: number) {
    this.count = count;
    this.resetLoadMore();
    this.items.length = 0;
    this.getItems();
  }

  public changeSorting(sort: number) {
    this.sort = sort;
    this.resetLoadMore();
    this.items.length = 0;
    this.getItems();
  }

  public changeViewType(obj: any) {
    this.viewType = obj.viewType;
    this.viewCol = obj.viewCol;
  }

  ngDoCheck() {
    if (this.settings.loadMore.load) {
      this.settings.loadMore.load = false;
      this.getItems();
    }
  }
}
