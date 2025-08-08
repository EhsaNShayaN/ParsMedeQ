export class PagingRequest {
  page!: number;
  pageSize!: number;
  sort?: number;
  adminSort?: SortRequest;
  query?: string;
  fromDate?: string;
  toDate?: string;
  fromPrice?: number;
  toPrice?: number;
  url?: string;
}

export class AlborzPagingRequest extends PagingRequest {
  expired?: boolean;
}

export class SortRequest {
  active!: string;
  direction!: string;
}

export interface Paginated {
  hasNext: boolean;
  hasPrevious: boolean;
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
}

export class Pagination {
  constructor(public page: number,
              public perPage: number,
              public prePage: number | null,
              public nextPage: number | null,
              public total: number,
              public totalPages: number) {
  }
}
