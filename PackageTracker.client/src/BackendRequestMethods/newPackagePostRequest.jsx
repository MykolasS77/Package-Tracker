export default async function newPackagePostRequest(props) {
    const response = await fetch('/api/createpackage', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            "sender": {
                "firstName": props.senderFirstName,
                "lastName": props.senderLastName,
                "address": props.senderAddress,
                "phone": props.senderPhone
            },
            "recipient": {
                "firstName": props.recipientFirstName,
                "lastName": props.recipientLastName,
                "address": props.recipientAddress,
                "phone": props.recipientPhone
            },
            "currentStatus": "Created"
        })
    })

    return response
    
}