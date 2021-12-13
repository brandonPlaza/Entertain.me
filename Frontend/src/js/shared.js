const API_URL = "http://127.0.0.1:5000/api";

const EntertainMe = (() => {

    const AuthToken = localStorage.getItem("auth_token");
    const LoggedIn = !!AuthToken;
    
    const AddAuthToHeaders = (headers) =>
        LoggedIn ? { Authorization: `Bearer ${AuthToken}`, ...headers } : headers;
    
    const Get = (path, body) => {
        return fetch(`${API_URL}/${path}`, {
            headers: AddAuthToHeaders({})
        })
            .then(response => response.json())
            .catch(err => console.error(err));
    };
    
    const Post = (path, body) => {
        return fetch(`${API_URL}/${path}`, {
            method: 'POST',
            headers: AddAuthToHeaders({
                'Content-type': 'application/json',
            }),
            body: JSON.stringify(body)
        })
            .then(response => response.json())
            .catch(err => console.error(err));
    };
    
    
    Post('Authentication/register', {
        userName: 'ethan2',
        email: 'ethan+foo@tague.me',
        password: 'TestPa12@ssword'
    }).then(res => console.log(res));
    
    return { Get, Post, LoggedIn };
    
})();