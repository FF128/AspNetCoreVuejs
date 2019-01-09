import axios from "axios";
import { Toast } from 'buefy/dist/components/toast'

const state = {
    cit: [],
    loading: false
}

const getters = {

}

const mutations = {
    getAllCitizenship(state) {
        state.loading = true;
        axios.get("api/citizenship")
            .then(response => {
                state.cit = response.data
                state.loading = false;
            })
            .catch(err => {
                Toast.open("Error!");
                state.loading = false;
            });
    }
}

const actions = {
    getAllCitizenship({ commit }) {
        commit("getAllCitizenship");
    }
}

export default {
    state,
    getters,
    mutations,
    actions,
    namespaced: true
}