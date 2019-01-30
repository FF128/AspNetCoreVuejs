<template>
    <v-list class="grey lighten-4">
         <v-list-tile v-for="(item,key) in recruitment"
            :key="key"
            @click="$router.push(item.link)">
            <!-- <v-list-tile-action>
                <v-icon>home</v-icon>
            </v-list-tile-action> -->
            <v-list-tile-title>{{item.title}}</v-list-tile-title>
        </v-list-tile>
    </v-list>
</template>
<script>
export default {
    data() {
        return {
            recruitment: [
                { title: 'Affiliations to Orgranization', link: "/affiliations"},
                { title: 'Personnel Request Type', link: "/personnel-req-type"},
                { title: 'Screening Type', link: "/screen-type"},
                { title: 'Screening Details', link: "/screen-details"},
                { title: 'Screening Ratings', link: "/ratings"},
                { title: 'Overall Ratings', link: "/overall-ratings"},
                { title: 'Pre-Employment Requirements', link: "/pre-emp-req"},
                { title: 'Designation Duties and Requirements', link: "/designation-duties-requirements"},
                { title: 'Job Group', link: "/job-group"},
                { title: 'Job Category', link: "/job-category"},
                { title: 'Applicants Entry', link: "/applicants-entry"},
                { title: 'Multi-Company Database', link: "/multi-company"},
                { title: 'Email Format', link: "/email-format"},
                { title: 'SMS Format', link: "/sms-format"},
                { title: 'Evaluator/Recruiter Screening Email Format', link: "/eval-email-format"}
            ]
        }
    }
}
</script>