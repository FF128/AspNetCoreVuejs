import Vue from 'vue'
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
} from 'vuetify/lib'
import 'vuetify/src/stylus/app.styl'

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
  iconfont: 'md',
  
})
