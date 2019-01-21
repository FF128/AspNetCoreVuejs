import axios from "axios";

const API_ENDPOINT = "api/applicants-entry"

const state = {
    genInfoData: [],
    loading: false
}

const getters = {

}

const mutations = {
    getAllGenInfoData(state) {
        state.loading = true;
        axios.get(`${API_ENDPOINT}/gen`)
            .then(response => {
                state.genInfoData = response.data
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
    getAllGenInfoData({ commit }) {
        commit("getAllGenInfoData");
    }
}

export default {
    state,
    getters,
    mutations,
    actions,
    namespaced: true
}