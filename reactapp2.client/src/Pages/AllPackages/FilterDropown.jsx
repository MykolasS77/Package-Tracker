import { useEffect, useState } from 'react';
import BootstrapButton from '../../Components/BootstrapButton';
import RecipientOrSenderDetails from '../PackageDetails/RecipientOrSenderDetails';
import StatusDropdownButton from '../../Components/StatusDropDownButton'
import Dropdown from 'react-bootstrap/Dropdown';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';




function FilterDropdown(props) {
    return (
        <div className="d-flex align-items-center text-center justify-content-start">
            <Dropdown>
                <Dropdown.Toggle variant="light" id="dropdown-basic" className="my-3">
                    Filter based on status
                </Dropdown.Toggle>

                <Dropdown.Menu>
                    
                    <Dropdown.Item onClick={() => props.filterPackages("Created")}>Created</Dropdown.Item>
                    <Dropdown.Item onClick={() => props.filterPackages("Sent")}>Sent</Dropdown.Item>
                    <Dropdown.Item onClick={() => props.filterPackages("Returned")}>Returned</Dropdown.Item>
                    <Dropdown.Item onClick={() => props.filterPackages("Accepted")}>Accepted</Dropdown.Item>
                    <Dropdown.Item onClick={() => props.filterPackages("Canceled")}>Canceled</Dropdown.Item>
                </Dropdown.Menu>
            </Dropdown>


            <Form action={getPackage}>
                <div className="d-flex align-items-center text-center justify-content-start">

                    <Form.Group className="mx-3">
                        <Form.Control id="disabledTextInput" type="number" min="1" placeholder="Enter tracking number" name="id" />
                    </Form.Group>

                    <Button type="submit" variant="light">Search</Button>
                </div>
            </Form>

        </div>        
    )
            
    function getPackage(formData) {
        
        console.log("Get package from component")
        const id = formData.get("id");
        window.location.href += "view-details/" + id
    }

}

export default FilterDropdown;