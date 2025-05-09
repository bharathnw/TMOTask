
import { TableColumn } from '../types/TableColumn';
import {
  Table,
  Thead,
  Tbody,
  Tr,
  Th,
  Td
} from '@chakra-ui/react'

interface CustomTableProps<T> {
  columns: TableColumn<T>[];
  data: T[];
}

function CustomTable<T>({ columns, data }: CustomTableProps<T>) {
  return (
    <Table variant="striped" colorScheme="gray" border="1px solid"
      borderColor="gray.300"
      boxShadow="md"
      bg="white">
      <Thead bg={'gray.600'} >
        <Tr height={'40px'}>
          {columns.map((col, index) => (
            <Th fontSize={'15px'} color={'whiteAlpha.900'} key={index}>{col.header}</Th>
          ))}
        </Tr>
      </Thead>
      <Tbody>
        {data.map((row, rowIndex) => (
          <Tr key={rowIndex}>
            {columns.map((col, colIndex) => (
              <Td key={colIndex}>{String(row[col.accessor])}</Td>
            ))}
          </Tr>
        ))}
      </Tbody>
    </Table>
  );
}

export default CustomTable;
