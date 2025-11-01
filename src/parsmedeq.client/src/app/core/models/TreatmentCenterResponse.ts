import {BaseResult} from './BaseResult';
import {Paginated} from './Pagination';

export interface TreatmentCenterResponse extends BaseResult<Paginated<TreatmentCenter>> {
}

export interface TreatmentCenter {
  id: number;
  locationId: number;
  province: string;
  city: string;
  title: string;
  description: string;
  image: string;
  creationDate: string;
}
