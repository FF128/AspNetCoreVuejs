//let Layout = () => import("../layouts/VuetifyLayout.vue");

let PageNotFound = () => import("../views/redirect/PageNotFound.vue");
export default {
  path: "*",
  component: PageNotFound
};
