const state = {
    activeRoute: 'STD'
}

const getters = {

}

const mutations = {
    changeRoute(state, payload) {
        state.activeRoute = payload;
    }
}

const actions = {
    changeRoute({ commit }, payload){
        commit("changeRoute", payload)
    }
}

export default {
    state,
    getters,
    mutations,
    actions,
    namespaced: true
}