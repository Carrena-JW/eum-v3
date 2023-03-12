import axios from '@axios';

export interface StepParams {

}

export interface StepData {
  id?: string;
  name: string;
}

export default {
  get(params: StepParams): Promise<StepData[]> {
    return axios.get('/admin/step', {params}).then(res => res.data);
  },
  delete(id: string): Promise<boolean> {
    return axios.delete('/admin/step', {params: {id: id}});
  },
  save(item: StepData): Promise<StepData> {
    return axios.post('/admin/step', item);
  }
};
