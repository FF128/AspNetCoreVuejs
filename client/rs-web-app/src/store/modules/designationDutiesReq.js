import axios from "axios";
import Toast from "@/project-modules/toast"

let toast = new Toast();
const API_ENDPOINT = "api/desig-duties-req";

const state = {
  dutiesReqData: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllDutiesReqData(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.dutiesReqData = response.data;
        state.loading = false;
      })
      .catch(({response}) => {
        // console.log(err.response.status)
        // if(err.response.status === 401){
        //     alert("Unauthorized")
        toast.show(response.data);
        // }
        state.loading = false;
      });
  }
};

const actions = {
  getAllDutiesReqData({ commit }) {
    commit("getAllDutiesReqData");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
