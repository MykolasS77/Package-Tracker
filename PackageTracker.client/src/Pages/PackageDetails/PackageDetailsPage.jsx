import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import PackageDetailsTable from './PackageDetailsTable';
import getSinglePackageRequest from '../../BackendRequestMethods/getSinglePackageRequest'


function PackageDetails() {
    const [details, setDetails] = useState(null);
    const { id } = useParams();

    useEffect(() => {
        getPackage();
    });


    const contents = !details
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <PackageDetailsTable details={details} />;

    return (
        <div>
            {contents}
        </div>
    );

    async function getPackage() {

        const data = await getSinglePackageRequest(id)
        setDetails(data)
        
    }

}

export default PackageDetails;