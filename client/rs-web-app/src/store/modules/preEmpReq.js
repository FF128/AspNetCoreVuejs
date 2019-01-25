import axios from "axios";
import Toast from "@/project-modules/toast"
let toast = new Toast();

const API_ENDPOINT = "api/pre-emp-req";

const state = {
  preEmpReqData: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllPreEmpReqData(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.preEmpReqData = response.data;
        state.loading = false;
      })
      .catch(({response}) => {
        toast.show(response.data);
        state.loading = false;
      });
  }
};

const actions = {
  getAllPreEmpReqData({ commit }) {
    commit("getAllPreEmpReqData");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
