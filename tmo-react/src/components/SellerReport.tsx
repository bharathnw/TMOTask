import { useQuery } from '@tanstack/react-query';
import { getSellerPerformanceReport, getSellerPerformanceReportWithBranch } from '../services/sellerReportService';
import CustomTable from '../common/CustomTable';
import { SellerReport as SellerReportModel } from '../types/SellerReport';
import { TableColumn } from '../types/TableColumn';
import { Box, Heading, HStack } from '@chakra-ui/react';
import LineChart from '../common/LineChart';
import { useState } from 'react';
import { getBranches } from '../services/branchesService';
import CustomDropdown from '../common/Dropdown';

const SellerReport = () => {

    const [selectedBranch, setSelectedBranch] = useState<string | null>(null);

    const { data: branches = [] } = useQuery({
        queryKey: ['branches'],
        queryFn: getBranches,
        staleTime: 2 * 60 * 1000,
        refetchOnWindowFocus: false
    });

    const { data = [] } = useQuery<SellerReportModel[]>({
        queryKey: ['sellerReport', selectedBranch],
        queryFn: () => selectedBranch ? getSellerPerformanceReportWithBranch(selectedBranch) : getSellerPerformanceReport(),
        staleTime: 2 * 60 * 1000,
        refetchOnWindowFocus: false
    });

    const columns: TableColumn<SellerReportModel>[] = [
        { header: 'Seller', accessor: 'name' },
        { header: 'Month', accessor: 'month' },
        { header: 'Total Orders', accessor: 'totalOrders' },
        { header: 'Total Price', accessor: 'totalPrice' },
    ];

    const monthlySales = Array.from(data.reduce((map, curr) => {
        if (map.has(curr.month)) {
            map.set(curr.month, map.get(curr.month)! + curr.totalOrders);
        } else {
            map.set(curr.month, curr.totalOrders);
        }
        return map;
    }, new Map<string, number>()).entries()).map(([name, value]) => ({ name, value }));


    return (
        <>
            <HStack spacing={8} justify="space-between" width="100%" mb={5}>
                <Box>
                    <Heading fontSize={'30px'}>Dashboard</Heading>

                </Box>
                <Box width={'200px'}>
                    <CustomDropdown
                        options={branches} placeHolder="Select Branch"
                        onSelect={(val) => setSelectedBranch(val)}
                        selectedValue={selectedBranch}
                    />
                </Box>

            </HStack>

            <Box width="100%" height={300} mb={6} borderRadius={10} border="1px solid"
                borderColor="gray.300"
                boxShadow="md"
                bg="white" p={6}>
                <LineChart data={monthlySales} />
            </Box>

            <Box >
                <CustomTable columns={columns} data={data} />
            </Box>

        </>
    )
};

export default SellerReport;