<template>
  <div>
    <vuetify-layout-default>
      <transition name="fade">
        <router-view></router-view>
      </transition>
    </vuetify-layout-default>
    <!-- <div v-else>
      <transition name="fade">
        <router-view></router-view>
      </transition> 
    </div> -->
  </div>
  

</template>

<script>
// import VuetifyLayoutDefault from "@/layouts/VuetifyLayoutDefault";
import Auth from "@/_services/auth"
// let VuetifyLayoutDefault = () => import("@/layouts/VuetifyLayoutDefault");
let VuetifyLayoutDefault = resolve =>
  require(["@/layouts/VuetifyLayoutDefault"], resolve);
let auth = new Auth();
export default {
  name: "App",
  data() {
    return {
      isLoggedIn: false
    };
  },
  components: {
    VuetifyLayoutDefault
  },
  computed: {
  },
  beforeMount() {
    this.isLoggedIn = auth.isLoggedIn();
  }
};
</script>
