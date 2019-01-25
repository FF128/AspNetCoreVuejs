//let Layout = () => import("../layouts/VuetifyLayout.vue");

let AppEntry = () =>
  import("../views/setup/recruitment/applicants-entry/ApplicantsEntry");
export default {
  meta: {
    module: 'REC'
  },
  path: "/applicants-entry",
  component: AppEntry
};
