import casePages from './case';
import admin from './admin';
import type {VerticalNavItems} from '@/@layouts/types';

export default [...casePages, ...admin] as VerticalNavItems;
