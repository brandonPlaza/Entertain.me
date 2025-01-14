﻿(() => {
    API.ExpectLoggedOut('../../');

    // Specific API endpoint wrapper: attempt to register a user.
    const Register = (email, username, password) => {
        return API.Post('Authentication/register', {
            email, password, username
        }).then(res => {
            return true;
        });
    };

    $_`#register`.addEventListener('click', () => {
        Register($_`#email`.value, $_`#username`.value, $_`#password`.value).then(e => {
            window.location.replace("http://127.0.0.1:5500/Frontend/auth/login/index.html");
        });
    });
})();
