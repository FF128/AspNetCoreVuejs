let Layout = () => import("../layouts/VuetifyLayout.vue");

let JobGroup = () =>
  import("../views/setup/recruitment/job-group/JobGroup.vue");
export default {
  path: "/job-group",
  component: JobGroup
};
