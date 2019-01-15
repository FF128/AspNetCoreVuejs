let Layout = () => import("../layouts/VuetifyLayout.vue");

let Rank = () => 
    import("../views/setup/standard/employment-information/organizational-level/rank/Rank.vue");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "rank",
      component: Rank
    }
  ]
}