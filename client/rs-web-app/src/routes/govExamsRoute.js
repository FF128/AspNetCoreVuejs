let Layout = () => import("../layouts/VuetifyLayout.vue");

let GovExams = () =>
  import("../views/setup/standard/employment-other-info/others/government-exams/GovExams.vue");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "gov-exams",
      component: GovExams
    }
  ]
};
