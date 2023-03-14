import api from './http-client';

export interface StepParams {

}

export interface StepData {
  id?: string;
  name: string;
}

export default {
  get(params: StepParams): Promise<StepData[]> {
    return api.get({
      url: '/ServiceDesk/step',
      params
    });
  },
  delete(id: string): Promise<boolean> {
    return api.delete({url: '/ServiceDesk/step', params: {id: id}});
  },
  save(item: StepData): Promise<StepData> {
    return api.post({url: '/ServiceDesk/step', data: item});
  }
};
