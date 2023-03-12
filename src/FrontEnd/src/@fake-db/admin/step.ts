// 👉 Get Client
import mock from '@/@fake-db/mock';
import type {StepData} from '@api/step';

var database: StepData[] = [
  {
    id: '1',
    name: 'test'
  }
];

mock.onGet('/admin/step').reply(() => {
  return [200, database];
});

mock.onDelete('/admin/step').reply((config) => {
  let result = false;
  const id = config.params.id as string;
  const idx = database.findIndex(x => x.id == id);
  if (idx >= 0) {
    database = [
      ...database.slice(0, idx),
      ...database.slice(idx + 1)
    ];
    result = true;
  }
  return [200, result];
});

mock.onPost('/admin/step').reply((config) => {
  const item = JSON.parse(config.data) as StepData;
  if (!item.id) {
    item.id = `${Date.now()}`;
  }
  database.push(item);
  return [200, item];
});

