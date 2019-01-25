<template>
    <div>
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
            app>
            <v-btn dark flat value="result" 
                @click.prevent="$router.push('/eval-email-format')">
                <span>Recruiter -> Result of Screening</span>
                <v-icon>person</v-icon>
            </v-btn>

            <v-btn dark flat value="rec-pending"
                @click.prevent="$router.push('/eval-email-format/recruiter-pending')">
                <span>Recruiter -> Pedning Screening</span>
                <v-icon>schedule</v-icon>
            </v-btn>

            <v-btn dark flat value="eval-pending"
                @click.prevent="$router.push('/eval-email-format/evaluator-pending')">
                <span>Evaluator -> Pending Screening</span>
                <v-icon>done</v-icon>
            </v-btn>

        </v-bottom-nav>
    </div>
</template>
<script>
import VuetifyLayoutDefault from "@/layouts/VuetifyLayoutDefault";
export default {
  components: {
    VuetifyLayoutDefault
  },
  data() {
    return {
      bottomNav: "result",
      title: "Evaluator/Recruiter Screening Email Format"
    };
  }
};
</script>
