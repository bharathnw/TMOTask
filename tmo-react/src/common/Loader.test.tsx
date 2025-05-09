import { render, screen } from '@testing-library/react';
import Loader from './Loader';

jest.mock('@chakra-ui/react', () => ({
    Spinner: () => <div role="status">Loading Spinner</div>,
    Center: ({ children }: any) => <div>{children}</div>,
    Text: ({ children }: any) => <div>{children}</div>,
    VStack: ({ children }: any) => <div>{children}</div>,
}));

jest.mock('@tanstack/react-query', () => ({
    useIsFetching: jest.fn(),
}));

describe('Loader Component', () => {
    it('Show Loader', () => {
        (require('@tanstack/react-query').useIsFetching as jest.Mock).mockReturnValue(1);

        render(<Loader />);

        expect(screen.getByText('Loading...')).toBeInTheDocument();
        expect(screen.getByRole('status')).toBeInTheDocument();
    });

    it('Hide Loader', () => {
        (require('@tanstack/react-query').useIsFetching as jest.Mock).mockReturnValue(0);

        render(<Loader />);

        expect(screen.queryByText('Loading...')).not.toBeInTheDocument();
        expect(screen.queryByRole('status')).not.toBeInTheDocument();
    });
});