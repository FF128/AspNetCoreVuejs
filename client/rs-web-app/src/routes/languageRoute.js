let Layout = () => import("../layouts/VuetifyLayout.vue");

let Language = () =>
  import("../views/setup/standard/employment-other-info/others/language-dialect/Language.vue");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "language",
      component: Language
    }
  ]
};
