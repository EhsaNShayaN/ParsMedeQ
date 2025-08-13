export interface ResourceCategoriesResponse {
  resourceCategories: ResourceCategory[];
}

export interface ResourceCategory {
  id: number;
  title: string;
  description: string;
  count: number;
  parentId: number;
  creationDate: string;
}
