import {AlborzPagingRequest, Paginated} from './Pagination';
import {ProductCategory} from './ProductCategoryResponse';

export interface ProductResponse {
  data: Paginated<Product>;
}

export class Product {
  id!: number;
  tableId!: number;
  productCategoryId?: number;
  productCategoryTitle?: string;

  title!: string;
  abstract!: string;
  anchors!: ProductAnchor[];
  description!: string;
  keywords!: string;

  image!: string;
  fileId?: number;

  language!: string;
  publishDate!: string;
  publishInfo!: string;
  publisher!: string;
  price?: number;
  discount?: number;
  downloadCount!: number;
  ordinal?: number;
  expirationDate!: string;
  expirationTime!: string;
  expired!: boolean;
  creationDate!: string;
  registered!: boolean;

  productCategories: ProductCategory[] = [];
}

export interface ProductAnchor {
  id: string;
  name: string;
  desc: string;
}

export class ProductRequest {
  id?: number;
}

export class ProductsRequest extends AlborzPagingRequest {
  productCategoryId?: number;
}

export interface AddProductRequest {
  id?: number;
  tableId: number;
  title: string;
  imagePath?: string;
  mimeType: string;
  language: string;
  price?: number;
  discount?: number;
  description: string;
  publishInfo: string;
  publisher: string;
  category: IdTitleRequest;
  abstract: string;
  anchors: ProductAnchor[];
  expirationDate: string;
  expirationTime: string;
  keywords: string;
  publishDate: string;
  categories: string[];
  fileId?: number;
}


export interface IdTitleRequest {
  id: number;
  title: string;
}
