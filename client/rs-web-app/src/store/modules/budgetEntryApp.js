import axios from "axios";

const API_ENDPOINT = "api/budget-entry";

const state = {
  budgetEntryData: [],
  returnedEntryData: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllBudgetEntryData(state) {
    state.loading = true;
    axios
      .get(`${API_ENDPOINT}/waiting`)
      .then(response => {
        state.budgetEntryData = response.data;
        state.loading = false;
      })
      .catch(err => {
        state.loading = false;
      });
  },
  getAllReturnedBudgetEntryData(state) {
    state.loading = true;
    axios
      .get(`${API_ENDPOINT}/returned`)
      .then(response => {
        state.returnedEntryData = response.data;
        state.loading = false;
      })
      .catch(err => {
        state.loading = false;
      });
  }
};

const actions = {
  getAllBudgetEntryData({ commit }) {
    commit("getAllBudgetEntryData");
  },
  getAllReturnedBudgetEntryData({ commit }) {
    commit("getAllReturnedBudgetEntryData");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
