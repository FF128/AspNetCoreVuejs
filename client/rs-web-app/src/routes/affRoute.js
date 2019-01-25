//let Layout = () => import("../layouts/VuetifyLayout.vue");

let Affiliations = () =>
  import("../views/setup/recruitment/affiliations/Affiliations.vue");
export default {
  meta: {
    module: 'REC'
  },
  path: "/affiliations",
  component: Affiliations
};
