import { RouteRecordRaw } from 'vue-router';

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      { path: '', component: () => import('pages/Index.vue') },
      {
        path: 'goods',
        component: () => import('pages/GoodsPage.vue')
      },
      {
        path: 'selling-goods/:id',
        component: () => import('pages/SellingGoodsPage.vue'),
        props: true
      },
      {
        path: 'transaction-history',
        component: () => import('pages/TransactionHistoryPage.vue')
      },
      {
        path: 'daily-expenses',
        component: () => import('pages/DailyExpensesPage.vue')
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
