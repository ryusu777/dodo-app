<template>
  <q-page class="row items-center justify-evenly">
    <div>
      <!-- <example-component
      title="Example component"
      active
      :todos="todos"
      :meta="meta"
      ></example-component> -->
      <base-button
        label="View"
        @click="showView = true" />
      <base-button
        label="Add"
        @click="showAdd = true" />
    </div>
    
    <BaseDialog
      class="row justify-center"
      v-model="showView"
      title="View"
    >
      <BaseField
        label="id"
        stack-label
        class="col-11"
      >
        asdaa
      </BaseField>

      <BaseField
        label="id"
        stack-label
        class="col-11"
      >
        asdaa
      </BaseField>
    </BaseDialog>
    
    <BaseAddDialog
      class="row justify-center"
      v-model="showAdd"
      title="Add"
    >
      <base-input>Tambah</base-input>
      <base-date >Tanggal</base-date>
      <base-select-input
        :option="options"
        v-model="multiple"
      >
      Pilihan
      </base-select-input>
      <base-check-box label="True">
      </base-check-box>
    </BaseAddDialog>
  </q-page>
</template>

<script lang="ts">
// import { Todo, Meta } from 'components/models';
// import ExampleComponent from 'components/CompositionComponent.vue';
import { defineComponent, ref } from 'vue';
import BaseButton from 'src/components/ui/BaseButton.vue';
import BaseInput from 'src/components/ui/BaseInput.vue'
import BaseDate from 'src/components/ui/BaseInputDate.vue'
import BaseDialog from 'src/components/ui/BaseDialog.vue';
import BaseAddDialog from 'src/components/ui/BaseAddDialog.vue';
import BaseField from 'src/components/ui/BaseField.vue';
import BaseCheckBox from 'src/components/ui/BaseCheckBox.vue';
import BaseSelectInput from 'src/components/ui/BaseSelectInput.vue';

export default defineComponent({
  name: 'PageIndex',
  components: {
    BaseButton, 
    BaseInput,
    BaseDate,
    BaseDialog, 
    BaseAddDialog,
    BaseField, 
    BaseCheckBox,
    BaseSelectInput
    },
  setup() {
    
    // const todos = ref<Todo[]>([
    //   {
    //     id: 1,
    //     content: 'ct1'
    //   },
    //   {
    //     id: 2,
    //     content: 'ct2'
    //   },
    //   {
    //     id: 3,
    //     content: 'ct3'
    //   },
    //   {
    //     id: 4,
    //     content: 'ct4'
    //   },
    //   {
    //     id: 5,
    //     content: 'ct5'
    //   }
    // ]);
    // const meta = ref<Meta>({
    //   totalCount: 1200
    // });
  return {
    // todos,
    // meta,
    showView: ref(false),
    showAdd: ref(false),
    multiple: ref(null),

    options: ['pilihan1', 'pilihan2', 'pilihan3']
  };
  }
});
</script>
