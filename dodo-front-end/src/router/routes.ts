import { RouteRecordRaw } from 'vue-router';

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      { path: '', component: () => import('components/HomePage.vue') },
      {
        path: 'goods',
        component: () => import('components/goods/GoodsPage.vue')
      },
      {
        path: 'transaction/:transactionType/:id',
        component: () =>
          import('components/goods-transaction/GoodsTransactionPage.vue'),
        props: (route) => {
          let id = Number.parseInt(route.params.id as string);
          if (Number.isNaN(id)) id = -1;

          return {
            transactionType: route.params.transactionType || '',
            id
          };
        }
      },
      {
        path: 'transaction-history',
        component: () =>
          import('components/transaction-history/TransactionHistoryPage.vue')
      },
      {
        path: 'currency',
        component: () => import('components/currency/CurrencyPage.vue')
      }
    ]
  },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: '/:catchAll(.*)*',
    component: () => import('components/NotFoundPage.vue')
  }
];

export default routes;
