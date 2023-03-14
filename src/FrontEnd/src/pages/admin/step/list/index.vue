<script setup lang="ts">
import {useStepStore} from '@/views/admin/step/useStepStore';
import {StepData} from '@api/step';
import StepAddDialog from '@/views/admin/step/StepAddDialog.vue';

// 👉 Store
const stepStore = useStepStore();

const items = ref<StepData[]>([]);
const current = ref<StepData | null>(null);
const isDialogVisible = ref(false);

const onRefresh = async () => {
  items.value = await stepStore.fetchSteps({});
};

// 👉 Fetch Invoices
watchEffect(async () => {
  await onRefresh();
});

const onDelete = (item: StepData) => {
  current.value = item;
  isDialogVisible.value = true;
};

const onConfirmDelete = async () => {
  if (current.value) {
    await stepStore.delete(current.value);
    await onRefresh();
  }
};
</script>

<template>
  <ConfirmDialog v-model:is-dialog-visible="isDialogVisible" confirmation-msg="삭제하시겠습니까?" @confirm="onConfirmDelete"/>
  <section v-if="items">
    <VCard id="invoice-list">
      <VCardText class="d-flex align-center flex-wrap gap-4">
        <VSpacer/>

        <div class="d-flex align-center flex-wrap gap-4">
          <!-- 👉 Create case -->
          <!--          <VBtn prepend-icon="mdi-plus" @click="() => isAddDialogVisible = true">Create Step</VBtn>-->
          <StepAddDialog @refresh="onRefresh"/>
        </div>
      </VCardText>

      <VDivider/>

      <!-- SECTION Table -->
      <VTable class="text-no-wrap table-header-bg rounded-0">
        <!-- 👉 Table head -->
        <thead>
        <tr>
          <th scope="col">Id</th>
          <th scope="col" class="text-center">System</th>
          <th scope="col" class="text-center">Name</th>
          <th scope="col" class="text-center">ACTIONS</th>
        </tr>
        </thead>

        <!-- 👉 Table Body -->
        <tbody>
        <tr v-for="item in items" :key="item.id">
          <!-- 👉 Id -->
          <td style="width: 200px;">
            <RouterLink :to="{ name: 'case-preview-id', params: { id: item.id } }">
              #{{ item.id }}
            </RouterLink>
          </td>

          <!-- 👉 IsSystem -->
          <td class="text-center" style="width: 1rem;">
            <VCheckbox
              :id="`check${item.id}`"
              disabled
              :model-value="item.isSystem"
            />
          </td>

          <!-- 👉 Status -->
          <td class="text-center">
            {{ item.name }}
          </td>

          <!-- 👉 Actions -->
          <td style="width: 8rem;" class="text-center">
            <IconBtn>
              <VIcon icon="mdi-delete-outline" @click="onDelete(item)"/>
            </IconBtn>
          </td>
        </tr>
        </tbody>

        <!-- 👉 table footer  -->
        <tfoot v-show="!items.length">
        <tr>
          <td
            colspan="8"
            class="text-center text-body-1"
          >
            No data available
          </td>
        </tr>
        </tfoot>
      </VTable>
      <!-- !SECTION -->
    </VCard>
  </section>
</template>

<style lang="scss">
#invoice-list {
  .invoice-list-actions {
    inline-size: 8rem;
  }

  .invoice-list-search {
    inline-size: 12rem;
  }
}


.case-title {
  min-width: 200px;
}

//
//@media (min-width: 600px) {
//  .case-title {
//    max-width: 300px;
//    white-space: nowrap !important;
//    overflow: hidden !important;
//    text-overflow: ellipsis !important;
//  }
//}

@media (max-width: 1200px) {
  .case-title {
    max-width: 500px;
    white-space: nowrap !important;
    overflow: hidden !important;
    text-overflow: ellipsis !important;
  }
}
</style>
