let Layout = () => import("../layouts/VuetifyLayout.vue");

let Location = () =>
  import("../views/setup/standard/employment-information/organizational-level/location/Location.vue");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "location",
      component: Location
    }
  ]
};
