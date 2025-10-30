import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import PackageDetailsTable from './PackageDetailsTable';


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

        let url = "/api/packageinformation/" + id
        
        const response = await fetch(url)
        if (response.ok) {

            const data = await response.json();

            setDetails(data)
        }
        else {
            alert("Package not found.")
            window.location.href = "/" 
        }
    }

}

export default PackageDetails;