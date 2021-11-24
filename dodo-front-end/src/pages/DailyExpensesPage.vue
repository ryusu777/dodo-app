<template>
  <q-page class="column items-left q-mt-xl q-px-sm">
    <h3 class="text-bold q-mx-lg q-mt-sm">Daily Expenses</h3>
    <q-table
      grid
      :rows="rows"
      row-key="id"
      v-model:filter="filter"
      v-model:pagination="requestPagination"
      @request="handleRequest"
    >
      <template v-slot:top-right>
        <base-button
          label="Tambah"
          @click="showAddDialog()"
          class="q-mr-md"
        />
      </template>
      <template v-slot:item="props">
        <div class="q-pa-xs col-12">
          <base-card
            :class="props.selected ? 'bg-grey-2' : ''"
            style="height: 100px"
          >
            <q-card-section horizontal class="row">
              <q-card-section class="col">
                <p class="text-bold text-h5 q-pa-none q-ma-none">
                  {{ props.row.expenses }}
                </p>
                <div class="row">
                  <p class="q-pr-md">{{ props.row.description }}</p>
                </div>
              </q-card-section>

            </q-card-section>
          </base-card>
        </div>
      </template>
    </q-table>
  </q-page>
</template>

<script lang="ts">
import { defineComponent, ref, inject, onMounted } from 'vue';
import { IExpenses } from 'src/models/interfaces/dailyexpenses.interface';
import { ICreateResponse, IPagination } from 'src/models/responses.interface';
import { api } from 'boot/axios';
import { IPageFilter } from 'src/models/requests.interface';
import { AxiosError, AxiosResponse } from 'axios';
import { useQuasar } from 'quasar';
import DailyExpensesFormDialog from 'components/expenses/DailyExpensesFormDialog.vue';
import BaseButton from 'components/ui/BaseButton.vue';
import BaseCard from 'components/ui/BaseCard.vue';

export default defineComponent({
  components: {
    BaseButton,
    BaseCard
  },
  setup() {
    const $q = useQuasar();
    const filter = ref('');
    const notifyError: ((err: unknown | AxiosError) => void) | undefined =
      inject('notifyError');

    const requestPagination = ref<IPageFilter>({
      page: 1,
      rowsPerPage: 5
    });

    const rows = ref<IExpenses[]>([]);

    onMounted(async () => await sendGetExpenses());

    async function handleRequest({ pagination }: { pagination: IPageFilter }) {
      requestPagination.value = pagination;
      await sendGetExpenses();
    }

    async function sendGetExpenses() {
      try {
        const response: AxiosResponse<IPagination<IExpenses>> = await api.get(
          '/dailyexpenses',
          {
            params: {
              ...requestPagination.value,
              searchText: filter.value
            }
          }
        );

        if (response.data.data) {
          rows.value = response.data.data;
          requestPagination.value.rowsNumber = response.data.rowsNumber;
          requestPagination.value.page = response.data.pageNumber;
          requestPagination.value.searchText = response.data.searchText;
          requestPagination.value.rowsPerPage = response.data.itemPerPage;
        }
      } catch (err) {
        notifyError?.(err);
      }
    }

    async function sendCreateRequest(dailyexpenses: IExpenses): Promise<void> {
      try {
        const response = await api.post<ICreateResponse>('/dailyexpenses', {
          expenses: dailyexpenses.expenses,
          description: dailyexpenses.description,
        });
        rows.value.push({
          id: response.data.id,
          expenses: dailyexpenses.expenses,
          description: dailyexpenses.description,
        });
      } catch (err) {
        notifyError?.(err);
      }
    }

    function showAddDialog() {
      $q.dialog({
        component: DailyExpensesFormDialog,
        componentProps: {
          title: 'Tambah Daftar'
        }
      }).onOk(async (dailyexpenses: IExpenses) => {
        await sendCreateRequest(dailyexpenses);
      });
    }

    return {
      rows,
      filter,
      sendGetExpenses,
      showAddDialog,
      requestPagination,
      handleRequest
    };
  }
});
</script>
