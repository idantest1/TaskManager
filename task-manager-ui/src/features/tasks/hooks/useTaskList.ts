import { useGetTasksQuery } from '../api/taskApi';
import { useErrorHandler } from '../../../hooks/useErrorHandler';

export function useTaskList() {
  const { data: tasks, isLoading, error } = useGetTasksQuery();

  useErrorHandler(error, 'Failed to load tasks');

  return {
    tasks,
    isLoading,
  };
}
