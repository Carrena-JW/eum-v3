import avatar1 from '@images/avatars/avatar-1.png'
import avatar2 from '@images/avatars/avatar-2.png'
import avatar3 from '@images/avatars/avatar-3.png'
import avatar4 from '@images/avatars/avatar-4.png'
import avatar5 from '@images/avatars/avatar-5.png'
import avatar6 from '@images/avatars/avatar-6.png'
import avatar7 from '@images/avatars/avatar-7.png'
import avatar8 from '@images/avatars/avatar-8.png'
import { paginateArray } from '@/@fake-db/utlis'
import type { Case, Engineer, Client } from '@/@fake-db/types'
import mock from '@/@fake-db/mock'

const now = new Date()
const currentMonth = now.toLocaleString('default', { month: '2-digit' })
const engineer: { [key: string]: Engineer } = {
  iwak29: {
    avatar: avatar1,
    name: '김지훈',
    title: '책임',
    email: 'iwak29@feelanet.com',
    contact: '010-8131-6558',
  },
  leejh: {
    avatar: avatar2,
    name: '이준형',
    title: '책임',
    email: 'leejh@feelanet.com',
    contact: '010-1234-5678',
  },
  seongjong: {
    avatar: avatar3,
    name: '윤성종',
    title: '전임',
    email: 'seongjong@feelanet.com',
    contact: '010-2016-6926',
  },
}

const client: { [key: string]: Client } = {
  arkim: {
    name: '김아림',
    title: '대리',
    department: '전략사업팀',
    company: 'ISU',
    companyEmail: 'arkim@isu.co.kr',
    country: 'KOR',
    contact: '010.8453.3056',
    address: '서울시 서초구 사평대로 60 덕명빌딩',
  },
  hjjeon: {
    name: '전현주',
    title: '대리',
    department: '경영지원팀',
    company: '한미글로벌',
    companyEmail: 'hjjeon@hanmiglobal.com',
    country: 'KOR',
    contact: '010-5155-6211',
    address: '서울시 강남구 테헤란로 87길 36 도심공항타워 11층 한미글로벌 경영지원팀',
  },
}

const database: Case[] = [
  {
    id: '2303030017489',
    issuedDate: `${now.getFullYear()}-${currentMonth}-13`,
    status: '진행중',
    reason: null,
    engineer: engineer.leejh,
    client: client.arkim,
    title: '부서원 삭제 시, 조직도 표기여부 N 설정',
    starts: '2023-03-02',
    consume: '36m',
    ends: null,
    product: 'EumMail',
    contract: '계약_계약_유지보수계약',
  },
  {
    id: '2212060015439',
    issuedDate: `${now.getFullYear()}-${currentMonth}-13`,
    status: '완료',
    reason: null,
    engineer: engineer.iwak29,
    client: client.hjjeon,
    title: '자기점검서 메일발송 오류 문의',
    starts: '2023-03-03',
    consume: '1h 12m',
    ends: null,
    product: 'EumMail',
    contract: '계약_계약_유지보수계약',
  },
  {
    id: '2303020017433',
    issuedDate: `${now.getFullYear()}-${currentMonth}-13`,
    status: '대기',
    reason: '고객확인',
    engineer: engineer.seongjong,
    client: client.hjjeon,
    title: '데데이터 관련 오류사항데이터 관련 오류사항데이터 관련 오류사항데이터 관련 오류사항이터 관련 오류사데데이터 관련 오류사항데이터 관련 오류사항데이터 관련 오류사항데이터 관련 오류사항이터 관련 오류사항항데데이터 관련 오류사항데이터 관련 오류사항데이터 관련 오류사항데이터 관련 오류사항이터 관련 오류사항',
    starts: '2023-03-06',
    consume: '3h 25m',
    ends: null,
    product: 'EumMail',
    contract: '계약_계약_유지보수계약',
  },
]

mock.onGet('/apps/cases').reply(config => {
  const {
    q = '',
    status = null,
    perPage = 0,
    currentPage = 1,
    startDate = '',
    endDate = '',
  } = config.params ?? {}

  const queryLowered = q.toLowerCase()

  let filteredCases = database.filter(
    item => ((item.engineer.name.toLowerCase().includes(queryLowered)
        || item.engineer.email.toLowerCase().includes(queryLowered)
        || item.title.toLowerCase().includes(queryLowered))
    ),
  ).reverse()

  if (status && status.length > 0) {
    filteredCases = filteredCases.filter(caseObj => status.includes(caseObj.status))
  }

  if (startDate && endDate) {
    filteredCases = filteredCases.filter(caseObj => {
      const start = new Date(startDate).getTime()
      const end = new Date(endDate).getTime()
      const issuedDate = new Date(caseObj.issuedDate).getTime()

      return issuedDate >= start && issuedDate <= end
    })
  }

  const totalPage = Math.round(filteredCases.length / perPage) ? Math.round(filteredCases.length / perPage) : 1
  const totalCases = filteredCases.length

  return [200, {
    cases: paginateArray(filteredCases, perPage, currentPage),
    totalPage,
    totalCases,
  }]
})

// 👉 Get a single item
mock.onGet(/\/apps\/cases\/\d+/).reply(config => {
  // Get event id from URL
  const caseId = config.url?.substring(config.url.lastIndexOf('/') + 1)

  const item = database.find(e => e.id === caseId)

  if (!item) {
    return [404, { message: 'Unable to find the requested item' }]
  }

  const responseData = {
    item,
    paymentDetails: {
      totalDue: '$12,110.55',
      bankName: 'American Bank',
      country: 'United States',
      iban: 'ETD95476213874685',
      swiftCode: 'BR91905',
    },
  }

  return [200, responseData]
})

// 👉 Get Client
mock.onGet('/apps/case/engineers').reply(() => {
  const engineers = database.map(item => item.engineer)

  return [200, engineers.slice(0, 5)]
})
