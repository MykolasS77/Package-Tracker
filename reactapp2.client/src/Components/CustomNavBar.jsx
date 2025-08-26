import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';


function CustomNavBar() {
    return (

        <Navbar expand="lg" className="bg-body-tertiary">

            <Navbar.Brand href="/">PackageTracker</Navbar.Brand>
            <Navbar.Toggle aria-controls="basic-navbar-nav" />
            <Navbar.Collapse id="basic-navbar-nav">
                <Nav className="me-auto">
                    <Nav.Link href="/">View all packages</Nav.Link>
                    <Nav.Link href="/new-package">Create a new package</Nav.Link>
                </Nav>
            </Navbar.Collapse>

        </Navbar>

    );
}

export default CustomNavBar;