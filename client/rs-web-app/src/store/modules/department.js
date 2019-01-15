import axios from "axios";

const API_ENDPOINT = "api/dep"

const state = {
    departments: [],
    loading: false
}

const getters = {

}

const mutations = {
    getAllDepartments(state) {
        state.loading = true;
        axios.get(API_ENDPOINT)
            .then(response => {
                state.departments = response.data
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
    getAllDepartments({ commit }) {
        commit("getAllDepartments");
    }
}

export default {
    state,
    getters,
    mutations,
    actions,
    namespaced: true
}