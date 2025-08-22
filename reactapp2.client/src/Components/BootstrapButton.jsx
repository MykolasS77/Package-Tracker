import Button from 'react-bootstrap/Button';

function BootstrapButton({ content, variant="primary", href="" }) {
    return (
        <>
            <Button variant={variant} href={href}>{content}</Button>
        </>
    );
}

export default BootstrapButton;