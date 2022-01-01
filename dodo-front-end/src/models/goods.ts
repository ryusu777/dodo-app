import { ref } from 'vue';
import { IPageFilter } from './requests.interface';
import { IPagination } from './responses.interface';
import crud from './crud';
export interface IGoods {
  id?: number;
  goodsName?: string;
  goodsCode?: string;
  carType?: string;
  partNumber?: string;
  minimalAvailable?: number;
  stockAvailable?: number;
  purchasePrice?: number;
}

export function useGoods() {
  const grid = ref<IPagination<IGoods>>({});
  const pageFilter = ref<IPageFilter>({
    page: 1,
    rowsPerPage: 5
  });
  async function getAll() {
    const response = await crud.get<IGoods>('/goods', pageFilter.value);
    if (response) {
      grid.value = response as IPagination<IGoods>;
      pageFilter.value.page = grid.value.pageNumber;
      pageFilter.value.rowsNumber = grid.value.rowsNumber;
      pageFilter.value.rowsPerPage = grid.value.rowsPerPage;
    }
  }

  async function get(id: number) {
    const response = await crud.get<IGoods>(`/goods/${id}`);
    if (response) return response;
  }

  async function paging(page: IPageFilter) {
    pageFilter.value = page;
    await getAll();
  }

  async function create(goods: IGoods) {
    const response = await crud.create<IGoods>('/goods', goods);
    if (response && grid.value.data) {
      grid.value.data.unshift(response);
    }
  }

  async function update(goods: IGoods) {
    const response = await crud.update<IGoods>(
      `/goods/${goods.id || -1}`,
      goods
    );
    if (response && grid.value.data)
      grid.value.data[
        grid.value.data.findIndex((item) => item.id == goods.id)
      ] = goods;
  }

  async function remove(id: number) {
    const response = await crud.remove('/goods', id);
    if (response && grid.value.data)
      grid.value.data.splice(
        grid.value.data.findIndex((item) => item.id == id),
        1
      );
  }

  return {
    grid,
    pageFilter,
    getAll,
    get,
    paging,
    create,
    update,
    remove
  };
}
