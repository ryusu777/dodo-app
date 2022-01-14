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

export default crud;
