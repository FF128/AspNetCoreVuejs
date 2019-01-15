let Layout = () => import("../layouts/VuetifyLayout.vue");

let CourseDegree = () => 
    import("../views/setup/standard/employment-other-info/education/course/CourseDegree.vue")
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "course",
      component: CourseDegree
    }
  ]
}