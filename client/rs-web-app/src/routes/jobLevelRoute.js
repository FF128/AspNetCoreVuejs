let Layout = () => import("../layouts/DefaultLayout.vue");

let JobLevel = () => 
    import("../views/setup/standard/employment-information/employee-level/job-level/JobLevel.vue");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "job-level",
      component: JobLevel
    }
  ]
}