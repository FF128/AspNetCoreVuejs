export function getUserDetails() {
    if(localStorage.getItem('_u')){
        return JSON.parse(localStorage.getItem('_u'));
    }
        
    return "";

}

export function removeUser() {
    localStorage.removeItem("_u");
}