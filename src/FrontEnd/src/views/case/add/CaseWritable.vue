<script setup lang="ts">
import {emailValidator, requiredValidator} from '@validators';
import InvoiceProductEdit from '../CaseProductEdit.vue';
import type {CaseData} from '../types';
import {useCaseStore} from '../useCaseStore';
import type {Client} from '@/@fake-db/types';
import {VNodeRenderer} from '@layouts/components/VNodeRenderer';
import {themeConfig} from '@themeConfig';
import AppDateTimePicker from '@core/components/AppDateTimePicker.vue';
import {de} from 'vuetify/locale';

interface Props {
  data: CaseData;
}

const props = defineProps<Props>();

const caseStore = useCaseStore();

const subject = ref('');
const importance = ref(0);
const ticksLabels = {0: '낮음', 1: '중간', 2: '높음', 3: '심각'};
const behavior = ref('');
const reproduce = ref('');
const expected = ref('');
const additional = ref('');

const color = computed(() => {
  switch (importance.value) {
    case 0:
      return 'primary';
    case 1:
      return 'success';
    case 2:
      return 'warning';
    default:
      return 'error';
  }
});
</script>

<template>
  <VCard>
    <VCardText class="d-flex flex-wrap justify-space-between flex-column flex-sm-row">
      <VCol cols="12">
        <h6 class="font-weight-bold text-xl">
          케이스 접수
        </h6>
      </VCol>

      <VCol cols="12">
        <VTextField
          v-model="subject"
          :rules="[requiredValidator]"
          label="제목"
          placeholder="제목을 입력하세요."
          required
        />
      </VCol>

      <VCol cols="12">
        <VTextarea label="증상"
                   id="case-add-behavior"
                   placeholder="버그가 무엇인지에 대한 명확하고 간결한 설명."
                   v-model="behavior"
                   :rules="[requiredValidator]"
                   :rows="4"
                   density="compact"/>
      </VCol>

      <VCol cols="12">
        <VSlider v-model="importance"
                 :ticks="ticksLabels"
                 :color="color"
                 :max="3"
                 step="1"
                 show-ticks="always"
                 tick-size="5"
        />
      </VCol>

      <VCol cols="12">
        <VTextarea label="재연절차"
                   id="case-add-reproduce"
                   placeholder="증상을 재현하는 단계:
1. Go to '...'
2. Click on '....'
3. Scroll down to '....'
4. See error"
                   v-model="reproduce"
                   :rules="[requiredValidator]"
                   :rows="5"
                   density="compact"/>
      </VCol>

      <VCol cols="12">
        <VTextarea label="예상되는 동작"
                   id="case-add-expected"
                   placeholder="예상되는 일에 대한 명확하고 간결한 설명."
                   v-model="expected"
                   :rules="[requiredValidator]"
                   :rows="5"
                   density="compact"/>
      </VCol>

      <VCol cols="12">
        <VTextarea label="추가 컨텍스트"
                   placeholder="여기에 문제에 대한 다른 컨텍스트를 추가하십시오."
                   v-model="additional"
                   :rows="4"
                   density="compact"/>
      </VCol>
    </VCardText>
  </VCard>
</template>
