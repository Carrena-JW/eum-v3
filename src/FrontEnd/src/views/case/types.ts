import { CalendarEvent } from '@/@fake-db/types'
import type { Except } from 'type-fest'

export type NewEvent = Except<Event, 'id'>
import type { Invoice, PaymentDetails } from '@/@fake-db/types'

export interface Event extends CalendarEvent {
  extendedProps: {
    calendar?: string
    location: string
    description: string
    guests: string[]
  }
}

export interface PurchasedProduct {
  title: string
  cost: number
  hours: number
  description: string
}

export interface CaseData {
  invoice: Invoice
  paymentDetails: PaymentDetails
  purchasedProducts: PurchasedProduct[]
  note: string
  paymentMethod: string
  salesperson: string
  thanksNote: string
}

export interface CaseParams {
  q?: string,
  status: string[],
  perPage: number,
  currentPage: number,
  startDate?: string,
  endDate?: string,
}
