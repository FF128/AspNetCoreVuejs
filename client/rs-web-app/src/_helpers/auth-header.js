export function authHeader() {
    // return authorization header with jwt token
    let token = JSON.parse(localStorage.getItem('_t'));

    if (token) {
        return { 'Authorization': 'Bearer ' + token };
    } else {
        return {};
    }
}