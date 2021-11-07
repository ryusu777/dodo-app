import { RouteRecordRaw } from 'vue-router';

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      { path: '', component: () => import('pages/Index.vue') },
      { path: 'create', component: () => import('src/pages/crud-pages/CreateGoods.vue') },
      { path: 'update', component: () => import('src/pages/crud-pages/UpdateGoods.vue') },
      { path: 'view', component: () => import('src/pages/crud-pages/ReadGoods.vue') },
      { path: 'coba', component: () => import('src/pages/crud-pages/coba.vue') }
    ],
  },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/Error404.vue'),
  },
];

export default routes;
