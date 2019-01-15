import axios from "axios";

const API_ENDPOINT = "api/gov-exams"

const state = {
    govExams: [],
    loading: false
}

const getters = {

}

const mutations = {
    getAllGovExams(state) {
        state.loading = true;
        axios.get(API_ENDPOINT)
            .then(response => {
                state.govExams = response.data
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
    getAllGovExams({ commit }) {
        commit("getAllGovExams");
    }
}

export default {
    state,
    getters,
    mutations,
    actions,
    namespaced: true
}