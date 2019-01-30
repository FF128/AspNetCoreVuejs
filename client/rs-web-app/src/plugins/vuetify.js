import Vue from "vue";
import Vuetify, {
  VApp,
  VNavigationDrawer,
  VForm,
  VContainer,
  VLayout,
  VFlex,
  VBtn,
  VTextField,
  VSnackbar,
  VDataTable,
  VToolbar
} from "vuetify/lib";
import "vuetify/src/stylus/app.styl";
import colors from "vuetify/lib/util/colors";

Vue.use(Vuetify, {
  components: {
    VApp,
    VNavigationDrawer,
    VForm,
    VContainer,
    VLayout,
    VFlex,
    VBtn,
    VTextField,
    VSnackbar,
    VDataTable,
    VToolbar
  },
  iconfont: "md",
  theme: {
    primary: colors.blue.darken2
  }
});
