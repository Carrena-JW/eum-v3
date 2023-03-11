<script setup lang="ts">
import {emailValidator, requiredValidator} from '@validators';

const emit = defineEmits(['submit'])

const paymentTerms = ref(true);
const clientNotes = ref(false);
const paymentStub = ref(false);
const hasDueDate = ref(true);
const dueDate = ref('')
const selectedCaseScope = ref('버그');
const caseScope = ['버그', '개선', '추가 기능'];

</script>

<template>
  <VCard class="mb-8">
    <VCardText>
      <!-- 👉 Send Case -->
      <VBtn block prepend-icon="mdi-send-outline" class="mb-3"
      @click="() => emit('submit', {})">
        접수하기
      </VBtn>

      <!-- 👉 Preview -->
      <!--      <VBtn block color="secondary" variant="outlined" class="mb-3"-->
      <!--            :to="{ name: 'case-preview-id', params: { id: '5036' } }">-->
      <!--        Preview-->
      <!--      </VBtn>-->

      <!-- 👉 Save -->
      <!--      <VBtn block color="secondary" variant="outlined">-->
      <!--        Save-->
      <!--      </VBtn>-->
    </VCardText>
  </VCard>

  <!-- 👉 Select payment method -->
  <VSelect v-model="selectedCaseScope" :items="caseScope"
           label="케이스 유형" class="mb-6"/>

  <!-- 👉 Payment Terms -->
<!--  <div class="d-flex align-center justify-space-between">-->
<!--    <VLabel for="payment-terms">마감 날짜 요구</VLabel>-->
<!--    <div>-->
<!--      <VSwitch id="payment-terms" v-model="hasDueDate"/>-->
<!--    </div>-->
<!--  </div>-->

  <!-- 👉 Due Date -->
<!--  <AppDateTimePicker-->
<!--    v-model="dueDate"-->
<!--    label="납기일"-->
<!--    :rules="[requiredValidator]"-->
<!--    :disabled="!hasDueDate"-->
<!--  />-->


  <!-- 👉  Client Notes -->
  <!--  <div class="d-flex align-center justify-space-between">-->
  <!--    <VLabel for="client-notes">Client Notes</VLabel>-->
  <!--    <div>-->
  <!--      <VSwitch id="client-notes" v-model="clientNotes"/>-->
  <!--    </div>-->
  <!--  </div>-->

  <!-- 👉  Payment Stub -->
  <!--  <div class="d-flex align-center justify-space-between">-->
  <!--    <VLabel for="payment-stub">Payment Stub</VLabel>-->
  <!--    <div>-->
  <!--      <VSwitch id="payment-stub" v-model="paymentStub"/>-->
  <!--    </div>-->
  <!--  </div>-->
</template>
