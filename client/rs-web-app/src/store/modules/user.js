import Vue from "vue"
import VueJWT from "vuejs-jwt"
import { getUserDetails } from "@/_helpers/user"

Vue.use(VueJWT);

const state = {
    companyCode: ''
}

const getters = {

}

const mutations = {
    decodeToken(state) {
        var user = getUserDetails();
        console.log(user);
        var decodedToken = Vue.$jwt.decode(user.token);

        state.companyCode = decodedToken.CompanyCode;
    }
}

const actions = {
    decodeToken({ commit }) {
        commit('decodeToken');
    }
}

export default {
    state,
    getters,
    mutations,
    actions,
    namespaced: true
}