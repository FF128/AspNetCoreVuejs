let Region = () =>
  import("../views/setup/standard/employment-information/organizational-level/region/Region.vue");
export default {
  path: "/region",
  component: Region,
  children: [
    {
      path: "region",
      component: Region
    }
  ]
};
