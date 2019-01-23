let Layout = () => import("../layouts/VuetifyLayout.vue");

let EmailFormat = () => 
    import("../views/setup/recruitment/email-format/EmailFormat.vue");
let Hired = () => 
    import("../views/setup/recruitment/email-format/Hired.vue");
let Screening = () =>
    import("../views/setup/recruitment/email-format/ScreeningSchedule.vue")
let ScreeningEntry = () =>
    import("../views/setup/recruitment/email-format/ScreeningEntry.vue") 
export default {
  path: "/email-format",
  component: EmailFormat,
  children: [
    {
        path: "",
        component: Hired
    },
    {   
        path: "screening",
        component: Screening
    },
    {
        path: "results",
        component: ScreeningEntry
    }
  ]
}