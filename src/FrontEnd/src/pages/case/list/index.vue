<script setup lang="ts">
import type { Case } from '@/@fake-db/types'
import CaseFilter, { SearchOptions } from '@/views/case/CaseFilter.vue'
import { useCaseStore } from '@/views/case/useCaseStore'
import { avatarText } from '@core/utils/formatters'

// 👉 Store
const invoiceListStore = useCaseStore()

const searchQuery = ref('')
const rowPerPage = ref(10)
const currentPage = ref(1)
const totalPage = ref(1)
const totalInvoices = ref(0)
const cases = ref<Case[]>([])
const selectedRows = ref<string[]>([])
const selectAllInvoice = ref(false)
const searchOptions = ref<SearchOptions>({
  dateRange: '',
  status: [],
})

// 👉 Fetch Invoices
watchEffect(() => {
  console.log('options', searchOptions)
  const [start, end] = searchOptions.value.dateRange ? searchOptions.value.dateRange.split('to') : ''

  invoiceListStore.fetchInvoices(
    {
      q: searchQuery.value,
      status: searchOptions.value.status,
      perPage: rowPerPage.value,
      currentPage: currentPage.value,
      startDate: start,
      endDate: end,
    },
  ).then(response => {
    cases.value = response.data.cases
    totalPage.value = response.data.totalPage
    totalInvoices.value = response.data.totalInvoices
  }).catch(error => {
    console.log(error)
  })
})

// 👉 watching current page
watchEffect(() => {
  if (currentPage.value > totalPage.value) {
    currentPage.value = totalPage.value
  }
})

// 👉 Computing pagination data
const paginationData = computed(() => {
  const firstIndex = cases.value.length ? ((currentPage.value - 1) * rowPerPage.value) + 1 : 0
  const lastIndex = cases.value.length + ((currentPage.value - 1) * rowPerPage.value)

  return `${firstIndex}-${lastIndex} of ${totalInvoices.value}`
})

// 👉 Invoice balance variant resolver
const resolveInvoiceBalanceVariant = (balance: string | number, total: number) => {
  if (balance === total) {
    return {
      status: 'Unpaid',
      chip: { color: 'error' },
    }
  }

  if (balance === 0) {
    return {
      status: 'Paid',
      chip: { color: 'success' },
    }
  }

  return {
    status: balance,
    chip: { variant: 'text' },
  }
}

// 👉 Invoice status variant resolver
const resolveInvoiceStatusVariantAndIcon = (status: string) => {
  if (status === 'Partial Payment') {
    return {
      variant: 'warning',
      icon: 'mdi-chart-timeline-variant',
    }
  }
  if (status === 'Paid') {
    return {
      variant: 'success',
      icon: 'mdi-check',
    }
  }
  if (status === 'Downloaded') {
    return {
      variant: 'info',
      icon: 'mdi-arrow-down',
    }
  }
  if (status === 'Draft') {
    return {
      variant: 'secondary',
      icon: 'mdi-content-save-outline',
    }
  }
  if (status === 'Sent') {
    return {
      variant: 'primary',
      icon: 'mdi-email-outline',
    }
  }
  if (status === 'Past Due') {
    return {
      variant: 'error',
      icon: 'mdi-alert-circle-outline',
    }
  }

  return {
    variant: 'secondary',
    icon: 'mdi-close',
  }
}

// 👉 Add/Remove all checkbox ids in/from array
const selectUnselectAll = () => {
  selectAllInvoice.value = !selectAllInvoice.value
  if (selectAllInvoice.value) {
    cases.value.forEach(invoice => {
      if (!selectedRows.value.includes(`check${invoice.id}`)) {
        selectedRows.value.push(`check${invoice.id}`)
      }
    })
  } else {
    selectedRows.value = []
  }
}

// 👉 watch if checkbox array is empty all checkbox should be uncheck
watch(selectedRows, () => {
  if (!selectedRows.value.length) {
    selectAllInvoice.value = false
  }
}, { deep: true })

// 👉 Add/Remove individual checkbox in/from array
const addRemoveIndividualCheckbox = (checkID: string) => {
  if (selectedRows.value.includes(checkID)) {
    const index = selectedRows.value.indexOf(checkID)

    selectedRows.value.splice(index, 1)
  } else {
    selectedRows.value.push(checkID)
    selectAllInvoice.value = true
  }
}

const computedMoreList = computed(() => {
  return (paramId: number) => ([
    {
      title: 'Download',
      value: 'download',
      prependIcon: 'mdi-download-outline',
    },
    {
      title: 'Edit',
      value: 'edit',
      prependIcon: 'mdi-pencil-outline',
      to: {
        name: 'apps-invoice-edit-id',
        params: { id: paramId },
      },
    },
    {
      title: 'Duplicate',
      value: 'duplicate',
      prependIcon: 'mdi-layers-outline',
    },
  ])
})
</script>

<template>
  <section v-if="cases">
    <!-- 👉 Invoice Filters  -->
    <CaseFilter v-model="searchOptions"/>

    <VCard id="invoice-list">
      <VCardText class="d-flex align-center flex-wrap gap-4">
        <!-- 👉 Actions  -->
        <div class="me-3">
          <VBtn variant="tonal" color="secondary" prepend-icon="mdi-tray-arrow-up">Export</VBtn>
        </div>

        <VSpacer/>

        <div class="d-flex align-center flex-wrap gap-4">
          <!-- 👉 Search  -->
          <div class="invoice-list-search">
            <VTextField v-model="searchQuery" placeholder="Search Case" density="compact"/>
          </div>

          <!-- 👉 Create case -->
          <VBtn prepend-icon="mdi-plus" :to="{ name: 'case-add' }">Create Case</VBtn>
        </div>
      </VCardText>

      <VDivider/>

      <!-- SECTION Table -->
      <VTable class="text-no-wrap table-header-bg rounded-0">
        <!-- 👉 Table head -->
        <thead>
        <tr>
          <th scope="col">#CaseNo</th>
          <th scope="col" class="text-center">상태</th>
          <th scope="col" class="text-center">원인</th>
          <th scope="col">엔지니어</th>
          <th scope="col" class="text-center">시간</th>
          <th scope="col">제목</th>
          <th scope="col" class="text-center">시작</th>
          <th scope="col" class="text-center">종료</th>
          <th scope="col" class="text-center">ACTIONS</th>
        </tr>
        </thead>

        <!-- 👉 Table Body -->
        <tbody>
        <tr v-for="item in cases" :key="item.id">
          <!-- 👉 Id -->
          <td>
            <RouterLink :to="{ name: 'case-preview-id', params: { id: item.id } }">
              #{{ item.id }}
            </RouterLink>
          </td>

          <!-- 👉 Status -->
          <td class="text-center">
            {{ item.status }}
          </td>

          <!-- 👉 Reason -->
          <td class="text-center">
            {{ item.reason }}
          </td>

          <!-- 👉 Client Avatar and Email -->
          <td>
            <VTooltip>
              <template #activator="{ props} ">
                <div class="d-flex align-center" v-bind="props">
                  <VAvatar
                    size="34"
                    :color="resolveInvoiceStatusVariantAndIcon(item.invoiceStatus).variant"
                    variant="tonal"
                    class="me-3"
                  >
                    <VImg
                      v-if="item.engineer.avatar.length"
                      :src="item.engineer.avatar"
                    />
                    <span
                      v-else
                      class="text-sm"
                    >{{ avatarText(item.engineer.name) }}</span>
                  </VAvatar>
                  <div class="d-flex flex-column">
                    <h6 class="text-sm font-weight-medium mb-0">
                      {{ item.engineer.name }}
                    </h6>
                    <!--                <span class="text-caption">{{ item.engineer.email}}</span>-->
                  </div>
                </div>
              </template>
              <p class="mb-0">
                {{ item.engineer.name }} {{ item.engineer.title }}
              </p>
              <p class="mb-0">
                Email: {{ item.engineer.email }}
              </p>
              <p class="mb-0">
                Contact: {{ item.engineer.contact }}
              </p>
            </VTooltip>
          </td>

          <!-- 👉 Hours -->
          <td class="text-center">
            {{ item.consume }}
          </td>

          <!-- 👉 Title -->
          <td class="text-wrap case-title">
            {{ item.title }}
          </td>

          <!-- 👉 Starts -->
          <td>
            {{ item.starts }}
          </td>

          <!-- 👉 Ends -->
          <td>
            {{ item.ends }}
          </td>

          <!-- 👉 Actions -->
          <td style="width: 8rem;">
            <IconBtn>
              <VIcon icon="mdi-delete-outline"/>
            </IconBtn>

            <VTooltip>
              <template #activator="{ props }">
                <IconBtn title="test" v-bind="props">
                  <VIcon icon="mdi-eye-outline"/>
                </IconBtn>
              </template>
              <p class="mb-0">
                이 케이스를 주시합니다. 엔지니어가 정보를 업데이트하면 변경사항에 대한 알림을 수신합니다.
              </p>
            </VTooltip>

            <MoreBtn
              :menu-list="computedMoreList(item.id)"
              item-props
            />
          </td>
        </tr>
        </tbody>

        <!-- 👉 table footer  -->
        <tfoot v-show="!cases.length">
        <tr>
          <td
            colspan="8"
            class="text-center text-body-1"
          >
            No data available
          </td>
        </tr>
        </tfoot>
      </VTable>
      <!-- !SECTION -->

      <VDivider/>

      <!-- SECTION Pagination -->
      <VCardText class="d-flex flex-wrap justify-end gap-4 pa-2">
        <!-- 👉 Rows per page -->
        <div
          class="d-flex align-center me-3"
          style="width: 171px;"
        >
          <span class="text-no-wrap me-3">Rows per page:</span>
          <VSelect
            v-model="rowPerPage"
            density="compact"
            variant="plain"
            class="mt-n4"
            :items="[10, 20, 30, 50]"
          />
        </div>

        <!-- 👉 Pagination and pagination meta -->
        <div class="d-flex align-center">
          <h6 class="text-sm font-weight-regular">
            {{ paginationData }}
          </h6>

          <VPagination
            v-model="currentPage"
            size="small"
            :total-visible="1"
            :length="totalPage"
            @next="selectedRows = []"
            @prev="selectedRows = []"
          />
        </div>
      </VCardText>
      <!-- !SECTION -->
    </VCard>
  </section>
</template>

<style lang="scss">
#invoice-list {
  .invoice-list-actions {
    inline-size: 8rem;
  }

  .invoice-list-search {
    inline-size: 12rem;
  }
}


.case-title {
  min-width: 200px;
}
//
//@media (min-width: 600px) {
//  .case-title {
//    max-width: 300px;
//    white-space: nowrap !important;
//    overflow: hidden !important;
//    text-overflow: ellipsis !important;
//  }
//}

@media (max-width: 1200px) {
  .case-title {
    max-width: 500px;
    white-space: nowrap !important;
    overflow: hidden !important;
    text-overflow: ellipsis !important;
  }
}
</style>
