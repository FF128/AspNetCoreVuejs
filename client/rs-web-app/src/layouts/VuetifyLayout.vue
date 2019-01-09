<template>
  <v-app>
    <v-navigation-drawer
      v-model="drawer"
      absolute
      clipped
      app>
      <v-list dense>
        <v-list-tile @click="$router.push('/')">
          <v-list-tile-action>
            <v-icon>home</v-icon>
          </v-list-tile-action>
          <v-list-tile-title>Home</v-list-tile-title>
        </v-list-tile>

        <v-list-group
          prepend-icon="account_circle"
          value="true"
        >
          <v-list-tile slot="activator">
            <v-list-tile-title>Setup</v-list-tile-title>
          </v-list-tile>

          <v-list-group
            no-action
            sub-group
            value="true"
          >
            <v-list-tile slot="activator">
                <v-list-tile-title>Standard</v-list-tile-title>
            </v-list-tile>
            <v-menu open-on-hover offset-x>
                <v-list-tile slot="activator">
                    <v-list-tile-title>General Setup</v-list-tile-title>
                </v-list-tile>
                <v-list>
                    <v-list-tile
                    v-for="(item, index) in generalSetupItems"
                    :key="index"
                    @click=""
                    >
                    <v-list-tile-title>{{ item.title }}</v-list-tile-title>
                    </v-list-tile>
                </v-list>
            </v-menu>
            <v-menu open-on-hover offset-x>
                <v-list-tile slot="activator">
                    <v-list-tile-title>Personal Information</v-list-tile-title>
                </v-list-tile>
                <v-list>
                    <v-list-tile
                    v-for="(item, index) in personalInformationItems"
                    :key="index"
                    @click=""
                    >
                    <v-list-tile-title>{{ item.title }}</v-list-tile-title>
                    </v-list-tile>
                </v-list>
            </v-menu>
            <v-menu open-on-hover offset-x>
                <v-list-tile slot="activator">
                    <v-list-tile-title>Employment Information</v-list-tile-title>
                </v-list-tile>
                <v-list>
                    <!-- <v-list-tile
                    v-for="(item, index) in employmentInformationItems"
                    :key="index"
                    @click=""
                    >
                        <v-list-tile-title>{{ item.title }}</v-list-tile-title>
                    </v-list-tile> -->
                    <v-menu open-on-hover offset-x>
                         <v-list-tile slot="activator">
                            <v-list-tile-title>Employee Level</v-list-tile-title>
                        </v-list-tile>
                        <v-list>
                            <v-list-tile>
                                <v-list-tile-title>Employee Status File</v-list-tile-title>
                            </v-list-tile>
                        </v-list>
                    </v-menu>
                </v-list>
            </v-menu>
          </v-list-group>

          <v-list-group
            sub-group
            no-action
          >
            <v-list-tile slot="activator">
              <v-list-tile-title>Actions</v-list-tile-title>
            </v-list-tile>

            <v-list-tile
              v-for="(crud, i) in cruds"
              :key="i"
              @click=""
            >
              <v-list-tile-title v-text="crud[0]"></v-list-tile-title>
              <v-list-tile-action>
                <v-icon v-text="crud[1]"></v-icon>
              </v-list-tile-action>
            </v-list-tile>
          </v-list-group>
        </v-list-group>
        <v-list-tile @click="$router.push('/members')">
          <v-list-tile-action>
            <v-icon>people</v-icon>
          </v-list-tile-action>
          <v-list-tile-title>Members</v-list-tile-title>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>
    <!-- Toolbar -->
    <v-toolbar 
      dark 
      color="primary" 
      >
      <v-toolbar-side-icon @click.stop="drawer = !drawer">
        
      </v-toolbar-side-icon>

      <v-toolbar-title class="white--text">Recruitment System</v-toolbar-title>

      <v-spacer></v-spacer>

      <v-btn icon>
        <v-icon>search</v-icon>
      </v-btn>

      <v-btn icon>
        <v-icon>apps</v-icon>
      </v-btn>

      <v-btn icon>
        <v-icon>refresh</v-icon>
      </v-btn>

      <v-btn icon>
        <v-icon>more_vert</v-icon>
      </v-btn>
    </v-toolbar>
    <!-- Content -->
    <v-content>
        <router-view></router-view>
    </v-content>
    <v-footer>2019</v-footer>
  </v-app>
</template>
<script>
export default {
    data() {
        return {
            title: "Recruitment System",
            aside: false,
            clipped: true,
            absolute: true,
            admins: [
                ['Management', 'people_outline'],
                ['Settings', 'settings']
            ],
            cruds: [
                ['Create', 'add'],
                ['Read', 'insert_drive_file'],
                ['Update', 'update'],
                ['Delete', 'delete']
            ],
            drawer: true,
            generalSetupItems: [
                { title: 'Company Information' }
            ],
            personalInformationItems: [
                { title: 'Citizenship' },
                { title: 'Religion' }
            ],
            employmentInformationItems: [
                { title: 'Employee Level' },
                { title: 'Organizational Level'}
            ]
        }
    },
    methods: {
        logout () {
            
        }
    }
}
</script>

