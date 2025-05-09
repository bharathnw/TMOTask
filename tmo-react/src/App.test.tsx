import { render, screen } from '@testing-library/react';
import App from './App';

jest.mock('./components/SellerReport', () => () => <div>SellerReport</div>);
jest.mock('./common/Loader', () => () => <div>Loader</div>);
jest.mock('./common/Navbar', () => () => <div>Navbar</div>);

jest.mock('@chakra-ui/react', () => ({
  Box: ({ children }: any) => <div>{children}</div>,
}));

describe('App Component', () => {
  it('Navbar, Loader, SellerReport', () => {
    render(<App />);

    expect(screen.getByText('Navbar')).toBeInTheDocument();
    expect(screen.getByText('Loader')).toBeInTheDocument();
    expect(screen.getByText('SellerReport')).toBeInTheDocument();
  });
});