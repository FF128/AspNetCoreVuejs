let Home = () => import("../views/Home.vue");
export default {
    meta: {
        mod: 'admin',
        activeRoute: 'STD',
        activeRouteName: 'Standard Setup'
    },
    path: "/",
    component: Home
};