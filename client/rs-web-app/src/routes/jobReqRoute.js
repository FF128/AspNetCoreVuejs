let Layout = () => import("../layouts/VuetifyLayout.vue");

let JobReq = () =>
  import("../views/setup/standard/employment-other-info/others/job-req/JobReq.vue");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "job-requirements",
      component: JobReq
    }
  ]
};
