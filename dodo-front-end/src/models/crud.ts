import { ref, Ref } from 'vue';
import { api } from 'src/boot/axios';
import { ICreateResponse, IPagination } from './responses.interface';
import { IPageFilter } from './requests.interface';
import { AxiosResponse } from 'axios';
import { LooseDictionary } from 'quasar';

const crud = {
  async get<T>(
    route: string,
    pagination?: IPageFilter
  ): Promise<T | IPagination<T> | null> {
    if (pagination) {
      try {
        const response: AxiosResponse<IPagination<T>> = await api.get(route, {
          params: {
            ...pagination
          }
        });

        return response.data;
      } catch {
        return null;
      }
    }

    try {
      const response: AxiosResponse<T> = await api.get(route);
      return response.data;
    } catch {
      return null;
    }
  },
  async create<T>(route: string, entityRequest: T) {
    try {
      const response = await api.post<ICreateResponse>(route, entityRequest);
      return {
        ...entityRequest,
        id: response.data.id
      };
    } catch {
      return null;
    }
  },

  async update<T>(route: string, entityRequest: T) {
    try {
      await api.put(route, entityRequest);
      return entityRequest;
    } catch {
      return null;
    }
  },

  async remove(route: string, id: number) {
    try {
      await api.delete(`${route}/${id}`);

      return id;
    } catch {
      return null;
    }
  },

  async filter<T>(
    route: string,
    pageFilter: IPageFilter,
    filter: LooseDictionary
  ) {
    try {
      const response = await api.post<IPagination<T>>(route, filter, {
        params: {
          ...pageFilter
        }
      });

      return response.data;
    } catch {
      return null;
    }
  }
};

// TODO: Request success or fail should be returned
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
    console.log(response);
    if (response && grid.value.data) {
      grid.value.data.unshift(response);
    }
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
      for (const key in entity) {
        selectedData[key] = entity[key];
      }
    }
  }

  async function remove(id: number) {
    const response = await crud.remove(route.value, id);
    if (response && grid.value.data)
      grid.value.data.splice(
        grid.value.data.findIndex((item) => item.id == id),
        1
      );
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

export default crud;
