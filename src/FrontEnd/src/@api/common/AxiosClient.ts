import axios, {AxiosRequestConfig, AxiosInstance} from 'axios';
import {HttpMethods, EumApiConfig, EumApiRequestConfig} from './types';

export default class AxiosClient {
  private axiosClient: AxiosInstance;

  constructor(config: EumApiConfig) {
    /**
     * A common base instance
     * 이 인스턴스를 통해 요청 할 경우 /api 가 자동으로 붙는다.
     * 또한, 모든 요청에 인증에 필요한 토큰이나 헤더가 포함된다.
     */
    this.axiosClient = axios.create(config.requestConfig);

    /**
     * 요청을 중간에 인터셉트하여 비즈 로직을 수행한다.
     */
    if (config.interceptors?.request) {
      this.axiosClient.interceptors.request.use(
        config.interceptors.request.onFulfilled,
        config.interceptors.request.onRejected
      );
    }

    /**
     * 응답을 중간에 인터셉트하여 비즈로직을 수행한다.
     */
    if (config.interceptors?.response) {
      this.axiosClient.interceptors.response.use(
        config.interceptors.response.onFulfilled,
        config.interceptors.response.onRejected
      );
    }
  }

  /**
   * 요청에 맞는 데이터로 렙핑합니다.
   * @param {*} method
   * @param {*} vlaue
   */
  wrap(method: HttpMethods, data: any, params: any): AxiosRequestConfig {
    //@ts-ignore
    return method === HttpMethods.GET ? {params: params} : {data: data};
  }

  /**
   * 요청을 보내고 응답을 우선 처리합니다.
   * @param {*} method
   * @param {String} url
   * @param {*} data
   */
  send<T>(
    method: HttpMethods,
    url: string,
    userData?: object,
    userParams?: object,
    headerValue?: any
  ): Promise<T> {
    const data = this.wrap(method, userData, userParams);
    // IE에서 GET 호출은 항상 브라우저 캐시하므로 이를 우회하기 위해 URL에 랜덤 version 추가
    // console.log('method-GET 1', url);
    if (method === HttpMethods.GET) {
      url += `${url.includes('?') ? '&' : '?'}v=${Math.random()}`;
      // console.log('method-GET 2', url);
    }
    var config: AxiosRequestConfig = {
      method: method,
      url: url,
      //@ts-ignore
      headers: headerValue || {},
      ...data
    };
    return this.axiosClient.request(config).then<T>((res) => {
      if (res === undefined) {
        debugger;
      }
      const data = JSON.parse(JSON.stringify(res.data));
      return data;
    });
  }
}
