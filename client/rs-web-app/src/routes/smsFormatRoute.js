//let Layout = () => import("../layouts/VuetifyLayout.vue");

let SMSFormat = () =>
  import("../views/setup/recruitment/sms-format/SMSFormat.vue");
let Hired = () => import("../views/setup/recruitment/sms-format/Hired.vue");
let Screening = () =>
  import("../views/setup/recruitment/sms-format/ScreeningSchedule.vue");
let ScreeningEntry = () =>
  import("../views/setup/recruitment/sms-format/ScreeningEntry.vue");
export default {
  path: "/sms-format",
  component: SMSFormat,
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
};
