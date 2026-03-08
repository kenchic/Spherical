// Previene el envío automático del formulario al presionar Enter

export async function attachPreventEnterSubmit() {
    console.log('incio');
    document.body.addEventListener('keypress', e => {
        if (e.which == 13 && !e.target.matches('button[type=submit]')) {
            const form = e.composedPath().find(el => el.matches && el.matches('form'));
            if (form) {
                e.preventDefault();
                return false;
            }
        }
    });
    console.log('fin');
}