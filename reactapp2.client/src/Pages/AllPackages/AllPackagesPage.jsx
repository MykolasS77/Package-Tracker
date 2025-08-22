import { useEffect, useState } from 'react';
import BootstrapButton from '../../Components/BootstrapButton';
import RecipientOrSenderDetails from '../PackageDetails/RecipientOrSenderDetails';
import StatusDropdownButton from '../../Components/StatusDropDownButton'
import FilterDropdown from './FilterDropown'
import AllPackagesList from './AllPackagesTable';




function PackageList() {
    const [packages, setPackages] = useState([]);


    useEffect(() => {
        getPackageInformation();
    }, []);

    const contents = packages === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <>
            <div className="d-flex align-items-center text-center justify-content-start">
                <FilterDropdown filterPackages={filterPackages} />
            </div>
            <AllPackagesList packages={packages} />

        </>

    return (
        <div>
            {contents}
        </div>
    );

    async function getPackageInformation() {
        const response = await fetch('api/packageinformation')
        if (response.ok) {

            const data = await response.json();
            setPackages(data)

        }
    }




    async function filterPackages(filter) {

        let url = 'api/packageinformation/filterpackages/' + filter

        const response = await fetch(url)
        if (response.ok) {

            const data = await response.json();
            setPackages(data)

        }
    }


}

export default PackageList;