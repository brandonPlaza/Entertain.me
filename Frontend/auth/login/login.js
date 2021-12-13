(() => {
    API.ExpectLoggedOut('../../');
    
    // Specific API endpoint wrapper: attempt to log the user in.
    const LogIn = (email, password) => {
        return API.Post('Authentication/login', {
            email, password
        }).then(res => {
            window.localStorage.setItem("auth_token", res.token);
            return true;
        });
    };

    
    $_`#login`.addEventListener('click', () => {
        LogIn($_`#username`.value, $_`#password`.value).then(e => {
            window.location.reload();
        });
    });
    
})();
