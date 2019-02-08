const Citizenship = resolve =>
  require([
    "../views/setup/standard/personal-information/citizenship/Citizenship.vue"
  ], resolve);
export default {
  path: "/citizenship",
  component: Citizenship
};