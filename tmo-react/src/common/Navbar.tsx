import {
    Box,
    Flex
} from '@chakra-ui/react'

export default function Navbar() {
    return (
        <>
            <Box bgGradient="linear(to-r, teal.500, green.500)" px={5}>
                <Flex h={16} alignItems={'center'} justifyContent={'space-between'}>
                    <Box fontSize={'25px'} fontWeight={'bold'}>TMO Seller</Box>
                </Flex>
            </Box>
        </>
    )
}