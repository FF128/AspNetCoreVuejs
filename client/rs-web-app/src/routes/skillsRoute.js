let Layout = () => import("../layouts/VuetifyLayout.vue");

let Skills = () => 
    import("../views/setup/standard/employment-other-info/others/skills/Skills.vue")
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "skills",
      component: Skills
    }
  ]
}