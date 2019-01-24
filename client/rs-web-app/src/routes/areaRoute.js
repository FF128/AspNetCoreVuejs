let Layout = () => import("../layouts/VuetifyLayout.vue");

let Area = () =>
  import("../views/setup/standard/employment-information/organizational-level/area/Area.vue");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "area",
      component: Area
    }
  ]
};
