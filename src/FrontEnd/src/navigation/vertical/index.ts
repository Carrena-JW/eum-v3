import type { VerticalNavItems } from '@/@layouts/types'
import admin from './admin'

export default [
  {
    title: 'Home',
    to: { name: 'index' },
    icon: { icon: 'mdi-home-outline' },
  },
  {
    title: 'Second page',
    to: { name: 'second-page' },
    icon: { icon: 'mdi-file-document-outline' },
  },
  ...admin,
] as VerticalNavItems
