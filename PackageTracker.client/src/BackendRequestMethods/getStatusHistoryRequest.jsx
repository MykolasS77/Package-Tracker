export default async function getStatusHistoryRequest(id) {
    let targeturl = '/api/statushistory/' + id;

    const response = await fetch(targeturl)
    if (response.ok) {

        const data = await response.json();

        return data;

    }
}