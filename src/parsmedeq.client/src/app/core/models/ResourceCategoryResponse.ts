export interface ResourceCategoriesResponse {
  resourceCategories: ResourceCategory[];
}

export interface ResourceCategory {
  id: string;
  title: string;
  description: string;
  count: number;
  parentId: string;
  creationDate: string;
}
