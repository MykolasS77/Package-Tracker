
export default async function filterByPackageStatusRequest(statusFilter) {

    let url = 'api/filterpackages/' + statusFilter

    const response = await fetch(url)

    if (response.ok) {

        const data = await response.json();
        return data

    }
}