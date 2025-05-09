import { Select } from '@chakra-ui/react';

interface DropdownProps {
    options: string[];
    onSelect: (selectedValue: string) => void;
    selectedValue: string | null;
    placeHolder: string
}

const CustomDropdown = ({ options, onSelect, selectedValue, placeHolder }: DropdownProps) => {
    return (
        <Select width={'100%'} border={'1px solid'}
            placeholder={placeHolder}
            onChange={(e) => onSelect(e.target.value)}
            value={selectedValue || ''}
        >
            {options.map((option, index) => (
                <option key={index} value={option}>
                    {option}
                </option>
            ))}
        </Select>
    );
};

export default CustomDropdown;
