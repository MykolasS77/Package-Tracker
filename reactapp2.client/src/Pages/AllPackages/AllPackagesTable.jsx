
import BootstrapButton from '../../Components/BootstrapButton';
import RecipientOrSenderDetails from '../PackageDetails/RecipientOrSenderDetails';
import StatusDropdownButton from '../../Components/StatusDropDownButton'

function AllItems({ packages }) {
    return (
        <table className="table table-striped " aria-labelledby="tableLabel">
            <thead>
                <tr>

                    <th>Tracking Number </th>
                    <th>Status</th>
                    <th>Sender</th>
                    <th>Recipient</th>
                    <th>Creation Date</th>
                    <th>Package Details</th>

                </tr>
            </thead>
            {packages.map(pkg => (
                <thead key={pkg.id}>
                    <tr className="align-middle">
                        <th> {pkg.id}</th>
                        <th><StatusDropdownButton currentStatus={pkg.currentStatus} packageRef={pkg.id} text={pkg.currentStatus} variant="light" /></th>
                        <th><RecipientOrSenderDetails firstName={pkg.sender.firstName} lastName={pkg.sender.lastName} adress={pkg.sender.adress} phone={pkg.sender.phone} /></th>
                        <th><RecipientOrSenderDetails firstName={pkg.recipient.firstName} lastName={pkg.recipient.lastName} adress={pkg.recipient.adress} phone={pkg.recipient.phone} /></th>
                        <th>{pkg.timeStampHistories[0].dateOfThisStatus}</th>
                        <th><BootstrapButton content={"View"} href={"/view-details/" + pkg.id} variant="light" /></th>

                    </tr>
                </thead>
            ))}

        </table >

    )



}

export default AllItems;