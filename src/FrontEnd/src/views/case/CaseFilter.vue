<script setup lang="ts">

export interface SearchOptions {
  dateRange: string
  status: string[]
}

const props = defineProps<{
  modelValue: SearchOptions
}>()

const emit = defineEmits(['update:modelValue'])

const dateRange = ref(props.modelValue.dateRange)
const selectedStatus = ref(props.modelValue.status)

watchEffect(() => {
  emit('update:modelValue', {
    dateRange: dateRange,
    status: selectedStatus,
  })
})
</script>

<template>
  <!-- 👉 Invoice Filters  -->
  <VCard
    title="Filters"
    class="mb-6"
  >
    <VCardText>
      <VRow>
        <!-- 👉 Status filter -->
        <VCol
          cols="12"
          md="6"
        >
          <!-- 👉 Autocomplete -->
          <VCombobox
            v-model="selectedStatus"
            chips
            clearable
            multiple
            closable-chips
            clear-icon="mdi-close-circle-outline"
            :items="['접수', '대기', '진행중', '해결됨', '완료', '종료']"
            label="상태 필터"
          />
        </VCol>

        <!-- 👉 DateRange filter -->
        <VCol
          cols="12"
          md="6"
        >
          <AppDateTimePicker
            v-model="dateRange"
            label="Invoice Date"
            clear-icon="mdi-close"
            clearable
            :config="{ mode: 'range' }"
          />
        </VCol>
      </VRow>
    </VCardText>
  </VCard>
</template>

<style lang="scss">
</style>
