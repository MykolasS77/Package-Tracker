import Dropdown from 'react-bootstrap/Dropdown';

function RecipientOrSenderDetails({ firstName, lastName, phone, adress}) {
    return (
        <Dropdown>
            <Dropdown.Toggle variant="transparent" id="dropdown-basic">
                {firstName} {lastName }
            </Dropdown.Toggle>

            <Dropdown.Menu>
                <Dropdown.Item disabled >Phone number: { phone }</Dropdown.Item>
                <Dropdown.Item disabled >Address: { adress }</Dropdown.Item>
            </Dropdown.Menu>
        </Dropdown>
    );
}

export default RecipientOrSenderDetails;