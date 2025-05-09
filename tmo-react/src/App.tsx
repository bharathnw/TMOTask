import './App.css';
import { Box } from '@chakra-ui/react';
import Loader from './common/Loader';
import SellerReport from './components/SellerReport';
import Navbar from './common/Navbar';


function App() {
  return (
    <>
      <Navbar></Navbar>
      <Box p={6}>
        <Loader />
        <>
          <Box>
            <SellerReport />
          </Box>
        </>
      </Box>
    </>
  );
}

export default App;
