let Layout = () => import("../layouts/VuetifyLayout.vue");

let Division = () =>
  import("../views/setup/standard/employment-information/organizational-level/division/Division.vue");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "division",
      component: Division
    }
  ]
};
