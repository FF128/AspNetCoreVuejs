import axios from "axios";

const API_ENDPOINT = "api/applicants-entry"

const state = {
    genInfoData: [],
    essayQuestions: [],
    attachments: [],
    sources: [],
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
                state.loading = false;
            });
    },
    getAllQuestions(state) {
        axios.get(`${API_ENDPOINT}/essay`)
            .then(response => {
                var { data } = response;
                state.essayQuestions = data;
            })
            .catch(err => {

            })
    },
    getAllAttachments(state) {
        axios.get(`${API_ENDPOINT}/attachment`)
            .then(response => {
                var { data } = response;
                state.attachments = data;
            })
            .catch(err => {

            })
    },
    getAllSourceOfInfo() {
        axios.get(`${API_ENDPOINT}/source`)
            .then(response => {
                var { data } = response;
                state.sources = data;
            })
            .catch(err => {

            })
    }
}

const actions = {
    getAllGenInfoData({ commit }) {
        commit("getAllGenInfoData");
    },
    getAllQuestions({ commit }) {
        commit("getAllQuestions")
    },
    getAllAttachments({ commit }) {
        commit("getAllAttachments");
    },
    getAllSourceOfInfo({ commit }) {
        commit("getAllSourceOfInfo");
    }
}

export default {
    state,
    getters,
    mutations,
    actions,
    namespaced: true
}