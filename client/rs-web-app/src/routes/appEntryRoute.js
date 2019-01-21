let Layout = () => import("../layouts/VuetifyLayout.vue");

let AppEntry = () => 
    import("../views/setup/recruitment/applicants-entry/ApplicantsEntry");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "applicants-entry",
      component: AppEntry
    }
  ]
}