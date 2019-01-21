<template>
    <div>
        <v-container>
            <h1>{{title}}</h1>
            <v-expansion-panel  popout expand v-model="panel">
                <v-expansion-panel-content>
                    <div slot="header">General Information</div>
                    <general-info></general-info>
                </v-expansion-panel-content>
                <v-expansion-panel-content>
                    <div slot="header">Text Certification</div>
                    <text-certificate></text-certificate>
                </v-expansion-panel-content>
                <v-expansion-panel-content>
                    <div slot="header">Essay Questions Definition</div>
                    <!-- <general-info></general-info> -->
                </v-expansion-panel-content>
                <v-expansion-panel-content>
                    <div slot="header">Attachments Required</div>
                    <general-info></general-info>
                </v-expansion-panel-content>
                <v-expansion-panel-content>
                    <div slot="header">Source of Information</div>
                    <general-info></general-info>
                </v-expansion-panel-content>
            </v-expansion-panel>
           
        </v-container>
    </div>
</template>
<script>
import GeneralInfo from "./GeneralInfo"
import TextCertificate from "./TextCertificate"
export default {
    components: {
        GeneralInfo,
        TextCertificate
    },
    data() {
        return {
            title: 'Applicants Entry Setup',
            focusable: true,
            genInfo: true,
            panel: [true, false, false, false, false]
        }
    }
}
</script>

