import { useEffect, useRef } from 'react';
import { toast } from 'react-toastify';

export function useErrorHandler(error: unknown, fallbackMessage = 'An error occurred') {
  const shown = useRef(false);

  useEffect(() => {
    if (!error || shown.current) return;

    shown.current = true;

    let message = fallbackMessage;

    if (
      typeof error === 'object' &&
      error !== null &&
      'status' in error &&
      ('error' in error || 'data' in error)
    ) {
      const err = error as {
        status: number;
        error?: string;
        data?: { message?: string; error?: string };
      };

      message =
        err?.data?.message ||
        err?.data?.error ||
        err?.error ||
        `Unexpected error (status ${err.status})`;
    }

    else if (error instanceof Error) {
      message = error.message;
    }

    toast.error(message);
  }, [error, fallbackMessage]);
}
