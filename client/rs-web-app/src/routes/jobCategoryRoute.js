let Layout = () => import("../layouts/VuetifyLayout.vue");

let JobCategory = () =>
  import("../views/setup/recruitment/job-category/JobCategory.vue");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "job-category",
      component: JobCategory
    }
  ]
};
