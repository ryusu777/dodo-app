export interface ICreateResponse {
  id?: number;
  errors?: string[];
}

export interface IPagination<T> {
  data?: T[];
  pageNumber?: number;
  totalPage?: number;
  itemPerPage?: number;
  totalItem?: number;
  sortBy?: string;
  descending?: string;
  searchText?: string;
}
