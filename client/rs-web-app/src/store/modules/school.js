import axios from "axios";

const API_ENDPOINT = "api/school";

const state = {
  schools: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllSchools(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.schools = response.data;
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
};

const actions = {
  getAllSchools({ commit }) {
    commit("getAllSchools");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
