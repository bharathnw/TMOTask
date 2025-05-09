export interface TableColumn<T>{
    header: string
    accessor: keyof T
}