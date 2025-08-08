/*import {Profile} from './UserResponse';*/
import {AlborzPagingRequest, Paginated} from './Pagination';
import {ResourceCategory} from './ResourceCategoryResponse';

export interface ResourceResponse extends Paginated {
  data: Resource[];
}

export class Resource {
  id!: string;
  secondId!: number;
  tableId!: number;
  categoryId?: string;
  categoryTitle!: string;

  title!: string;
  abstract!: string;
  anchors!: ResourceAnchor[];
  description!: string;
  keywords!: string;

  image!: string;
  mimeType!: string;
  doc!: string;
  journalId?: string;

  language!: string;
  publishDate!: string;
  publishInfo!: string;
  publisher!: string;
  price?: number;
  discount?: number;
  isVip!: boolean;
  pinned!: boolean;
  downloadCount!: number;
  ordinal?: number;
  showInChem!: boolean;
  showInAcademy!: boolean;
  deleted!: boolean;
  disabled!: boolean;
  expirationDate!: string;
  expirationTime!: string;
  expired!: boolean;
  creationDate!: string;
  registered!: boolean;

  resourceCategories: ResourceCategory[] = [];

  /*authors: Profile[];
  advisors: Profile[];
  chiefEditors: Profile[];*/
}

export interface ResourceAnchor {
  id: string;
  name: string;
  desc: string;
}

export class ResourceRequest {
  id?: string;
  secondId?: number;
  tableId?: number;
  languageCode?: string;
}

export class ResourcesRequest extends AlborzPagingRequest {
  id?: string;
  tableId?: number;
  pinned?: boolean;
  categoryId?: string;
}

export interface AddResourceRequest {
  id?: string;
  tableId: number;
  showInChem: boolean;
  showInAcademy: boolean;
  title: string;
  image: string;
  mimeType: string;
  language: string;
  isVip: boolean;
  price?: number;
  discount?: number;
  description: string;
  authors: string[];
  publishInfo: string;
  publisher: string;
  category: IdTitleRequest;
  abstract: string;
  anchors: ResourceAnchor[];
  expirationDate: string;
  expirationTime: string;
  keywords: string;
  journalId?: string;
  publishDate: string;
  categories: string[];
  categoryId?: string;
  doc: string;
  advisors: string[];
  chiefEditors: string[];

  // Journal specific property
  journalExpirationDate?: number;
}


export interface IdTitleRequest {
  id: string;
  title: string;
}
