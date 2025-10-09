import {AlborzPagingRequest, Paginated} from './Pagination';
import {ProductCategory} from './ProductCategoryResponse';
import {FileUploadValue} from '../../shared/components/multi-file-upload/multi-file-upload.component';

export interface ProductResponse {
  data: Paginated<Product>;
}

export class Product {
  id!: number;
  tableId!: number;
  productCategoryId?: number;
  productCategoryTitle?: string;

  title!: string;
  description!: string;

  image!: string;
  fileId?: number;

  price?: number;
  discount?: number;
  stock!: number;
  creationDate!: string;
  registered!: boolean;

  images: ProductImage[] = [];
}

export interface ProductImage {
  id: number;
  ordinal: number;
  path: string;
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
  gallery: FileUploadValue;
}


export interface IdTitleRequest {
  id: number;
  title: string;
}
