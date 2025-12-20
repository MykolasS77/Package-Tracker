export default async function getAllPackagesRequest() {
    const response = await fetch('api/getallpackages')
    if (response.ok) {

        const data = await response.json();
        return data

    }
}