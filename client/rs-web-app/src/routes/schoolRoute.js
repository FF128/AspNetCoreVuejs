let Layout = () => import("../layouts/VuetifyLayout.vue");

let School = () =>
  import("../views/setup/standard/employment-other-info/education/school/School.vue");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "school",
      component: School
    }
  ]
};
