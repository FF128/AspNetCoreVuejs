<template>
    <div>
        <v-form>
            <v-container>
                <h1>{{title}}</h1>
                <v-layout row wrap>
                    <v-flex xs12 sm4 md4>
                        <v-text-field
                            label="Code"
                            v-model="loc.locationCode">

                        </v-text-field>
                    </v-flex>
                    
                    <v-flex xs12 sm4 md8>
                        <v-text-field
                            label="Description"
                            v-model="loc.locationDesc">

                        </v-text-field>
                    </v-flex>

                    <v-flex xs12 sm4 md4>
                        <v-text-field
                            label="Head"
                            v-model="loc.headCode"
                            :append-icon="'mdi-account-search'"
                            @click:append="searchJobLevelCode"
                            readonly>

                        </v-text-field>
                    </v-flex>

                    <v-flex xs12 sm4 md4>
                        <v-text-field
                            label="Name"
                            v-model="loc.name"
                            readonly>

                        </v-text-field>
                    </v-flex>
                    
                    <v-flex xs12 sm12 md12>
                        <v-btn
                            color="success"
                            @click.prevent="save"
                            v-if="!onEdit">
                            Save
                        </v-btn>
                        <div v-if="onEdit">
                            <v-btn
                                color="success"
                                @click.prevent="update">
                                Update
                            </v-btn>
                            <v-btn
                                @click.prevent="cancel">
                                Cancel
                            </v-btn>
                        </div>
                    </v-flex>
                    
                </v-layout>
            </v-container>
        </v-form>
        <v-container>
            <v-data-table
                :headers="headers"
                :items="locationData"
                class="elevation-1">

                <template slot="items" slot-scope="props">
                    <td>{{ props.item.locationCode }}</td>
                    <td class="text-xs-left">{{ props.item.locationDesc }}</td>
                    <td class="text-xs-left">{{ props.item.headCode }}</td>
                    <td>
                        <v-btn icon
                            @click.prevent="edit(props.item)">
                            <v-icon>edit</v-icon>
                        </v-btn>
                        <v-btn icon
                            @click.prevent="deleteInfo(props.item)">
                            <v-icon color="red">delete</v-icon>
                        </v-btn>
                    </td>
                </template>
            </v-data-table>
        </v-container>
        <!-- Delete Confirmation Dialog -->
        <v-dialog
            v-model="deleteDialog"
            max-width="400">
            <v-card>
                <v-card-title class="headline">Confirmation</v-card-title>

                <v-card-text>
                    Do you want to delete this Code: {{ selectedLoc.locationCode }}?
                </v-card-text>

                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn
                        color="red"
                        flat="flat"
                        @click.prevent="deleteConfirmed">
                        Yes
                    </v-btn>

                    <v-btn
                        flat="flat"
                        @click.prevent="closeDeleteDialog">
                        No
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>

        <!-- Job Level Dialog -->
        <v-dialog
            v-model="jobLevelDialog"
            max-width="850">
            <v-card>
                <v-card-title class="headline">List of Job Level</v-card-title>

                <v-card-text>
                    <v-layout>
                         <v-data-table
                            :headers="jobLevelHeaders"
                            :items="jobLevelData"
                            class="elevation-1">

                            <template slot="items" slot-scope="props">
                                <td>{{ props.item.code }}</td>
                                <td class="text-xs-left">{{ props.item.description }}</td>
                                <td class="text-xs-left">{{ props.item.gradeCode }}</td>
                                <td class="text-xs-left">{{ props.item.stepCode }}</td>
                                <td class="text-xs-left">{{ props.item.minimumSalary }}</td>
                                <td class="text-xs-left">{{ props.item.maximumSalary }}</td>
                                <td>
                                    <v-btn icon
                                        @click.prevent="selectJobLevel(props.item)">
                                        <v-icon>mdi-magnify-plus</v-icon>
                                    </v-btn>
                                </td>
                            </template>
                        </v-data-table>
                    </v-layout>
                </v-card-text>

                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn
                        flat="flat"
                        @click.prevent="jobLevelDialog = false">
                        Close
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </div>
</template>
<script>
import { mapActions, mapState } from "vuex"

export default {
    data() {
        return {
            title: "Location",
            loc: {},
            isSaving: false,
            isUpdating: false,
            isDeleting: false,
            onEdit: false,
            deleteDialog: false,
            selectedLoc: {},
            headers: [
                {
                    text: 'Code',
                    align: 'left',
                    sortable: false,
                    value: 'branchCode'
                },
                { text: 'Description', value: 'branchDesc', align: 'left' },
                { text: 'Head Code', value: 'headCode', align: 'left'},
                { text: '', value: 'actions' }
            ],
            jobLevelHeaders: [
                {
                    text: 'Code',
                    align: 'left',
                    sortable: false,
                    value: 'code'
                },
                { text: 'Description', value: 'description', align: 'left' },
                { text: 'Grade', value: 'gradeDescription', align: 'left'},
                { text: 'Step', value: 'stepDescription', align: 'left'},
                { text: 'Minimum Salary', value: 'minimumSalary', align: 'left'},
                { text: 'Maximum Salary', value: 'maximumSalary', align: 'left'},
                { text: '', value: 'actions' }
            ],
            jobLevelDialog: false,
            apiEndpoint: "api/loc"
        }
    },
    methods: {
        ...mapActions('jobLevel', [
            'getAllJobLevels'
        ]),
        ...mapActions('location', [
            'getAllLocations'
        ]),
        save() {
            this.isSaving = true;
            this.$axios.post(this.apiEndpoint, this.loc)
                .then(response => {
                    this.isSaving = false;

                    // Update List
                    this.getAllLocations();
                    this.loc = {}
                })
                .catch(err => {
                    this.isSaving = false;
                });
        },
        edit(item){
            this.onEdit = true;
            this.loc = item;
        },
        update() {
            this.isUpdating = true
            this.$axios.put(this.apiEndpoint, this.loc)
                .then(response => {
                    this.isUpdating = false;
                    this.onEdit = false;

                    this.getAllLocations();
                })
                .catch(err => {
                    this.isUpdating = false;
                })
        },
        deleteInfo(item) {
            this.deleteDialog = true;
            this.selectedLoc = item;
        },
        deleteConfirmed() {
            this.isDeleting = true;
            this.$axios.delete(`${this.apiEndpoint}/${this.selectedLoc.id}`)
                .then(response => {
                    this.getAllLocations();
                    
                    this.isDeleting = false;
                    this.deleteDialog = false;
                })
                .catch(err => {
                    this.isDeleting = false;
                })
        },
        closeDeleteDialog() {
            this.deleteDialog = false;
        },
        cancel() {
            this.onEdit = false;
            this.loc = {}

            this.getAllLocations();
        },
        searchJobLevelCode() {
            this.jobLevelDialog = true;

            this.getAllJobLevels();
        },
        selectJobLevel(item) {
            this.branch.jobLevelCode = item.code;

            this.jobLevelDialog = false;
        }
    },
    computed: {
        ...mapState('jobLevel', {
            jobLevelData: state => state.jobLevels
        }),
        ...mapState('location', {
            locationData: state => state.locations,
            isLoading: state => state.loading
        })
    },
    created() {
        this.getAllLocations();
    }
}
</script>
