import {Component, DoCheck, OnInit} from '@angular/core';
import {BaseComponent} from '../../../base-component';
import {Notice, NoticeResponse} from '../../../../lib/models/NoticeResponse';
import {AppSettings, Settings} from '../../../app.settings';
import {Pagination} from '../../../../lib/app.models';
import {createTree, Tree} from '../../../../lib/models/MenusResponse';
import {Tables} from '../../../../lib/core/constants/server.constants';
import {ResourceCategoriesResponse} from '../../../../lib/models/ResourceCategoryResponse';
import {ResourcesRequest} from "../../../../lib/models/ResourceResponse";

@Component({
  selector: 'app-notices',
  templateUrl: './notices.component.html',
  styleUrls: ['./notices.component.scss']
})
export class NoticesComponent extends BaseComponent implements OnInit, DoCheck {
  viewText = 'VIEW';
  public noticeCategories = [];
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
  public items: Notice[] = [];

  ///////////////////////////////

  constructor(public appSettings: AppSettings) {
    super();
    this.settings = appSettings.settings;
  }

  ngOnInit() {
    this.restClientService.getResourceCategories(Tables.Notice).subscribe((acr: ResourceCategoriesResponse) => {
      this.noticeCategories = acr.resourceCategories;
      this.data = createTree(this.noticeCategories);
      this.getItems();
    });
  }

  itemClicked($event: Tree) {
    this.getItems($event.id);
  }

  public getItems(id: string = null) {
    if (id !== this.selectedId) {
      this.pagination = new Pagination(1, this.count, null, 2, 0, 0);
      this.items = null;
    }
    this.selectedId = id;
    const model: ResourcesRequest = {
      page: this.pagination.page,
      pageSize: this.pagination.perPage,
      sort: this.sort,
      id: this.selectedId,
      tableId: Tables.Notice
    };
    this.title = this.noticeCategories.find(s => s.id === id)?.title;
    this.restClientService.getResources(model).subscribe((result: NoticeResponse) => {
      if (this.items && this.items.length > 0) {
        this.settings.loadMore.page++;
        this.pagination.page = this.settings.loadMore.page;
      }
      if (result.data.length == 0) {
        if (this.items) {
          this.items.length = 0;
        }
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

  public changeCount(count) {
    this.count = count;
    this.resetLoadMore();
    this.items.length = 0;
    this.getItems(this.selectedId);
  }

  public changeSorting(sort) {
    this.sort = sort;
    this.resetLoadMore();
    this.items.length = 0;
    this.getItems(this.selectedId);
  }

  public changeViewType(obj) {
    this.viewType = obj.viewType;
    this.viewCol = obj.viewCol;
  }

  ngDoCheck() {
    if (this.settings.loadMore.load) {
      this.settings.loadMore.load = false;
      this.getItems(this.selectedId);
    }
  }
}
