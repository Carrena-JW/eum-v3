import api from './http-client';

export interface SignInReply {
  token: string
  succeed: boolean
  errors: string[]
}

export default {
  login(username: string, password: string): Promise<SignInReply> {
    return api.post({
      url: '/Account/Auth/SignIn',
      data: {userName: username, password}
    });
  }
};
