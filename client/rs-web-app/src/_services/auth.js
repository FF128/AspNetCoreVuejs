import Vue from 'vue'
import VueJWT from 'vuejs-jwt'

Vue.use(VueJWT)

export default class AuthService {
    isLoggedIn() {
        if (localStorage.getItem('token')) {
            var decodedToken = Vue.$jwt.decode(localStorage.getItem('token'));
            if (decodedToken) {
                if (decodedToken.exp < new Date().getTime() / 1000) {
                    alert("Session expired. Please re-login your account");
                    return false;
                } else {
                    return true; // allow access
                }
            }
        } else {
            return false;
        }
    }
}