import { boot } from 'quasar/wrappers';
import axios, { AxiosInstance } from 'axios';
import { Notify } from 'quasar';

declare module '@vue/runtime-core' {
  interface ComponentCustomProperties {
    $axios: AxiosInstance;
  }
}

// Be careful when using SSR for cross-request state pollution
// due to creating a Singleton instance here;
// If any client changes this (global) instance, it might be a
// good idea to move this instance creation inside of the
// "export default () => {}" function below (which runs individually
// for each client)
const api = axios.create({
  baseURL: 'http://localhost:5000/api/v1',
  headers: {
    AccessControlAllowOrigin: '*'
  }
});

api.interceptors.response.use(
  (response) => response,
  (err) => {
    if (axios.isAxiosError(err)) {
      const { response } = err;
      // eslint-disable-next-line
      if (response?.data.errors)
        // eslint-disable-next-line
        response?.data.errors.forEach((element: string) => {
          Notify.create({
            message: element
          });
        });
      else
        Notify.create({
          message: 'Terjadi kesalahan'
        });
      throw err;
    }
  }
);

export default boot(({ app }) => {
  // for use inside Vue files (Options API) through this.$axios and this.$api

  app.config.globalProperties.$axios = axios;
  // ^ ^ ^ this will allow you to use this.$axios (for Vue Options API form)
  //       so you won't necessarily have to import axios in each vue file

  app.config.globalProperties.$api = api;
  // ^ ^ ^ this will allow you to use this.$api (for Vue Options API form)
  //       so you can easily perform requests against your app's API
});

export { api };
