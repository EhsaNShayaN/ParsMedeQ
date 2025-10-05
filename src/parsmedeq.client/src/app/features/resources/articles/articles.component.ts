import {Component, DoCheck, OnInit} from '@angular/core';
import {BaseComponent} from '../../../base-component';
import {Article, ArticleResponse} from '../../../../lib/models/ArticleResponse';
import {AppSettings, Settings} from '../../../app.settings';
import {Pagination} from '../../../../lib/app.models';
import {createTree, Tree} from '../../../../lib/models/MenusResponse';
import {Tables} from '../../../../lib/core/constants/server.constants';
import {ResourceCategoriesResponse} from '../../../../lib/models/ResourceCategoryResponse';
import {ResourcesRequest} from "../../../../lib/models/ResourceResponse";

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.scss']
})
export class ArticlesComponent extends BaseComponent implements OnInit, DoCheck {
  viewText = 'VIEW';
  public articleCategories = [];
  data: Tree[] = [];
  title: string;
  start = 0;
  selectedId: string = null;
  ///////////////////////////////

  public settings: Settings;
  public viewType: string = 'grid';
  public viewCol: number = 25;
  public count: number = 12;
  public sort: number = 1;
  public pagination: Pagination = new Pagination(1, this.count, null, 2, 0, 0);
  public message: string | null;
  public items: Article[] = [];

  ///////////////////////////////

  constructor(public appSettings: AppSettings) {
    super();
    this.settings = appSettings.settings;
  }

  ngOnInit() {
    this.restClientService.getResourceCategories(Tables.Article).subscribe((acr: ResourceCategoriesResponse) => {
      this.articleCategories = acr.resourceCategories;
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

  public getItems() {
    const model: ResourcesRequest = {
      page: this.pagination.page,
      pageSize: this.pagination.perPage,
      sort: this.sort,
      id: this.selectedId,
      tableId: Tables.Article,
    };
    this.title = this.articleCategories.find(s => s.id === this.selectedId)?.title;
    this.restClientService.getResources(model).subscribe((result: ArticleResponse) => {
      if (this.items && this.items.length > 0) {
        this.settings.loadMore.page++;
        this.pagination.page = this.settings.loadMore.page;
      }
      if (result.data.length == 0) {
        this.items = [];
        this.items.length = 0;
        this.pagination = new Pagination(1, this.count, null, 2, 0, 0);
        this.message = 'No Results Found';
        return false;
      }
      if (this.items && this.items.length > 0) {
        this.items = this.items.concat(result.data);
      } else {
        this.items = result.data;
      }
      this.pagination = {
        page: result.pageNumber + 1,
        perPage: result.pageSize,
        prePage: result.pageNumber - 1 ? result.pageNumber - 1 : null,
        nextPage: (result.totalPages > result.pageNumber) ? result.pageNumber + 1 : null,
        total: result.totalCount,
        totalPages: result.totalPages,
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
      console.log(this.pagination);
      this.getItems();
    }
  }
}
