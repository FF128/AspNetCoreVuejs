<template>
  <div>
    <div
      :class="sideBarClass"
      v-if="isLoggedIn"
    >
      <div class="ulDiv">
        <div class="liDiv">
          <router-link
            to="/"
            :title="isSidebarActive ? null : 'Dashboard'"
          >
            <i class="fas fa-tachometer-alt"></i><span>Dashboard</span>
          </router-link>
        </div>
        <div class="liDiv">
          <router-link
            to="/clinics"
            :title="isSidebarActive ? null : 'Clinic'"
          ><i class="fa fa-hospital"></i><span>Clinic</span></router-link>
        </div>
        <div class="liDiv">
          <router-link
            to="/physicians"
            :title="isSidebarActive ? null : 'Physician'"
          ><i class="fa fa-user-md"></i><span>Physician</span></router-link>
        </div>
        <div class="liDiv">
          <a
            href="#"
            class="dropdown-btn"
            :title="isSidebarActive ? null : 'Patient'"
          >
            <i class="fa fa-users"></i>
            <span>Patient
            </span>
            <i
              id="caretId"
              class="fa fa-caret-down"
            ></i>
          </a>
          <div
            :style="dropdownContainerStyle"
            :class="dropdownContainerClass"
          >
            <router-link to="/patients">Patient Record</router-link>
            <router-link to="/insurances">Insurance Record</router-link>
            <router-link to="/placeOfService">Place Of Service</router-link>
          </div>
        </div>
        <div class="liDiv">
          <a
            href="#"
            class="dropdown-btn"
            :title="isSidebarActive ? null : 'Appointment'"
          >
            <i class="fa fa-calendar"></i>
            <span>Appointment
            </span>
            <i
              id="caretId"
              class="fa fa-caret-down"
            ></i>
          </a>
          <div
            :style="dropdownContainerStyle"
            :class="dropdownContainerClass"
          >
            <router-link to="/manage-physician-sched">Manage Physician Schedule</router-link>
            <router-link to="/patient-calendar">Create Appointment</router-link>
            <router-link to="/piv">Patient Initial Visit</router-link>
          </div>
        </div>
        <!-- <div class="liDiv">
          <a
            href="#"
            class="dropdown-btn"
            :title="isSidebarActive ? null : 'Patient Notes'"
          >
            <i class="fas fa-notes-medical"></i>
            <span>Patient Notes
            </span>
            <i
              id="caretId"
              class="fa fa-caret-down"
            ></i>
          </a>
          <div
            :style="dropdownContainerStyle"
            :class="dropdownContainerClass"
          >
            <router-link to="/notes/daily-notes/">Patient Note</router-link>
            <router-link to="/notes/daily-notes/1/print">Daily Note Print Template</router-link>
          </div>
        </div> -->
      </div>
    </div>
    <!-- Content -->
    <div
      :class="mainClass"
      v-if="isLoggedIn"
    >
      <router-view></router-view>
    </div>
    <div v-else>
      <router-view></router-view>
    </div>
  </div>
</template>
<script>
import AuthService from "../../_services/auth";

import { mapState, mapActions } from "vuex";

const authService = new AuthService();

export default {
  mounted() {
    var dropdown = document.getElementsByClassName("dropdown-btn");
    var i;

    var sideBar = document.getElementsByClassName("sidebar");
    var main = document.getElementsByClassName("main");

    for (i = 0; i < dropdown.length; i++) {
      dropdown[i].addEventListener("click", function() {
        this.classList.toggle("active");
        var dropdownContent = this.nextElementSibling;
        if (dropdownContent.style.display === "block") {
          dropdownContent.style.display = "none";
          if (window.innerWidth <= 1087) {
            if (sideBar[0].className === "sidebar active") {
              sideBar[0].className = "sidebar";
            }
          }
        } else {
          dropdownContent.style.display = "block";
          if (window.innerWidth <= 1087) {
            if (sideBar[0].className === "sidebar") {
              sideBar[0].className = "sidebar active";
            }
          } else {
            if (sideBar[0].className === "sidebar") {
              sideBar[0].className = "sidebar active";
              main[0].className = "main active";
            }
          }
        }
      });
    }
  },
  computed: {
    ...mapState("topandsidebar", {
      isOpenAndCloseSidebar: state => state.isOpenAndCloseSidebar
    })
  },
  data() {
    return {
      sideBarClass: "",
      mainClass: "",
      dropdownContainerClass: "dropdown-container",
      dropdownContainerStyle: "",
      isSidebarActive: false
    };
  },
  methods: {
    ...mapActions("topandsidebar", ["openAndCloseSideBar"]),
    sidebarEvent() {
      if (
        this.sideBarClass === "sidebar active" &&
        this.mainClass === "main active"
      ) {
        this.sideBarClass = "sidebar";
        this.mainClass = "main";
        this.openAndCloseSideBar({
          sideBarClass: "sidebar",
          mainClass: "main"
        });
      } else {
        this.sideBarClass = "sidebar active";
        this.mainClass = "main active";
        this.openAndCloseSideBar({
          sideBarClass: "sidebar active",
          mainClass: "main active"
        });
      }
      if (this.sideBarClass === "sidebar") {
        this.dropdownContainerStyle = "display: none";
      } else {
        this.dropdownContainerStyle = "";
      }
    },
    windowSizeTrigger() {
      if (window.innerWidth <= 1087) {
        this.sideBarClass = "sidebar";
        this.mainClass = "main";
      } else {
        this.sideBarClass = "sidebar active";
        this.mainClass = "main active";
      }
    }
  },
  created() {
    this.isLoggedIn = authService.isLoggedIn();
    if (
      /Android|webOS|iPhone|iPad|iPod|pocket|psp|kindle|avantgo|blazer|midori|Tablet|Palm|maemo|plucker|phone|BlackBerry|symbian|IEMobile|mobile|ZuneWP7|Windows Phone|Opera Mini/i.test(
        navigator.userAgent
      )
    ) {
      this.sideBarClass = "sidebar";
      this.mainClass = "main";
    }
    window.addEventListener("resize", this.windowSizeTrigger);
    this.windowSizeTrigger();
  },
  watch: {
    isOpenAndCloseSidebar() {
      if (this.isOpenAndCloseSidebar) {
        this.sidebarEvent();
      }
    },
    sideBarClass() {
      this.sideBarClass === "sidebar active"
        ? (this.isSidebarActive = true)
        : (this.isSidebarActive = false);
    }
  }
};
</script>

<style lang="less">
@import "../../custom-css/less/admin.less";
</style>

<style>
@import url(https://use.fontawesome.com/releases/v5.5.0/css/all.css);
</style>

