import axios from "axios";

const API_ENDPOINT = "api/residence-type"

const state = {
    residenceTypeData: [],
    loading: false
}

const getters = {

}

const mutations = {
    getAllResidenceTypeData(state) {
        state.loading = true;
        axios.get(API_ENDPOINT)
            .then(response => {
                state.residenceTypeData = response.data
                state.loading = false;
            })
            .catch(err => {
                // console.log(err.response.status)
                // if(err.response.status === 401){
                //     alert("Unauthorized")
                // }
                state.loading = false;
            });
    }
}

const actions = {
    getAllResidenceTypeData({ commit }) {
        commit("getAllResidenceTypeData");
    }
}

export default {
    state,
    getters,
    mutations,
    actions,
    namespaced: true
}