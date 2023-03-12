import casePages from './case'
import admin from './admin'
import type { HorizontalNavItems } from '@layouts/types'

export default [...casePages, ...admin] as HorizontalNavItems
