export default [
  {
    title: 'Admin',
    icon: { icon: 'mdi-package-variant' },
    children: [
      {
        title: 'Step',
        icon: { icon: 'mdi-file-document-outline' },

        children: [
          { title: 'List', to: 'admin-step-list', },
          // { title: 'Calendar', to: 'case-calendar', },
          // { title: 'Preview', to: { name: 'case-preview-id', params: { id: '5036' } } },
          // { title: 'Edit', to: { name: 'case-edit-id', params: { id: '5036' } } },
          // { title: 'Add', to: 'case-add' },
          // {
          //   title: 'User',
          //   children: [
          //     {title: 'List', to: 'case-user-list' },
          //     { title: 'View', to: { name: 'case-user-view-id', params: { id: 21 } } },
          //   ]
          // }
        ],
      },
    ],
  },
]
