let Layout = () => import("../layouts/VuetifyLayout.vue");

let Region = () => 
    import("../views/setup/standard/employment-information/organizational-level/region/Region.vue");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "region",
      component: Region
    }
  ]
}