import type { Event, NewEvent } from './types'
import axios from '@axios'

export const useCalendarStore = defineStore('case-calendar', {
  // arrow function recommended for full type inference
  state: () => ({
    availableCalendars: [
      {
        color: 'error',
        label: 'ISU',
      },
      {
        color: 'primary',
        label: '삼호중공업',
      },
      {
        color: 'warning',
        label: '교보AXA자산운용',
      },
      {
        color: 'success',
        label: '신영증권',
      },
      {
        color: 'info',
        label: '지누스',
      },
    ],
    selectedCalendars: ['ISU', '삼호중공업'],
  }),
  actions: {
    async fetchEvents() {
      return axios.get('/apps/case-calendar/events', { params: { calendars: this.selectedCalendars.join(',') } })
    },
    async addEvent(event: NewEvent) {
      return axios.post('/apps/case-calendar/events', { event })
    },
    async updateEvent(event: Event) {
      return axios.post(`/apps/case-calendar/events/${event.id}`, { event })
    },
    async removeEvent(eventId: string) {
      return axios.delete(`/apps/case-calendar/events/${eventId}`)
    },

  },
})
