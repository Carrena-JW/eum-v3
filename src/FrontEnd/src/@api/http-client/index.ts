import EumApiClient, { EumApiRequestConfig, EumApiResponse } from '../common'
import router from '@/router'

/**
 * 앱 전역에서 Activity Indicator 표시/해제
 * @param {*} loading
 */
function loading(loading: boolean): void {
  if (loading) {
  }
  // store.dispatch({ type: SET_LOADING, loading: loading });
}
const style = 'background: #222; color: #FF725D'
const style2 = 'background: #222; color: #CC5B4A'

export const globalApi = {}

export const api = {}

const routeToLogin = () => {
  //   store.dispatch(types.LOGOUT, true)
}

export default new EumApiClient({
  rootUrl: '/cm',
  interceptors: {
    request: {
      onFulfilled: (request: EumApiRequestConfig) => {
        const { url, data, method, params } = request
        const methodName = (method || '').toUpperCase()
        // const dataJson = JSON.stringify(data || params, null, 2);
        // const headerJson = JSON.stringify(headers, null, 2);
        console.log(`%c>>${methodName}: ${url}`, style, data, params)
        loading(true)
        return request
      },
      onRejected: function(err: any) {
        console.log('request', 'rejected', err, this)
        loading(false)
        return Promise.reject(err)
      },
    },
    response: {
      onFulfilled: (response: EumApiResponse<any>): EumApiResponse<any> => {
        const { url, method } = response.config
        const methodName = (method || '').toUpperCase()
        const { data } = response
        console.log(`%c<<${methodName}: ${url}`, style2, data)
        loading(false)
        if (data.code === 'UnauthorizedAccessException') {
          throw new Error(data)
        } else if (data.code === 'ErrorNonExistentMailbox') {
          throw new Error(data)
        }
        return response
      },
      onRejected: (err: any) => {
        console.log('response', 'rejected', 'code', err)
        loading(false)
        if (err?.response?.status == 401) {
          throw new Error(err)
        } else {
          return Promise.reject(err.response)
        }
      },
    },
  },
})
