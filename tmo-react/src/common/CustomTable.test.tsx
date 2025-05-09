import { render, screen } from '@testing-library/react';
import CustomTable from './CustomTable';
import { TableColumn } from '../types/TableColumn';

interface Data {
    name: string;
    totalPrice: number;
}

const columns: TableColumn<Data>[] = [
    { header: 'Name', accessor: 'name' },
    { header: 'Total Price', accessor: 'totalPrice' },
];

const data: Data[] = [
    { name: 'Seller 1', totalPrice: 250 },
    { name: 'Seller 2', totalPrice: 300 },
];

jest.mock('@chakra-ui/react', () => ({
    Table: ({ children }: any) => <table data-testid="custom-table">{children}</table>,
    Thead: ({ children }: any) => <thead>{children}</thead>,
    Tbody: ({ children }: any) => <tbody>{children}</tbody>,
    Tr: ({ children }: any) => <tr>{children}</tr>,
    Th: ({ children }: any) => <th>{children}</th>,
    Td: ({ children }: any) => <td>{children}</td>,
}));

describe('CustomTable', () => {
    test('renders table headers', () => {
        render(<CustomTable columns={columns} data={data} />);
        expect(screen.getByText('Name')).toBeInTheDocument();
        expect(screen.getByText('Total Price')).toBeInTheDocument();
    });

    test('renders table rows', () => {
        render(<CustomTable columns={columns} data={data} />);
        expect(screen.getByText('Seller 1')).toBeInTheDocument();
        expect(screen.getByText('250')).toBeInTheDocument();
        expect(screen.getByText('Seller 2')).toBeInTheDocument();
        expect(screen.getByText('300')).toBeInTheDocument();
    });
});