import { useEffect, useState } from 'react';
import FilterDropdown from './FilterDropown'
import AllPackagesList from './AllPackagesTable';
import getAllPackagesRequest from '../../BackendRequestMethods/getAllPackagesRequest';
import filterByPackageStatusRequest from '../../BackendRequestMethods/filterByPackageStatusRequest';

function PackageList() {
    const [packages, setPackages] = useState([]);

    useEffect(() => {
        displayAllPackages();
    }, []);

    const contents = packages === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <>
            <div className="d-flex align-items-center text-center justify-content-start">
                <FilterDropdown filterPackages={displayFilteredPackages} />
            </div>
            <AllPackagesList packages={packages} />

        </>

    return (
        <div>
            {contents}
        </div>
    );

    async function displayAllPackages() {
        const data = await getAllPackagesRequest();
        setPackages(data)
    }

    async function displayFilteredPackages(statusFilter) {
        const data = await filterByPackageStatusRequest(statusFilter)
        setPackages(data)
    }

}

export default PackageList;