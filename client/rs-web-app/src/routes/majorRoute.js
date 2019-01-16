let Layout = () => import("../layouts/VuetifyLayout.vue");

let Major = () => 
    import("../views/setup/standard/employment-other-info/education/major/Major.vue")
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "major",
      component: Major
    }
  ]
}