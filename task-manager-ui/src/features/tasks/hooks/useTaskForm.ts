import { useForm } from 'react-hook-form';
import { Task } from '../types/task';
import {
  useCreateTaskMutation,
  useUpdateTaskMutation,
  useGetTaskQuery,
} from '../api/taskApi';
import { useEffect } from 'react';
import { useSuccessHandler } from '../../../hooks/useSuccessHandler';
import { useErrorHandler } from '../../../hooks/useErrorHandler';
import { useNavigate } from 'react-router-dom';

export function useTaskForm(taskId?: number) {
  const navigate = useNavigate();
  const isEditMode = !!taskId;

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors, isSubmitting },
  } = useForm<Task>();

  const { data: task, isLoading: isLoadingTask, error: taskError } = useGetTaskQuery(taskId!, {
    skip: !isEditMode,
  });

  const [createTask, { isSuccess: createSuccess, error: createError }] = useCreateTaskMutation();
  const [updateTask, { isSuccess: updateSuccess, error: updateError }] = useUpdateTaskMutation();

  useSuccessHandler(createSuccess || updateSuccess, 'Task saved successfully');
  useErrorHandler(taskError, 'Failed to load task');
  useErrorHandler(createError, 'Failed to create task');
  useErrorHandler(updateError, 'Failed to update task');

  useEffect(() => {
    if (task) {
      reset(task); 
    }
  }, [task, reset]);

  const onSubmit = async (data: Task) => {
    try {
      if (isEditMode && taskId) {
        await updateTask({ id: taskId, task:data }).unwrap();
      } else {
        await createTask(data).unwrap();
      }

      navigate('/');
    } catch {
      // error handled by useErrorHandler
    }
  };

  return {
    register,
    handleSubmit,
    errors,
    isSubmitting,
    isEditMode,
    isLoadingTask,
    onSubmit,
  };
}
