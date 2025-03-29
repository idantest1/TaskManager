import { useEffect, useState } from 'react';

export function useLoadingHandler(...flags: boolean[]): boolean {
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    setLoading(flags.some(Boolean));
  }, [flags]);

  return loading;
}
