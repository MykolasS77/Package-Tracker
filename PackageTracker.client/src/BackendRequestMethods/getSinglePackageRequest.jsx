export default async function getSinglePackageRequest(id) {

    let url = "/api/getsinglepackage/" + id

    const response = await fetch(url)
    if (response.ok) {

        const data = await response.json();

        return data
    }
    else {
        alert("Package not found.")
        window.location.href = "/"
    }
}