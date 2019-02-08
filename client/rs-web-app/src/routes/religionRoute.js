let Religion = resolve =>
  require([
    "../views/setup/standard/personal-information/religion/Religion.vue"
  ], resolve);
export default {
  path: "/religion",
  component: Religion
};