<template>
  <q-page class="column items-left q-mt-xl q-px-sm">
    <h3 class="text-bold q-mx-lg q-mt-sm">Menjual</h3>
    <!-- TODO: Pagination -->
    <q-table
      grid
      :rows="rows"
      :columns="goodsColumns"
      row-key="id"
      :filter="filter"
      hide-header
    >
      <template v-slot:top-right>
        <base-input
          borderless
          dense
          debounce="300"
          v-model="filter"
          placeholder="Search"
        >
          <template v-slot:append>
            <q-icon name="search" />
          </template>
        </base-input>
      </template>
      <template v-slot:item="props">
        <div class="q-pa-xs col-12">
          <base-card
            :class="props.selected ? 'bg-grey-2' : ''"
            style="height: 100px"
          >
            <q-card-section horizontal class="row">
              <q-card-section class="col">
                <p
                  class="text-overline q-pa-none q-ma-none"
                  style="line-height: 15px"
                >
                  {{ props.row.goodsCode }}
                </p>
                <p class="text-bold text-h5 q-pa-none q-ma-none">
                  {{ props.row.goodsName }}
                </p>
                <div class="row">
                  <p class="q-pr-md">{{ props.row.partNumber }}</p>
                  |
                  <p class="q-pl-md">{{ props.row.carType }}</p>
                </div>
              </q-card-section>

              <q-card-section class="text-right">
                <p class="text-overline q-ma-none" style="line-height: 15px">
                  Stok: {{ props.row.stockAvailable }}
                </p>
                <p
                  class="text-overline q-ma-none self-end"
                  style="line-height: 15px"
                >
                  Rp {{ props.row.purchasePrice }}
                </p>
                <q-card-actions align="right">
                  <base-button
                    icon="remove"
                  />
                  <base-button
                    icon="add"
                  />
                </q-card-actions>
              </q-card-section>
            </q-card-section>
          </base-card>
        </div>
      </template>
    </q-table>
  </q-page>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted, inject } from 'vue';
import { IGoods } from 'pages/goods/goods.interface';
import { ICreateResponse, IPagination } from 'src/models/responses.interface';
import { api } from 'src/boot/axios';
import { IPageFilter } from 'src/models/requests.interface';
import { AxiosError, AxiosResponse } from 'axios';
import { useQuasar } from 'quasar';
import { goodsColumns } from 'pages/goods/goods-columns';
import BaseInput from 'components/ui/BaseInput.vue';
import BaseButton from 'src/components/ui/BaseButton.vue';
import BaseCard from 'src/components/ui/BaseCard.vue';

export default defineComponent({
  components: {
    BaseInput,
    BaseButton,
    BaseCard
  },
  setup() {
    const $q = useQuasar();
    const filter = ref('');
    const notifyError: ((err: unknown | AxiosError) => void) | undefined =
      inject('notifyError');

    const pagination = ref<IPageFilter>({
      page: 1,
      rowsPerPage: 5
    });

    const rows = ref<IGoods[]>([]);

    onMounted(async () => {
      try {
        const response: AxiosResponse<IPagination<IGoods>> = await api.get(
          '/goods',
          {
            params: {
              ...pagination.value
            }
          }
        );

        if (response.data.data) rows.value = response.data.data;
      } catch (err) {
        notifyError?.(err);
      }
    });


    return {
      goodsColumns,
      rows,
      filter,
    };
  }
});
</script>
