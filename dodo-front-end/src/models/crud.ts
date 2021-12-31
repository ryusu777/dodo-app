import { api } from 'src/boot/axios';
import { ICreateResponse, IPagination } from './responses.interface';
import { Dialog } from 'quasar';
import BaseDialog from 'components/ui/BaseDialog.vue';
import { IPageFilter } from './requests.interface';
import { AxiosResponse } from 'axios';

export async function get<T>(
  route: string,
  pagination?: IPageFilter
): Promise<T | IPagination<T> | null> {
  if (pagination) {
    try {
      const response: AxiosResponse<IPagination<T>> = await api.get('/goods', {
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
    const response: AxiosResponse<T> = await api.get('route');
    return response.data;
  } catch {
    return null;
  }
}
export async function create<T>(entityRequest: T, route: string) {
  try {
    const response = await api.post<ICreateResponse>(route, {
      entityRequest
    });
    return {
      id: response.data.id,
      ...entityRequest
    };
  } catch {
    return null;
  }
}

export async function update<T>(entityRequest: T, route: string) {
  try {
    await api.put(route, entityRequest);
    return entityRequest;
  } catch {
    return null;
  }
}

export function remove(route: string, id: number) {
  Dialog.create({
    component: BaseDialog,
    componentProps: {
      title: 'Hapus barang',
      body: 'Yakin ingin menghapus barang?',
      okLabel: 'Hapus',
      cancelLabel: 'Tidak'
    }
  }).onOk(async () => {
    try {
      await api.delete(`${route}/${id}`);

      return id;
    } catch {
      return null;
    }
  });
}
