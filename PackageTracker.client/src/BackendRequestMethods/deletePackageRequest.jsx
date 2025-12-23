export default async function deletePackage(packageId) {

    const confirmBox = window.confirm(
        "Do you want to delete this package?"
    )

    if (confirmBox === false) {
        alert("Package status deletion canceled")
        return
    }

    const response = await fetch('/api/deletepackage/' + packageId, {
        method: 'DELETE',
    })
    if (response.ok) {


        alert("Package deleted successfully")
        window.location.reload();

    }
}
