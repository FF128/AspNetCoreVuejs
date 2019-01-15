let Layout = () => import("../layouts/VuetifyLayout.vue");

let Section = () => 
    import("../views/setup/standard/employment-information/organizational-level/section/Section.vue");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "section",
      component: Section
    }
  ]
}