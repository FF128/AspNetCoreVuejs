import axios from "axios";

const API_ENDPOINT = "api/affiliations";

const state = {
  affData: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllAffiliations(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.affData = response.data;
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
  getAllAffiliations({ commit }) {
    commit("getAllAffiliations");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
