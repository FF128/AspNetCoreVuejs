import axios from "axios";
const state = {
  designationFiles: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllDesignationFiles(state) {
    state.loading = true;
    axios
      .get("api/designation-file")
      .then(response => {
        state.designationFiles = response.data;
        state.loading = false;
      })
      .catch(err => {
        state.loading = false;
      });
  }
};

const actions = {
  getAllDesignationFiles({ commit }) {
    commit("getAllDesignationFiles");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
