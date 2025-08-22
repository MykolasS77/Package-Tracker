import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { useState } from 'react';


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

            alert("Package added successfuly!")
            setMessage("Package added successfuly!")

        }
    }

    return (
        <Form action={createNewPackage}>
            <Form.Group className="mb-3">
                <Form.Label>Sender's' First Name</Form.Label>
                <Form.Control placeholder="Enter sender's first name" name="senderFirstName" />
            </Form.Group>
            <Form.Group className="mb-3">
                <Form.Label>Sender's' Last Name</Form.Label>
                <Form.Control placeholder="Enter sender's last name" name="senderLastName" />
            </Form.Group>
            <Form.Group className="mb-3">
                <Form.Label>Sender's Adress</Form.Label>
                <Form.Control placeholder="Enter sender's adress" name="senderAdress" />
            </Form.Group>
            <Form.Group className="mb-3">
                <Form.Label>Sender's Phone</Form.Label>
                <Form.Control placeholder="Enter sender's phone" name="senderPhone" />
            </Form.Group>

            <Form.Group className="mb-3">
                <Form.Label>Recipient's First Name</Form.Label>
                <Form.Control placeholder="Enter recipient's first name" name="recipientFirstName" />
            </Form.Group>
            <Form.Group className="mb-3">
                <Form.Label>Recipient's Last Name</Form.Label>
                <Form.Control placeholder="Enter sender's last name" name="recipientLastName" />
            </Form.Group>
            <Form.Group className="mb-3">
                <Form.Label>Recipient's Adress</Form.Label>
                <Form.Control placeholder="Enter recipient's adress" name="recipientAdress" />
            </Form.Group>
            <Form.Group className="mb-3">
                <Form.Label>Recipient's Phone</Form.Label>
                <Form.Control placeholder="Enter recipient's phone" name="recipientPhone" />
            </Form.Group>

            <Button variant="light" type="submit" >
                Submit
            </Button>
            {message ? <p className="text-success mt-3">{message}</p> : null}
        </Form>

        
    );
}

export default CreateNewItemForm