import { AxiosRequestConfig, AxiosResponse } from 'axios';

export interface EumApiRequestConfig extends AxiosRequestConfig {}
export interface EumApiResponse<T> extends AxiosResponse<T> {}

export type EumApiInterceptors = {
    request?: {
        onFulfilled: (value: EumApiRequestConfig) => EumApiRequestConfig;
        onRejected: (error: any) => any;
    };
    response?: {
        onFulfilled: (value: EumApiResponse<any>) => EumApiResponse<any>;
        onRejected: (error: any) => any;
    };
};

export type EumApiCallOptions = {
    code?: string;
    url?: string;
    data?: object;
    params?: object;
    headers?: object;
    async?: boolean;
};

export type EumApiDataMoldel<T> = {
    result: T;
    code: 'ok' | string;
    message: string;
    error: string | null;
};

export type RequestData = {
    data: object;
    params: object;
};

export enum HttpMethods {
    GET = 'get',
    PUT = 'put',
    POST = 'post',
    DELETE = 'delete',
    PATCH = 'patch',
}

export type EumApiConfig = {
    rootUrl?: string;
    timeout?: number;
    requestConfig?: EumApiRequestConfig;
    interceptors?: EumApiInterceptors;
};
