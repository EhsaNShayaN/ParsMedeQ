import {BaseResult} from './BaseResult';
import {Paginated} from './Pagination';

export interface TreatmentCenterResponse extends BaseResult<Paginated<TreatmentCenter>> {
}

export interface TreatmentCenter {
  id: number;
  provinceId: number;
  province: string;
  cityId: number;
  city: string;
  title: string;
  description: string;
  image: string;
  creationDate: string;
}
