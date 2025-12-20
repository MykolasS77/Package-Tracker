
export default async function changePackageStatusRequest(props) {
    const response = await fetch('/api/statushistory', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            "status": props.action,
            "packageRef": props.packageRef
        })

    })

    return response

}

