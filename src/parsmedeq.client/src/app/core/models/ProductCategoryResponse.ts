import {BaseResult} from './BaseResult';

export interface ProductCategoriesResponse extends BaseResult<ProductCategory[]> {
}

export interface ProductCategory {
  id: number;
  parentId: number;
  parentTitle: string;
  title: string;
  description: string;
  image: string;
  creationDate: string;
}
