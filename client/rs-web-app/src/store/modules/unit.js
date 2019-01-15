import axios from "axios";

const API_ENDPOINT = "api/unit"

const state = {
    units: [],
    loading: false
}

const getters = {

}

const mutations = {
    getAllUnits(state) {
        state.loading = true;
        axios.get(API_ENDPOINT)
            .then(response => {
                state.units = response.data
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
    getAllUnits({ commit }) {
        commit("getAllUnits");
    }
}

export default {
    state,
    getters,
    mutations,
    actions,
    namespaced: true
}