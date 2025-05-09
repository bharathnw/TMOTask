import { Spinner, Center, Text, VStack } from '@chakra-ui/react';
import { useIsFetching } from '@tanstack/react-query';

const Loader = () => {
  const isFetching = useIsFetching();
  if (!isFetching) return null;

  return (
    <Center pos="fixed" top="0" left="0" w="100vw" h="100vh" zIndex={9999}>
      <VStack>
        <Spinner color="teal.500" borderWidth="4px" size="xl" />
        <Text color="teal.500">Loading...</Text>
      </VStack>
    </Center>
  );
};

export default Loader;