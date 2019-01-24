<template>
    <vuetify-layout-default>
        <v-container>
            <h1>{{title}}</h1>
        </v-container>
        <router-view/>
        <v-bottom-nav
            :active.sync="bottomNav"
            color="primary"
            :value="true"
            fixed
            shift
            dark>
            <v-btn dark flat value="hired" 
                @click.prevent="$router.push('/email-format')">
                <span>Hired</span>
                <v-icon>person</v-icon>
            </v-btn>

            <v-btn dark flat value="screening"
                @click.prevent="$router.push('/email-format/screening')">
                <span>Scheduled Screening</span>
                <v-icon>schedule</v-icon>
            </v-btn>

            <v-btn dark flat value="results"
                @click.prevent="$router.push('/email-format/results')">
                <span>Screening Entry Results</span>
                <v-icon>done</v-icon>
            </v-btn>

        </v-bottom-nav>
    </vuetify-layout-default>
</template>
<script>
import VuetifyLayoutDefault from "@/layouts/VuetifyLayoutDefault";
export default {
  components: {
    VuetifyLayoutDefault
  },
  data() {
    return {
      bottomNav: "hired",
      title: "Email Format Setup"
    };
  }
};
</script>
