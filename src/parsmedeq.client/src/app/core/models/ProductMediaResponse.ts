import {BaseResult} from "./BaseResult";

export interface ProductMediaListResponse extends BaseResult<ProductMedia[]> {
}

export interface ProductMedia {
  id: number;
  productId: number;
  mediaId: number;
  ordinal: number;
  path: string;
}
