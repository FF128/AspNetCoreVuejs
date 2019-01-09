<template>
  <nav
    class="navbar is-white is-fixed-top"
    id="navbarStyle"
    role="navigation"
    aria-label="main navigation"
    style="position:fixed; width: 100%"
  >
    <div class="navbar-brand">
      <a
        class="navbar-item"
        href="https://bulma.io"
      >
        <img
          src="https://bulma.io/images/bulma-logo.png"
          width="112"
          height="28"
        >
        <!-- <h1 class="title">Campus Physical Theraphy Inc.</h1> -->
      </a>
      <a
        class="navbar-item"
        @click="triggerCloseOpenSidebar"
        title="Close/Expand Sidebar"
      ><span class="icon">
          <i class="fa fa-th-list"></i>
        </span></a>

      <a
        role="button"
        class="navbar-burger burger"
        aria-label="menu"
        aria-expanded="false"
        onclick="document.querySelector('.navbar-menu').classList.toggle('is-active');"
      >
        <span aria-hidden="true"></span>
        <span aria-hidden="true"></span>
        <span aria-hidden="true"></span>
      </a>
    </div>

    <div class="navbar-menu">
      <div class="navbar-end">
        <div class="navbar-item has-dropdown is-hoverable">
          <a
            class="navbar-link"
            href="#"
          >
            <span class="icon">
              <i class="far fa-file-alt"></i></span>
            &nbsp; Documents
          </a>
          <div class="navbar-dropdown is-boxed">
            <router-link
              class="navbar-item"
              to="/appointment-contract"
            >Appointment Contract</router-link>
            <router-link
              class="navbar-item"
              to="/sofresponsibility"
            >Statement of Financial Responsibility</router-link>
          </div>
        </div>
        <router-link
          class="navbar-item"
          to="/users"
        >
          <span class="icon">
            <i class="fas fa-shield-alt"></i>
          </span>
          &nbsp;
          Account
        </router-link>
        <a
          class="navbar-item"
          href="#"
          @click="logout"
        >
          Logout
        </a>
      </div>
    </div>
  </nav>
</template>

<script>
import { mapActions } from "vuex";

export default {
  methods: {
    ...mapActions("topandsidebar", ["openAndCloseSideBar"]),
    triggerCloseOpenSidebar() {
      this.openAndCloseSideBar({
        isOpenAndCloseSidebar: true
      });
    },
    logout() {
      localStorage.removeItem("token");
      //window.location.href = "/login";
      //this.$router.push("/login")
      this.$router.go("/login")
    }
  }
};
</script>

<style>
#navbarStyle {
  -webkit-box-shadow: 0px 1px 5px #ccc;
  -moz-box-shadow: 0px 1px 5px #ccc;
  box-shadow: 0px 1px 5px #ccc;
  position: -webkit-sticky;
  position: sticky;
  top: 0;
}
</style>
