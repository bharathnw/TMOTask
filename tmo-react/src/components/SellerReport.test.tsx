import { render, screen } from '@testing-library/react';
import SellerReport from './SellerReport';
import { useQuery } from '@tanstack/react-query';

jest.mock('@tanstack/react-query');
jest.mock('../services/sellerReportService', () => ({
    getSellerPerformanceReport: jest.fn(),
    getSellerPerformanceReportWithBranch: jest.fn(),
}));
jest.mock('../services/branchesService', () => ({
    getBranches: jest.fn(),
}));

jest.mock('@chakra-ui/react', () => ({
    Box: ({ children }: any) => <div>{children}</div>,
    Heading: ({ children }: any) => <h1>{children}</h1>,
    HStack: ({ children }: any) => <div>{children}</div>,
}));

jest.mock('../common/CustomTable', () => (props: any) => (
    <table>
        <thead>
            <tr>
                {props.columns.map((col: any) => (
                    <th key={col.header}>{col.header}</th>
                ))}
            </tr>
        </thead>
        <tbody>
            {props.data.map((row: any, i: number) => (
                <tr key={i}>
                    {props.columns.map((col: any) => (
                        <td key={col.accessor}>{row[col.accessor]}</td>
                    ))}
                </tr>
            ))}
        </tbody>
    </table>
));
jest.mock('../common/LineChart', () => () => (
    <div>LineChart Mock</div>
));
jest.mock('../common/Dropdown', () => ({ onSelect, options, placeHolder, selectedValue }: any) => (
    <div>
        <input
            placeholder={placeHolder}
            value={selectedValue || ''}
            onChange={(e) => onSelect(e.target.value)}
        />
        <div>
            {options?.map((opt: any) => (
                <div key={opt.id}>{opt.name}</div>
            ))}
        </div>
    </div>
));

describe('SellerReport', () => {
    const mockBranches = [
        { id: '1', name: 'Branch 1' },
        { id: '2', name: 'Branch 2' },
    ];

    const mockSellerData = [
        { name: 'Seller 1', month: 'January', totalOrders: 10, totalPrice: 1000 },
        { name: 'Seller 2', month: 'January', totalOrders: 15, totalPrice: 1500 },
        { name: 'Seller 1', month: 'February', totalOrders: 8, totalPrice: 800 },
    ];

    beforeEach(() => {
        (useQuery as jest.Mock).mockImplementation(({ queryKey }: { queryKey: string[] }) => {
            if (queryKey.includes('branches')) {
                return { data: mockBranches, isLoading: false };
            }
            return { data: mockSellerData, isLoading: false };
        })
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    it('initial rendering', () => {
        render(<SellerReport />);

        expect(screen.getByText('Dashboard')).toBeInTheDocument();
        expect(screen.getByPlaceholderText('Select Branch')).toBeInTheDocument();
        expect(screen.getByTestId('custom-table')).toBeInTheDocument();
        expect(screen.getByTestId('line-chart')).toBeInTheDocument();
    });

    it('aggregates the data to display the line chart', () => {
        render(<SellerReport />);
        expect(screen.getByTestId('line-chart')).toBeInTheDocument();
    });

    it('renders seller data in the table', () => {
        render(<SellerReport />);

        const januaryCells = screen.getAllByText('January');
        expect(januaryCells.length).toBe(2);

        const seller1Cells = screen.getAllByText('Seller 1');
        expect(seller1Cells.length).toBe(2);

        expect(screen.getByText('February')).toBeInTheDocument();
        expect(screen.getByText('10')).toBeInTheDocument();
        expect(screen.getByText('1000')).toBeInTheDocument();
    });

    it('fetches all seller data when no branch is selected', () => {
        render(<SellerReport />);

        const seller1Instances = screen.getAllByText('Seller 1');
        const seller2Instances = screen.getAllByText('Seller 2');

        expect(seller1Instances.length).toBe(2);
        expect(seller2Instances.length).toBe(1);
    });
});