import Vue from "vue";
import VueJWT from "vuejs-jwt";
import { getUserDetails, removeUser } from "../_helpers/user";

Vue.use(VueJWT);

export default class AuthService {
  isLoggedIn() {
    let user = getUserDetails();

    if (user.token) {
      var decodedToken = Vue.$jwt.decode(user.token);
      if (decodedToken) {
        if (decodedToken.exp < new Date().getTime() / 1000) {
          alert("Session expired. Please re-login your account");

          // Remove user from Local Storage
          removeUser();
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
