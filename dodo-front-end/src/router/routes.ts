import { RouteRecordRaw } from 'vue-router';

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      { path: '', component: () => import('pages/Index.vue') },
      {
        path: 'goods',
        component: () => import('pages/goods/GoodsPage.vue')
      },
      {
        path: 'selling-goods',
        component: () => import('pages/selling-goods/SellingGoodsPage.vue')
      },
      {
        path: 'transaction-history',
        component: () => import('pages/transaction/TransactionHistory.vue')
      },
    ]
  },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/Error404.vue')
  }
];

export default routes;
