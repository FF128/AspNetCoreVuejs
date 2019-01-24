let Layout = () => import("../layouts/VuetifyLayout.vue");

let Affiliations = () =>
  import("../views/setup/recruitment/affiliations/Affiliations.vue");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "affiliations",
      component: Affiliations
    }
  ]
};
