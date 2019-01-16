<template>
    <div>
        <v-form v-model="valid">
            <v-container>
                <h1>{{title}}</h1>
                <v-layout row wrap>
                    <v-flex xs12 sm4 md3>
                        <v-text-field
                            label="Code"
                            v-model="jobLevel.code"
                            :rules="codeRules"
                            :readonly="onEdit">

                        </v-text-field>
                    </v-flex>
                    
                    <v-flex xs12 sm4 md3>
                        <v-text-field
                            label="Description"
                            v-model="jobLevel.description">

                        </v-text-field>
                    </v-flex>

                    <v-flex xs12 sm4 md3>
                        <v-text-field
                            label="Grade"
                            v-model="jobLevel.gradeCode"
                            :append-icon="'mdi-account-search'"
                            @click:append="searchGradeCode"
                            readonly>

                        </v-text-field>
                    </v-flex>
                    <v-flex xs12 sm4 md3>
                        <v-text-field
                            label="Step"
                            v-model="jobLevel.stepCode"
                            :append-icon="'mdi-account-search'"
                            @click:append="searchStepCode"
                            readonly>

                        </v-text-field>
                    </v-flex>

                    <v-flex xs12 sm4 md3>
                        <v-text-field
                            label="Minimum Salary"
                            v-model="jobLevel.minimumSalary">

                        </v-text-field>
                    </v-flex>
                    <v-flex xs12 sm4 md3>
                        <v-text-field
                            label="Maximum Salary"
                            v-model="jobLevel.maximumSalary">

                        </v-text-field>
                    </v-flex>
                        <!-- Actions -->
                    <v-flex md12>
                        <v-btn
                            color="success"
                            @click.prevent="save"
                            v-if="!onEdit && valid">
                            Save
                        </v-btn>
                        <div v-if="onEdit && valid">
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
        <!-- Job Level Table -->
        <v-container>
            <v-data-table
                :headers="headers"
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

        <!-- Grade Dialog -->
        <v-dialog
            v-model="gradeDialog"
            max-width="600">
            <v-card>
                <v-card-title class="headline">List of Grades</v-card-title>

                <v-card-text>
                    <v-layout>
                         <v-data-table
                            :headers="gradeHeaders"
                            :items="gradeData"
                            class="elevation-1">

                            <template slot="items" slot-scope="props">
                                <td>{{ props.item.code }}</td>
                                <td class="text-xs-left">{{ props.item.description }}</td>
                                <td>
                                    <v-btn icon
                                        @click.prevent="selectGrade(props.item)">
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
                        @click.prevent="gradeDialog = false">
                        Close
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>

        <!-- Step Dialog -->
        <v-dialog
            v-model="stepDialog"
            max-width="600">
            <v-card>
                <v-card-title class="headline">List of Steps</v-card-title>

                <v-card-text>
                    <v-layout>
                        <v-data-table
                            :headers="gradeHeaders"
                            :items="stepData"
                            class="elevation-1">

                            <template slot="items" slot-scope="props">
                                <td>{{ props.item.code }}</td>
                                <td class="text-xs-left">{{ props.item.description }}</td>
                                <td>
                                    <v-btn icon
                                        @click.prevent="selectStep(props.item)">
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
                        @click.prevent="stepDialog = false">
                        Close
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>

        <!-- Delete Confirmation Dialog -->
        <v-dialog
            v-model="deleteDialog"
            max-width="400">
            <v-card>
                <v-card-title class="headline">Confirmation</v-card-title>

                <v-card-text>
                    Do you want to delete this Code: {{ selectedJobLevel.code }}?
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
    </div>
</template>
<script>
import { mapActions, mapState } from "vuex"
import Toast from "@/project-modules/toast"
import codeRules from "@/rules/codeRules"

let toast = new Toast();
export default {
    data() {
        return {
            title: "Job Level",
            jobLevel: {},
            codeRules,
            valid: false,
            isSaving: false,
            isUpdating: false,
            isDeleting: false,
            onEdit: false,
            deleteDialog: false,
            stepDialog: false,
            gradeDialog: false,
            selectedJobLevel: {},
            headers: [
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
            gradeHeaders: [
                {
                    text: 'Code',
                    align: 'left',
                    sortable: false,
                    value: 'code'
                },
                { text: 'Description', value: 'description', align: 'left' },
                { text: '', value: 'actions' }
            ],
            apiEndpoint: "api/job-level"
        }
    },
    methods: {
        ...mapActions('jobLevel', [
            'getAllJobLevels'
        ]),
        ...mapActions('step', [
            'getAllSteps'
        ]),
        ...mapActions('grade', [
            'getAllGrades'
        ]),
        save() {
            this.isSaving = true;
            this.$axios.post(this.apiEndpoint, this.jobLevel)
                .then(response => {
                    this.isSaving = false;

                    let { message, hasError } = response.data;

                    // Toast custom message
                    if(hasError) {
                        toast.error(message)
                    }else{
                        toast.success(message)
                    }

                    // Update List
                    this.cancel();
                })
                .catch(err => {
                    this.isSaving = false;
                });
        },
        edit(item){
            this.onEdit = true;
            this.jobLevel = item;
        },
        update() {
            this.isUpdating = true
            this.$axios.put(this.apiEndpoint, this.jobLevel)
                .then(response => {
                    this.isUpdating = false;
                    
                    let { message, hasError } = response.data;

                    // Toast custom message
                    if(hasError) {
                        toast.error(message)
                    }else{
                        toast.success(message)
                    }

                    this.cancel();
                })
                .catch(err => {
                    this.isUpdating = false;
                })
        },
        deleteInfo(item) {
            this.cancel();
            this.deleteDialog = true;
            this.selectedJobLevel = item;
        },
        deleteConfirmed() {
            this.isDeleting = true;
            this.$axios.delete(`${this.apiEndpoint}/${this.selectedJobLevel.id}`)
                .then(response => {
                    let { message, hasError } = response.data;

                    // Toast custom message
                    if(hasError) {
                        toast.error(message)
                    }else{
                        toast.success(message)
                    }

                    this.cancel();
                    
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
            this.jobLevel = {}

            this.getAllJobLevels();
        },
        searchStepCode() {
            this.stepDialog = true;

            this.getAllSteps();
        },
        searchGradeCode() {
            this.gradeDialog = true;

            this.getAllGrades();
        },
        selectStep(item) {
            this.jobLevel.stepCode = item.code;

            this.stepDialog = false;
        },
        selectGrade(item) {
            this.jobLevel.gradeCode = item.code;

            this.gradeDialog = false;
        }
    },
    computed: {
        ...mapState('jobLevel', {
            jobLevelData: state => state.jobLevels
        }),
        ...mapState('step', {
            stepData: state => state.steps
        }),
        ...mapState('grade', {
            gradeData: state => state.grades
        })
    },
    created() {
        this.getAllJobLevels();
    }
}
</script>
