import axios from "axios";

const API_ENDPOINT = "api/skills";

const state = {
  skills: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllSkills(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.skills = response.data;
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
  getAllSkills({ commit }) {
    commit("getAllSkills");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
