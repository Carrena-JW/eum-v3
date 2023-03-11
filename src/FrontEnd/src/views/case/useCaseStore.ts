import { defineStore } from 'pinia'
import type { CaseParams } from './types'
import axios from '@axios'

export const useCaseStore = defineStore('CaseStore', {
  actions: {
    // 👉 Fetch all Invoices
    fetchInvoices(params: CaseParams) {
      return axios.get('apps/cases', { params })
    },

    // 👉 Fetch single invoice
    fetchInvoice(id: number) {
      return axios.get(`/apps/cases/${id}`)
    },

    // 👉 Fetch Clients
    fetchClients() {
      return axios.get('/apps/case/clients')
    },
  },
})
