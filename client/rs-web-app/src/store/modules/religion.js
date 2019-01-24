import axios from "axios";
import { Toast } from "buefy/dist/components/toast";

const state = {
  religions: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllReligions(state) {
    state.loading = true;
    axios
      .get("api/religion")
      .then(response => {
        state.religions = response.data;
        state.loading = false;
      })
      .catch(err => {
        Toast.open("Error!");
        state.loading = false;
      });
  }
};

const actions = {
  getAllReligions({ commit }) {
    commit("getAllReligions");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
