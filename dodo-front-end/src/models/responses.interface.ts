export interface ICreateResponse {
  id?: number;
  errors?: string[];
}

export interface IPagination<T> {
  data?: T[];
  pageNumber?: number;
  totalPage?: number;
  rowsPerPage?: number;
  rowsNumber?: number;
  sortBy?: string;
  descending?: string;
  searchText?: string;
}
