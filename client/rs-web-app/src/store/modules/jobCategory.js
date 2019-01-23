import axios from "axios";

const API_ENDPOINT = "api/job-cat"

const state = {
    jobCategories: [],
    loading: false
}

const getters = {

}

const mutations = {
    getAllJobCategories(state) {
        state.loading = true;
        axios.get(API_ENDPOINT)
            .then(response => {
                state.jobCategories = response.data
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
    getAllJobCategories({ commit }) {
        commit("getAllJobCategories");
    }
}

export default {
    state,
    getters,
    mutations,
    actions,
    namespaced: true
}