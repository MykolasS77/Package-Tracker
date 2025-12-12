import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { useState } from 'react';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';


function renderErrorMessages(errorMessagesDict) {
    
    const errorMessages = Object.keys(errorMessagesDict).map((obj) => {
        return (
            <div className="text-warning mt-3">
                {errorMessagesDict[obj]}
            </div>
        )
    })
    return errorMessages
}
        
            

function CreateNewItemForm() {

    const [message, setMessage] = useState(null)
    const [messageColour, setMessageColour] = useState(null)
    const [errorMessages, setErrorMessages] = useState([])

    async function createNewPackage(formData) {


        const confirmBox = window.confirm(
            "Do you want to add a new package?"
        )

        if (confirmBox === false) {
            setMessage("Package added successfully!")
            return
        }


        const senderFirstName = formData.get("senderFirstName").trim();
        const senderLastName = formData.get("senderLastName").trim();
        const senderAddress = formData.get("senderAddress");
        const senderPhone = formData.get("senderPhone").trim();
        const recipientFirstName = formData.get("recipientFirstName").trim();
        const recipientLastName = formData.get("recipientLastName").trim();
        const recipientAddress = formData.get("recipientAddress");
        const recipientPhone = formData.get("recipientPhone").trim();



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
                    "address": senderAddress,
                    "phone": senderPhone
                },
                "recipient": {
                    "firstName": recipientFirstName,
                    "lastName": recipientLastName,
                    "address": recipientAddress,
                    "phone": recipientPhone
                },
                "currentStatus": "Created"
            })

        })
        if (response.ok) {
            setMessage("Package added successfully!")
            setMessageColour("success")
            setErrorMessages([])
            console.log("text-" + messageColour + " mt-3");
            console.log(messageColour);

        }
        else {
            const errorData = await response.json();
            setMessage(errorData["title"])
            setMessageColour("danger")
            setErrorMessages([errorData["errors"]])




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
                    <Form.Control maxlength="50" placeholder="Enter sender's address" name="senderAddress" />
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
                    <Form.Control maxlength="50" placeholder="Enter recipient's address" name="recipientAddress" />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Control maxlength="12" placeholder="Enter recipient's phone number" name="recipientPhone" />
                </Form.Group>

            </Row>

            <Button variant="light" type="submit" >
                Submit
            </Button>

            {message ? <p className={"text-" + messageColour + " mt-3"}>{message}</p> : null}

            {errorMessages.length > 0 ? renderErrorMessages(errorMessages[0]) : null}


        </Form>


    );
}

export default CreateNewItemForm