import axios from "axios";

const API_ENDPOINT = "api/dr"

const state = {
    duties: [],
    loading: false
}

const getters = {

}

const mutations = {
    getAllDuties(state) {
        state.loading = true;
        axios.get(API_ENDPOINT)
            .then(response => {
                state.duties = response.data
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
    getAllDuties({ commit }) {
        commit("getAllDuties");
    }
}

export default {
    state,
    getters,
    mutations,
    actions,
    namespaced: true
}