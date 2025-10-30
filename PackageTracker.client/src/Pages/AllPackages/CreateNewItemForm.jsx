import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { useState } from 'react';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';


function CreateNewItemForm() {

    const [message, setMessage] = useState(null)

    async function createNewPackage(formData) {


        const confirmBox = window.confirm(
            "Do you want to add a new package?"
        )

        if (confirmBox === false) {
            console.log("Package creation canceled")
            return
        }


        const senderFirstName = formData.get("senderFirstName");
        const senderLastName = formData.get("senderLastName");
        const senderAdress = formData.get("senderAdress");
        const senderPhone = formData.get("senderPhone");
        const recipientFirstName = formData.get("recipientFirstName");
        const recipientLastName = formData.get("recipientLastName");
        const recipientAdress = formData.get("recipientAdress");
        const recipientPhone = formData.get("recipientPhone");

        const response = await fetch('/api/packageinformation', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                "sender": {
                    "firstName": senderFirstName,
                    "lastName": senderLastName,
                    "adress": senderAdress,
                    "phone": senderPhone
                },
                "recipient": {
                    "firstName": recipientFirstName,
                    "lastName": recipientLastName,
                    "adress": recipientAdress,
                    "phone": recipientPhone
                },
                "currentStatus": "Created"
            })

        })
        if (response.ok) {

            alert("Package added successfully!")
            setMessage("Package added successfully!")

        }
    }

    return (
        <Form action={createNewPackage}>

            <Row>



                <Form.Label>Sender's Information</Form.Label>

                <Form.Group as={Col} className="mb-3">
                    <Form.Control maxlength="30" placeholder="Enter sender's first name" name="senderFirstName" />
                </Form.Group>

                <Form.Group as={Col} className="mb-3">
                    <Form.Control maxlength="30" placeholder="Enter sender's last name" name="senderLastName" />
                </Form.Group>


                <Form.Group className="mb-3">
                    <Form.Control maxlength="50" placeholder="Enter sender's address" name="senderAdress" />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Control maxlength="12" placeholder="Enter sender's phone number" name="senderPhone" type="number" />
                </Form.Group>



                <Form.Label>Recipient's Information</Form.Label>

                <Form.Group as={Col} className="mb-3">
                    <Form.Control maxlength="30" placeholder="Enter recipient's first name" name="recipientFirstName" />
                </Form.Group>

                <Form.Group as={Col} className="mb-3">
                    <Form.Control maxlength="30" placeholder="Enter recipient's last name" name="recipientLastName" />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Control maxlength="50" placeholder="Enter recipient's address" name="recipientAdress" />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Control maxlength="12" placeholder="Enter recipient's phone number" name="recipientPhone" />
                </Form.Group>

            </Row>

            <Button variant="light" type="submit" >
                Submit
            </Button>

            {message ? <p className="text-success mt-3">{message}</p> : null}

        </Form>


    );
}

export default CreateNewItemForm