import { Ref, ref } from 'vue';
import { IPagination } from 'src/models/responses.interface';
import { IPageFilter } from 'src/models/requests.interface';
import { LooseDictionary } from 'quasar';
import crud from 'src/models/crud';

export function useCrudEntity<T extends { id?: number }>(routing: string) {
  const route = ref(routing);
  const grid = ref({ data: [] }) as Ref<IPagination<T>>;
  const pageFilter = ref<IPageFilter>({
    page: 1,
    rowsPerPage: 5
  });

  async function getAll() {
    const response = await crud.get<T>(route.value, pageFilter.value);
    if (response) {
      grid.value = response as IPagination<T>;
      pageFilter.value.page = grid.value.pageNumber;
      pageFilter.value.rowsNumber = grid.value.rowsNumber;
      pageFilter.value.rowsPerPage = grid.value.rowsPerPage;
    }
  }

  async function get(id: number) {
    const response = await crud.get<T>(`${route.value}/${id}`);
    if (response) return response;
  }

  async function paging(page: IPageFilter) {
    pageFilter.value = page;
    await getAll();
  }

  async function search(searchText: string) {
    pageFilter.value.searchText = searchText;
    await getAll();
  }

  async function create(entity: T) {
    const response = await crud.create<T>(route.value, entity);
    if (response && grid.value.data) {
      grid.value.data.unshift(response);
      return true;
    }
    return false;
  }

  async function update(entity: T) {
    const response = await crud.update<T>(
      `${route.value}/${entity.id || -1}`,
      entity
    );
    if (response && grid.value.data) {
      const selectedData =
        grid.value.data[
          grid.value.data.findIndex((item) => item.id == entity.id)
          ];

      if (selectedData)
        for (const key in entity) {
          selectedData[key] = entity[key];
        }
      return true;
    }
    return false;
  }

  async function remove(id: number) {
    const response = await crud.remove(route.value, id);
    if (response && grid.value.data) {
      grid.value.data.splice(
        grid.value.data.findIndex((item) => item.id == id),
        1
      );
      return true;
    }
    return false;
  }

  async function filter(filter: LooseDictionary) {
    const response = await crud.filter<T>(
      `${route.value}-filter`,
      pageFilter.value,
      filter
    );

    if (response) {
      grid.value = response;
      pageFilter.value.page = grid.value.pageNumber;
      pageFilter.value.rowsNumber = grid.value.rowsNumber;
      pageFilter.value.rowsPerPage = grid.value.rowsPerPage;
    }
  }

  return {
    grid,
    pageFilter,
    getAll,
    get,
    paging,
    create,
    update,
    remove,
    search,
    filter
  };
}
