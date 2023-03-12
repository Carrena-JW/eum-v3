import {defineStore} from 'pinia';
import api, {StepData, StepParams} from '@api/step';

export const useStepStore = defineStore('StepStore', {
  actions: {
    // 👉 Fetch all Invoices
    fetchSteps(params: StepParams) {
      return api.get(params);
    },
    delete(item: StepData) {
      return api.delete(item.id!);
    },
    save(item: StepData) {
      return api.save(item)
    }
  }
});
