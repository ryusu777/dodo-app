<template>
  <q-page class="row items-center justify-evenly">
    <div>
      <base-button
        label="View"
        @click="showView = true" />
      <base-button
        label="Add"
        @click="showAdd = true" />
      <base-button
        label="Confirm"
        @click="confirm" />
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
      <base-date>Tanggal</base-date>
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
import { defineComponent, ref } from 'vue';
import { useQuasar } from 'quasar'
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
    const $q = useQuasar()

     function confirm () {
      $q.dialog({
        title: 'Confirm',
        message: 'Would you like to turn on the wifi?',
        cancel: true,
        persistent: true
      }).onOk(() => {
        console.log('>>>> OK')
      }).onOk(() => {
        console.log('>>>> second OK catcher')
      }).onCancel(() => {
        console.log('>>>> Cancel')
      }).onDismiss(() => {
        console.log('I am triggered on both OK and Cancel')
      })
    }

  return {
    confirm,
    showView: ref(false),
    showAdd: ref(false),
    multiple: ref(null),

    options: ['pilihan1', 'pilihan2', 'pilihan3']
  };
  }
});
</script>
