// Entertain.me Common JS Utilities


// Simple shorthand for document.querySelector(...): $_`#query`
const $_ = (a) => document.querySelector(a);


// API Helper: Checks login status, wraps requests, etc.
// Exports:
//      LoggedIn            bool                        Indicates log-in status
//      ExpectLoggedIn      void(url)                   Check if the user is logged in, else redirect to URL.
//      ExpectLoggedOut     void(url)                   Check if the user is logged out, else redirect to URL.
//      Get                 Promise<object>(url)        Make a GET request to the URL and parse and return JSON data.
//      Post                Promise<object>(url)        Make a POST request to the URL and parse and return JSON data.
const API = (() => {
    const API_URL = "http://127.0.0.1:5000/api";
    
    // Check authorization status
    const AuthToken = localStorage.getItem("auth_token");
    const LoggedIn = !!AuthToken;
    
    // Inject bearer header into a request.
    const AddAuthToHeaders = (headers) =>
        LoggedIn ? { Authorization: `Bearer ${AuthToken}`, ...headers } : headers;
    
    // Generic HTTP GET request, parsing JSON response.
    const Get = (path) => {
        console.log("AATH: " + JSON.stringify(AddAuthToHeaders({})));

        return fetch(`${API_URL}/${path}`, {
            method: 'GET',
            headers: AddAuthToHeaders({}),
            credentials: "include"
        })
            .then(response => response.json())
            .catch(err => console.error(err));
    };
    
    // Generic HTTP POST request, parsing JSON response.
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
    
    // Helper: Invoke from JS body to indicate that this page expects the user to be logged in and should redirect
    // elsewhere otherwise.
    const ExpectLoggedIn = (fallback) => {
        if (!LoggedIn) window.location.href = fallback;
    };
    
    // Helper: Invoke from JS body to indicate that this page expects the user to be logged out and should redirect
    // elsewhere otherwise.
    const ExpectLoggedOut = (fallback) => {
        if (LoggedIn) window.location.href = fallback;
    }

    return { Get, Post, LoggedIn, ExpectLoggedIn, ExpectLoggedOut };
})();