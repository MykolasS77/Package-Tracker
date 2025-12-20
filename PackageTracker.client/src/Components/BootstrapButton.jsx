import Button from 'react-bootstrap/Button';

function BootstrapButton({ content, variant="primary", href="", onClick=null }) {
    return (
        <>
            <Button variant={variant} href={href} onClick={onClick}>{content}</Button>
        </>
    );
}

export default BootstrapButton;