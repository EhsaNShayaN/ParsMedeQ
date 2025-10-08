import { BaseResult } from "./BaseResult";

export interface ResourceCategoriesResponse extends BaseResult<ResourceCategory[]> {
}

export interface ResourceCategory {
  id: number;
  title: string;
  description: string;
  count: number;
  parentId: number;
  parentTitle: string;
  creationDate: string;
}
