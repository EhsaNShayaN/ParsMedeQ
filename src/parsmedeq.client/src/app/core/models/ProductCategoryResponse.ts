export interface ProductCategoriesResponse {
  data: ProductCategory[];
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
