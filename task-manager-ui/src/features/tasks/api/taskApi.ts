import { getApiUrl } from '../../../utils/config';
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { Task } from '../types/task'; // ודא שה-import הוא נכון

export const taskApi = createApi({
  reducerPath: 'taskApi',
  baseQuery: fetchBaseQuery({ baseUrl: getApiUrl(), }), // כאן תשתמש במשתנה סביבה שהגדרת
  endpoints: (builder) => ({
    getTasks: builder.query<Task[], void>({
      query: () => '/api/tasks', // שאילתא לקבלת כל המשימות
    }),
    getTask: builder.query<Task, number>({
      query: (taskId) => `/api/tasks/${taskId}`, // שאילתא לקבלת משימה לפי id
    }),
    createTask: builder.mutation<Task, Task>({
      query: (task) => ({
        url: '/api/tasks',
        method: 'POST',
        body: task,
      }),
    }),
    updateTask: builder.mutation<Task, { id: number; task: Task }>({
      query: ({ id, task }) => {
        console.log("Updating task:", task);
        return {
          url: `/api/tasks/${id}`,
          method: 'PUT',
          body: task,
        };
      },
    }),
    deleteTask: builder.mutation<void, number>({
      query: (id) => ({
        url: `/api/tasks/${id}`,
        method: 'DELETE',
      }),
    }),
  }),
});

export const { 
  useGetTasksQuery, 
  useGetTaskQuery, 
  useCreateTaskMutation, 
  useUpdateTaskMutation, 
  useDeleteTaskMutation 
} = taskApi;
