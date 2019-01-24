let Layout = () => import("../layouts/VuetifyLayout.vue");

let License = () =>
  import("../views/setup/standard/employment-other-info/others/license/License.vue");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "license",
      component: License
    }
  ]
};
