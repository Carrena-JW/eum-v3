<script lang="ts" setup>
import {useStepStore} from '@/views/admin/step/useStepStore';

interface Emit {
  (e: 'refresh'): void;
}

const emit = defineEmits<Emit>();

const stepStore = useStepStore();

const isDialogVisible = ref(false);
const name = ref('');

const onSave = async () => {
  await stepStore.save({
    name: name.value
  });
  emit('refresh');
  name.value = '';
  isDialogVisible.value = false;
};
</script>

<template>
  <VDialog
    v-model="isDialogVisible"
    max-width="600"
  >
    <!-- Dialog Activator -->
    <template #activator="{ props }">
      <VBtn prepend-icon="mdi-plus"
            v-bind="props">
        Create Step
      </VBtn>
    </template>

    <!-- Dialog Content -->
    <VCard title="새로운 스텝 생성">
      <DialogCloseBtn
        variant="text"
        size="small"
        @click="isDialogVisible = false"
      />

      <VCardText>
        <VRow>
          <VCol cols="12">
            <VTextField
              focused
              v-model="name"
              label="Name"
              placeholder=""
              persistent-hint
            />
          </VCol>
        </VRow>
      </VCardText>

      <VCardActions>
        <VSpacer/>
        <VBtn
          color="error"
          @click="isDialogVisible = false"
        >
          Close
        </VBtn>
        <VBtn
          color="success"
          @click="onSave"
        >
          Save
        </VBtn>
      </VCardActions>
    </VCard>
  </VDialog>
</template>
