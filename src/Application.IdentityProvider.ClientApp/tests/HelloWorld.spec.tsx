import { expect, test } from 'vitest';
import { render } from 'vitest-browser-react';
import App from '../src/App';

test('renders name', async () => {
  const { getByText, getByRole } = render(<App />);

  await expect.element(getByText('Vite and React')).toBeInTheDocument();
  await getByRole('button', { name: 'count is' }).click();

  await expect.element(getByText('count is 1')).toBeInTheDocument();
});