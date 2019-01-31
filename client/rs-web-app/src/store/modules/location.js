import axios from "axios";

const API_ENDPOINT = "api/loc";

const state = {
  locations: [],
  headers: [
    {
      text: "Code",
      align: "left",
      sortable: false,
      value: "code"
    },
    { text: "Description", value: "description", align: "left" },
    { text: "", value: "actions" }
  ],
  loading: false
};

const getters = {};

const mutations = {
  getAllLocations(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.locations = response.data;
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
  getAllLocations({ commit }) {
    commit("getAllLocations");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
