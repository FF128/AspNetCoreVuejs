//let Layout = () => import("../layouts/VuetifyLayout.vue");

let EvalEmailFormat = () =>
  import("../views/setup/recruitment/eval-email-format/EvalEmailFormat.vue");
let ScreeningResult = () => import("../views/setup/recruitment/eval-email-format/ScreeningResult.vue");
let RecruiterPending = () =>
  import("../views/setup/recruitment/eval-email-format/RecruiterPending.vue");
let EvaluatorPending = () =>
  import("../views/setup/recruitment/eval-email-format/EvaluatorPending.vue");
export default {
  path: "/eval-email-format",
  component: EvalEmailFormat,
  children: [
    {
      path: "",
      component: ScreeningResult
    },
    {
      path: "recruiter-pending",
      component: RecruiterPending
    },
    {
      path: "evaluator-pending",
      component: EvaluatorPending
    }
  ]
};
