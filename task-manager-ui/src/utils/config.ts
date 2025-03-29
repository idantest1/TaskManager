export function getApiUrl(): string {
    return (import.meta.env.VITE_TASKMANAGER_API_URL as string);
  }
  