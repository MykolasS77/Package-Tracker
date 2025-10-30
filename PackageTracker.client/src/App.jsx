
import CustomNavBar from './Components/CustomNavBar.jsx';
import PackageDetailsPage from './Pages/PackageDetails/PackageDetailsPage.jsx';
import PackageListPage from './Pages/AllPackages/AllPackagesPage.jsx';
import CreateNewItemForm from './Pages/AllPackages/CreateNewItemForm.jsx';
import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

function App() {

    return (
            <Router>
                <CustomNavBar />
                <Routes>
                    <Route path="/" element={<PackageListPage />} />
                    <Route path="/new-package" element={<CreateNewItemForm />} />
                    <Route path="/view-details/:id" element={<PackageDetailsPage />} />
                </Routes>
            </Router>
    );

}

export default App;