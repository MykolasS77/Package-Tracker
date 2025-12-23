import Dropdown from 'react-bootstrap/Dropdown';
import changePackageStatusRequest from '../BackendRequestMethods/changePackageStatusRequest';

function StatusDropdownButton({ currentStatus, packageRef, variant = "transparent", text }) {

    function dropdownItem(action) {
        return <Dropdown.Item onClick={() => changePackageStatus(action, packageRef)}>{action}</Dropdown.Item>
    }

    async function changePackageStatus(action, packageRef) {

        const confirmBox = window.confirm(
            "Do you want to change package status?"
        )

        if (confirmBox === false) {
            
            alert("Package status change canceled")
            return
        }

        const response = await changePackageStatusRequest({ action, packageRef })
        if (response.ok) {

            
            alert("Status changed successfully")
            window.location.reload();

        }

    }

    function returnDropDownItems(currentStatus) {

        

        if (currentStatus == "Created") {
            return (<>
                {dropdownItem("Sent")}
                {dropdownItem("Canceled")}
            </>)
        }
        if (currentStatus == "Sent") {
            return (<>
                {dropdownItem("Accepted")}
                {dropdownItem("Returned")}
                {dropdownItem("Canceled")}
            </>)
        }
        if (currentStatus == "Returned") {
            return (<>
                {dropdownItem("Sent")}
                {dropdownItem("Canceled")}
            </>)
        }
        if (currentStatus == "Accepted") {
            return <Dropdown.Item disabled >Status cannot be changed</Dropdown.Item>
        }
        if (currentStatus == "Canceled") {
            return <Dropdown.Item disabled >Status cannot be changed</Dropdown.Item>
        }
    }

    return (
        <Dropdown>
            <Dropdown.Toggle variant={variant} id="dropdown-basic">
                {text}
            </Dropdown.Toggle>

            <Dropdown.Menu>
                {returnDropDownItems(currentStatus)}
            </Dropdown.Menu>
        </Dropdown>
    );
}

export default StatusDropdownButton;