import Table from 'react-bootstrap/Table';
import StatusDropdownButton from '../../Components/StatusDropDownButton'
import StatusHistory from './StatusHistory';


function PackageDetailsTable({ details }) {

    return (
        <>
            <h1>Package number: {details.id}</h1>

            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>Current Status</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <th><StatusDropdownButton currentStatus={details.currentStatus} packageRef={details.id} text={details.currentStatus} /></th>
                    </tr>
                </tbody>
            </Table>


            <h2>Sender information</h2>
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Adress</th>
                        <th>Phone Number</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>{details.sender.firstName}</td>
                        <td>{details.sender.lastName}</td>
                        <td>{details.sender.adress}</td>
                        <td>{details.sender.phone}</td>
                    </tr>
                </tbody>
            </Table>

            <h2>Recipient information</h2>
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Adress</th>
                        <th>Phone Number</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>{details.recipient.firstName}</td>
                        <td>{details.recipient.lastName}</td>
                        <td>{details.recipient.adress}</td>
                        <td>{details.recipient.phone}</td>
                    </tr>
                </tbody>
            </Table>

            <h2>Status History</h2>
            <StatusHistory />

        </>
    );




}

export default PackageDetailsTable;