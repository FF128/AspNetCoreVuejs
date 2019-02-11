<template>
  <div>
    <v-app id="inspire">
      <v-navigation-drawer
        class="white"
        v-model="drawer"
        fixed
        app>
        <!-- <img src="@/assets/banner.png" class="img-responsive"/> -->
        <v-card color="white" height="65">
          <v-card-title primary-title>
            <v-badge right>
              <h2>Recruitment System</h2>
            </v-badge>
            
            
          </v-card-title>
        </v-card>
        <standard-side-menu v-if="activeRoute == 'STD'"></standard-side-menu>
        <recruitment-side-menu v-if="activeRoute == 'REC'"></recruitment-side-menu>
        <transaction-side-menu v-if="activeRoute == 'TRANS'"></transaction-side-menu>
        
      </v-navigation-drawer>
      <!-- Toolbar -->
      <v-toolbar 
        dark 
        color="primary"
        fixed
        app
        >
        <v-toolbar-side-icon @click.stop="drawer = !drawer">
          
        </v-toolbar-side-icon>

        <v-toolbar-title class="white--text"></v-toolbar-title>

        <v-spacer></v-spacer>
    
        <v-toolbar-items class="hidden-sm-and-down">
            <v-menu bottom offset-y
                v-for="(item,key) in menu"
                v-bind:key="key">
              <v-btn
                flat
                slot="activator"
                @click="changeRoute(item.routeName)"
              >
                {{ item.title }}
                <v-icon>{{item.icon}}</v-icon>
              </v-btn>
              <v-list v-if="item.submenu">
                <v-list-tile
                  @click="changeRoute(sub.routeName)"
                  v-for="(sub,key) in item.submenu"
                  :key="key"
                >
                  <v-list-tile-title>{{sub.title}}</v-list-tile-title>
                </v-list-tile>
              </v-list>
            </v-menu>
          </v-toolbar-items>
          
          <v-menu class="hidden-md-and-up" bottom offset-y>
            <v-toolbar-side-icon slot="activator">
              <v-icon>settings</v-icon>
            </v-toolbar-side-icon>
            <v-list>
              <v-list-tile @click="changeRoute('STD')">
                <v-list-tile-content>
                  <v-list-tile-title>Standard</v-list-tile-title>
                </v-list-tile-content>
              </v-list-tile>
              <v-list-tile @click="changeRoute('REC')">
                <v-list-tile-content>
                  <v-list-tile-title>Recruitment</v-list-tile-title>
                </v-list-tile-content>
              </v-list-tile>   
            </v-list>
          </v-menu>

          <v-toolbar-items class="hidden-md-and-up" >
            <v-toolbar-side-icon>
              <v-tooltip bottom>
                <v-icon slot="activator" @click="changeRoute('TRANS')">description</v-icon>
                <span>Transactions</span>
              </v-tooltip>
            </v-toolbar-side-icon>
          </v-toolbar-items>
            
          <v-toolbar-items class="hidden-md-and-up">
            <v-toolbar-side-icon>
              <v-tooltip bottom>
                <v-icon slot="activator">dvr</v-icon>
                <span>Reports</span>
              </v-tooltip>
            </v-toolbar-side-icon>
          </v-toolbar-items>

          <v-toolbar-items class="hidden-md-and-up">
            <v-toolbar-side-icon>
              <v-tooltip bottom>
                <v-icon slot="activator">people</v-icon>
                <span>Admin</span>
              </v-tooltip>
            </v-toolbar-side-icon>
          </v-toolbar-items>

          <!-- Logout Settings -->
          <v-menu bottom offset-y>
            <v-btn
              slot="activator"
              dark
              icon
            >
              <v-icon>more_vert</v-icon>
            </v-btn>

            <v-list>
              <v-list-tile
                @click="logout"
              >
                <v-list-tile-title>Settings</v-list-tile-title>
              </v-list-tile>
            </v-list>
             <v-list>
              <v-list-tile
                @click="logout"
              >
                <v-list-tile-title>Logout</v-list-tile-title>
              </v-list-tile>
            </v-list>
          </v-menu>
      </v-toolbar>
      <!-- Content -->
      <v-content>
        <img src="@/assets/banner.png" class="img-responsive"/>
        <slot/>
      </v-content>
      <v-footer app inset>© 2019 - 128 Tech Consulting Inc.</v-footer>
    </v-app>
  </div>
  
</template>
<script>
import { mapState } from 'vuex';
import { mapActions } from 'vuex';
let StandardSideMenu = resolve =>
  require(["@/layouts/StandardSideMenu"], resolve);

let RecruitmentSideMenu = resolve =>
  require(["@/layouts/RecruitmentSideMenu"], resolve);

let TransactionSideMenu = resolve =>
  require(["@/layouts/TransactionSideMenu"], resolve);

  
export default {
  components: {
    StandardSideMenu,
    RecruitmentSideMenu,
    TransactionSideMenu
  },
  data() {
    return {
      title: "Recruitment System",
      aside: false,
      clipped: true,
      absolute: true,
      admins: [["Management", "people_outline"], ["Settings", "settings"]],
      cruds: [
        ["Create", "add"],
        ["Read", "insert_drive_file"],
        ["Update", "update"],
        ["Delete", "delete"]
      ],
      drawer: true,
      generalSetupItems: [{ title: "Company Information" }],
      personalInformationItems: [
        { title: "Citizenship" },
        { title: "Religion" }
      ],
      employmentInformationItems: [
        { title: "Employee Level" },
        { title: "Organizational Level" }
      ],
       menu: [
        { 
          icon: 'arrow_drop_down',
          title: 'Setup', 
          submenu: [
            {
              title: 'Standard',
              routeName: 'STD'
            },
            {
              title: 'Recruitment',
              routeName: 'REC'
            }
          ] 
        },
        { icon: '', title: 'Transactions', routeName: 'TRANS' },
        { icon: '', title: 'Reports', routeName: 'REP' },
        { icon: '', title: 'Admin', routeName: 'ADMIn'  }
      ]
    };
  },
  methods: {
    ...mapActions('routing', [
      'changeRoute'
    ]),
    logout() {
      localStorage.removeItem("_u");
      // this.$router.push("/login");
      this.$router.go("/login");
    }
  },
  computed: {
    ...mapState('routing', {
      activeRoute: state => state.activeRoute
    })
  }
};
</script>
<style>
.img-responsive {
  max-width: 100%;
  height: auto;
  display: block;
}
</style>
