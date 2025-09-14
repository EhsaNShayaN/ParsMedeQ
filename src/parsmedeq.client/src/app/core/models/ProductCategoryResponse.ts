export interface ProductCategoriesResponse {
  data: ProductCategory[];
}

export interface ProductCategory {
  id: number;
  title: string;
  description: string;
  count: number;
  parentId: number;
  parentTitle: string;
  creationDate: string;
}
