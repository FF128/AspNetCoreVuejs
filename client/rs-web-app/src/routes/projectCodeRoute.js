let Layout = () => import("../layouts/VuetifyLayout.vue");

let ProjectCode = () =>
  import("../views/setup/standard/employment-information/organizational-level/project-code/ProjectCode.vue");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "project-code",
      component: ProjectCode
    }
  ]
};
