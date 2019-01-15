import axios from "axios";

const API_ENDPOINT = "api/course"

const state = {
    courses: [],
    loading: false
}

const getters = {

}

const mutations = {
    getAllCourses(state) {
        state.loading = true;
        axios.get(API_ENDPOINT)
            .then(response => {
                state.courses = response.data
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
    getAllCourses({ commit }) {
        commit("getAllCourses");
    }
}

export default {
    state,
    getters,
    mutations,
    actions,
    namespaced: true
}