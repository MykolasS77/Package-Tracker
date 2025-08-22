
import { useParams } from 'react-router-dom';
import Table from 'react-bootstrap/Table';
import { useEffect, useState } from 'react';
function StatusHistory() {

    const [history, setHistory] = useState([]);
    const { id } = useParams();

    useEffect(() => {
        getStatusHistory();
    });

    const contents = history === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <Table striped bordered hover>
            <thead>
                <tr>
                    <th>Status</th>
                    <th>Timestamp</th>
                </tr>
            </thead>
            {history.map(pkg => (
                <tbody key={pkg.id}>
                    <tr className="align-middle">
                        <th> {pkg.status}</th>
                        <th> {pkg.dateOfThisStatus}</th>
                    </tr>
                </tbody>
            ))}
        </Table>

    async function getStatusHistory() {
        let targeturl = '/api/packageinformation/statushistory/' + id;

        const response = await fetch(targeturl)
        if (response.ok) {

            const data = await response.json();
          
            setHistory(data)

        }
    }

    return (
        <div>{contents}</div>
    )
}

export default StatusHistory;