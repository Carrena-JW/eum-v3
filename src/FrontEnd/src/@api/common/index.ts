import { EumApiCallOptions, EumApiDataMoldel, HttpMethods, EumApiConfig } from './types'
import AxiosClient from './AxiosClient'
export * from './types'

class EumApiClient {
  private apiConfig: EumApiConfig
  private axiosClient: AxiosClient

  constructor(userConfig: EumApiConfig) {
    this.apiConfig = {
      // baseURL: baseUrl,
      timeout: userConfig.timeout,
      ...userConfig,
      requestConfig: {
        ...userConfig.requestConfig,
        // @ts-ignore
        headers: {
          // Authorization: 'Bearer {token}',
          'X-Requested-With': 'XMLHttpRequest',
          "X-EUM-TIMEZONE" : this.getStandardTimezone(),  //2022-06-19 타임존 정보를 헤더에 추가 함
          "X-EUM-TIMEOFFSET" : new Date().getTimezoneOffset(),  //2022-06-19 타임존 정보를 헤더에 추가 함
          ...userConfig.requestConfig?.headers,
        },
      },
    }
    this.axiosClient = new AxiosClient(this.apiConfig)
  }


  private getStandardTimezone(){

    let originDate = new Date().toLocaleDateString("en-us", {
        timeZoneName : "long",
        day : "numeric"
    })

    let strTimezone = originDate.split(",")[1].trim()

    return strTimezone != ""  ? strTimezone : ""

}


  private send<T>(method: HttpMethods, option?: EumApiCallOptions): Promise<T> {
    const { url, data, params, headers } = option || {}
    const invalidatedUrl: string = this.invalidateUrl(url || '')
    const promise = this.axiosClient.send<EumApiDataMoldel<T>>(
      method,
      invalidatedUrl,
      data,
      params,
      headers
    )

    return promise.then<T>((data) => {
      if (data === undefined) {
        debugger
      }
      /*
          2021-10-20
          단일파일 업로드인경우 (/file/store/flow) Uncaught (in promise) undefined 에러 발생
          예외처리
          return Data {
              contentType: null
              serializerSettings: null
              statusCode: null
              value: {{resultData}}
          }
          */
      if ((data as any).value) {
        return (data as any).value;
      }

      if (data && data.code === 'ok') {
        return data.result
      } else {
        throw (data || {}).error
      }
    })
  }

  get<T>(option?: EumApiCallOptions): Promise<T> {
    return this.send<T>(HttpMethods.GET, option)
  }

  delete<T>(option?: EumApiCallOptions): Promise<T> {
    return this.send<T>(HttpMethods.DELETE, option)
  }

  put<T>(option?: EumApiCallOptions): Promise<T> {
    return this.send<T>(HttpMethods.PUT, option)
  }

  post<T>(option?: EumApiCallOptions): Promise<T> {
    return this.send<T>(HttpMethods.POST, option)
  }

  patch<T>(option?: EumApiCallOptions): Promise<T> {
    return this.send<T>(HttpMethods.PATCH, option)
  }

  invalidateUrl(path: string): string {
    var rootUrl = this.apiConfig.rootUrl
    var url = ''
    if (path.startsWith('/')) {
      url = rootUrl + path
    } else if (path.startsWith('http://') || path.startsWith('https://')) {
      url = path
    } else {
      url = path
    }
    return url
  }
}
export default EumApiClient
