<script lang="ts" setup>
import type {VForm} from 'vuetify/components';
import CaseAddActions from '@/views/case/add/CaseAddActions.vue';
import CaseWritable from '@/views/case/add/CaseWritable.vue';

// Type: Case data
import type {CaseData} from '@/views/case/types';

// 👉 Default Blank Data
const invoiceData = ref<CaseData>({
  invoice: {
    id: 5037,
    issuedDate: '',
    service: '',
    total: 0,
    avatar: '',
    invoiceStatus: '',
    balance: '',
    dueDate: '',
    client: {
      title: '',
      department: '',
      address: '',
      company: '',
      companyEmail: '',
      contact: '',
      country: '',
      name: ''
    }
  },
  paymentDetails: {
    totalDue: '$12,110.55',
    bankName: 'American Bank',
    country: 'United States',
    iban: 'ETD95476213874685',
    swiftCode: 'BR91905'
  },
  purchasedProducts: [
    {
      title: '',
      cost: 0,
      hours: 0,
      description: ''
    }
  ],
  note: '',
  paymentMethod: '',
  salesperson: '',
  thanksNote: ''
});


const form = ref<VForm>();


const router = useRouter();

const onSubmit = async () => {
  const result = await form.value?.validate();
  console.log(result);
  if (result?.valid) router.back();
  else {
    const id = result?.errors[0].id as string;
    const elme = document.getElementById(id);
    console.log('focus', id, elme)
    if (elme) elme.focus();
  }
};


</script>

<template>
  <VForm ref="form" lazy-validation>
    <VRow>
      <!-- 👉 CaseEditable -->
      <VCol cols="12" md="9">
        <CaseWritable :data="invoiceData"/>
      </VCol>

      <!-- 👉 Right Column: Case Action -->
      <VCol cols="12" md="3">
        <CaseAddActions @submit="onSubmit"/>
      </VCol>
    </VRow>
  </VForm>
</template>
