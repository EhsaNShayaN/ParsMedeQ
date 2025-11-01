import {BaseResult} from './BaseResult';

export interface LocationResponse extends BaseResult<Location[]> {
}

export interface Location {
  id: number;
  parentId: number;
  title: string;
}
