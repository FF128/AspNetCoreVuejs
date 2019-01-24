import axios from "axios";

const state = {
  areas: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllAreas(state) {
    state.loading = true;
    axios
      .get("api/area")
      .then(response => {
        state.areas = response.data;
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
  getAllAreas({ commit }) {
    commit("getAllAreas");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
