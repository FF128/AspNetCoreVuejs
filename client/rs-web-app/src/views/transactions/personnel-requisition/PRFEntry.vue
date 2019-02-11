<template>
    <div>
        <v-container>
            <h1>{{title}}</h1>
            <v-expansion-panel  popout expand v-model="panel">
                <v-expansion-panel-content>
                    <div slot="header">Fill out the Personnel Requisition Form</div>
                    <!-- <general-info></general-info> -->
                    <v-form v-model="valid">
                        <v-container>
                           <v-layout row wrap>
                               <v-flex xs12 sm6 md3>
                                   <v-text-field label="Description"
                                    v-model="pr.description">
                                       
                                   </v-text-field>
                               </v-flex>
                               <v-flex xs12 sm6 md3>
                                   <v-text-field label="Date Required"
                                    type="date"
                                    :rules="requiredRules"
                                    v-model="pr.dateRequired">
                                       
                                   </v-text-field>
                               </v-flex>
                               <v-flex xs12 sm6 md3>
                                   <v-text-field label="Duration From"
                                    type="date"
                                    :rules="requiredRules"
                                    v-model="pr.durationFrom">
                                       
                                   </v-text-field>
                               </v-flex>
                               <v-flex xs12 sm6 md3>
                                   <v-text-field label="Duration To"
                                    type="date"
                                    :rules="requiredRules"
                                    v-model="pr.durationTo">
                                       
                                   </v-text-field>
                               </v-flex>
                               <v-flex xs12 sm6 md3>
                                    <v-text-field
                                        v-model="pr.empStatus"
                                        label="Employment Status"
                                        :append-icon="'search'"
                                        @click:append="searchEmploymentCode"
                                        readonly>

                                    </v-text-field>
                                </v-flex>
                                <v-flex xs12 sm6 md3>
                                    <v-text-field
                                        v-model="pr.reason"
                                        label="Personnel Request Type"
                                        :append-icon="'search'"
                                        @click:append="searchPRTCode"
                                        readonly>

                                    </v-text-field>
                                </v-flex>
                           </v-layout>
                        </v-container>
                        <v-container>
                            <v-layout row>
                                <v-radio-group v-model="radioGroup" row>
                                    <v-radio label="Budgeted" value="budgeted" color="primary"></v-radio>
                                    <v-btn icon color="primary" 
                                        v-if="radioGroup == 'budgeted'" 
                                        small
                                        @click.prevent="searchBudgetEntries">
                                        <v-icon>search</v-icon>
                                    </v-btn>
                                    <v-radio label="Not Budgeted" value="notBudgeted" color="primary"></v-radio>
                                </v-radio-group>
                            </v-layout>
                        </v-container>
                    </v-form>
                </v-expansion-panel-content>
            </v-expansion-panel>
            <v-data-table
                :items="selectedEntries"
                :headers="budgetEntriesHeaders"
                v-if="typeof selectedEntries[0] != 'undefined'">
                    <template slot="items" slot-scope="props" >
                        <td >{{ props.item.budgetDetailsID }}</td>
                        <td >{{ props.item.transactionNo }}</td>
                        <td >{{ props.item.description }}</td>
                        <td >{{ props.item.durationFrom | dateFormat }}</td>
                        <td >{{ props.item.durationTo | dateFormat }}</td>
                        <td >{{ props.item.locName }}</td>
                        <td >{{ props.item.department }}</td>
                        <td >{{ props.item.location }}</td>
                        <td >{{ props.item.group }}</td>
                        <td >{{ props.item.branch }}</td>
                        <td >{{ props.item.unit }}</td>
                        <td >{{ props.item.section }}</td>
                        <td >{{ props.item.designation }}</td>
                        <td >{{ props.item.division }}</td>
                        <td >{{ props.item.quantity }}</td>
                        <td >{{ props.item.agreedSalary }}</td>
                    </template>
            </v-data-table>
            <v-layout>
                <v-btn
                    @click.prevent="save"
                    color="success"
                    v-if="typeof selectedEntries[0] != 'undefined'">
                    Submit
                </v-btn>
            </v-layout>
        </v-container>
        <look-up-dialog :dialog="employmentStatusDialog" :title="'Employee Status'" v-if="employmentStatusDialog">
            <template slot="table">
                <v-data-table
                    :headers="empStatusHeaders"
                    :items="employeeStatusFiles"
                    class="elevation-1">

                    <template slot="items" slot-scope="props">
                        <td>{{ props.item.code }}</td>
                        <td class="text-xs-left">{{ props.item.description }}</td>
                        <td>
                            <v-btn icon
                                @click.prevent="selectEmpStatus(props.item)">
                                <v-icon>mdi-magnify-plus</v-icon>
                            </v-btn>
                        </td>
                    </template>
                </v-data-table>
            </template>
            <template slot="card-actions">
                <v-btn
                    flat="flat"
                    @click.prevent="employmentStatusDialog = false">
                    Close
                </v-btn>
            </template>
        </look-up-dialog>
        <!-- Personnel Req Type -->
        <look-up-dialog :dialog="prtDialog" :title="'Personnel Request Type'" v-if="prtDialog">
            <template slot="table">
                <v-data-table
                    :headers="prtHeaders"
                    :items="prtData"
                    class="elevation-1">

                    <template slot="items" slot-scope="props">
                        <td>{{ props.item.personnelReqTypeCode }}</td>
                        <td class="text-xs-left">{{ props.item.personnelReqTypeDesc }}</td>
                        <td>
                            <v-btn icon
                                @click.prevent="selectPRT(props.item)">
                                <v-icon>mdi-magnify-plus</v-icon>
                            </v-btn>
                        </td>
                    </template>
                </v-data-table>
            </template>
            <template slot="card-actions">
                <v-btn
                    flat="flat"
                    @click.prevent="prtDialog = false">
                    Close
                </v-btn>
            </template>
        </look-up-dialog>
        <look-up-dialog :title="'Available Budget Entries'" :dialog="budgetEntryDialog" v-if="budgetEntryDialog">
            <template slot="table">
                <v-data-table
                    :items="budgetEntries"
                    :headers="budgetEntriesHeaders"
                    select-all
                    item-key="budgetDetailsID"
                    v-model="selectedEntries">
                    <template slot="items" slot-scope="props" >
                        <td>
                            <v-checkbox
                                v-model="props.selected"
                                primary
                                hide-details>
                            </v-checkbox>
                        </td>
                        <td >{{ props.item.budgetDetailsID }}</td>
                        <td >{{ props.item.transactionNo }}</td>
                        <td >{{ props.item.description }}</td>
                        <td >{{ props.item.durationFrom | dateFormat }}</td>
                        <td >{{ props.item.durationTo | dateFormat }}</td>
                        <td >{{ props.item.locName }}</td>
                        <td >{{ props.item.department }}</td>
                        <td >{{ props.item.location }}</td>
                        <td >{{ props.item.group }}</td>
                        <td >{{ props.item.branch }}</td>
                        <td >{{ props.item.unit }}</td>
                        <td >{{ props.item.section }}</td>
                        <td >{{ props.item.designation }}</td>
                        <td >{{ props.item.division }}</td>
                        <td >{{ props.item.quantity }}</td>
                        <td >{{ props.item.agreedSalary }}</td>
                    </template>
                </v-data-table>
            </template>
            <template slot="card-actions">
                <v-btn
                    flat
                    color="error"
                    @click.prevent="budgetEntryDialog = false">
                    Close
                </v-btn>
            </template>
        </look-up-dialog>
    </div>
</template>
<script>
import { mapState, mapActions } from "vuex"
import LookUpDialog from "@/components/LookUpDialog"
import requiredRules from "@/rules/requiredRules"
import Toast from "@/project-modules/toast";
import _ from 'lodash'
import moment from "moment"

let toast = new Toast();
export default {
    components: {
        LookUpDialog
    },
    data() {
        return {
            title: 'Personnel Requisition Entry',
            panel: [true],
            requiredRules,
            valid: false,
            pr: {
                dateRequired: moment(new Date()).format('YYYY-MM-DD'),
                durationFrom: moment(new Date()).format('YYYY-MM-DD'),
                durationTo: moment(new Date()).format('YYYY-MM-DD'),
            },
            employmentStatusDialog: false,
            prtDialog: false,
            budgetEntryDialog: false,
            radioGroup: 'budgeted',
            budgetEntries: [],
            budgetEntriesHeaders: [
                { text: '', value: 'budgetDetailsID'},
                { text: 'Trans No.', value: 'transactionNo'},
                { text: 'Description', value: 'description'},
                { text: 'Duration From', value: 'durationFrom'},
                { text: 'Duration To', value: 'durationTo'},
                { text: 'Payroll Location', value: 'locName'},
                { text: 'Department', value: 'department'},
                { text: 'Location', value: 'location'},
                { text: 'Group', value: 'group'},
                { text: 'Branch', value: 'branch'},
                { text: 'Unit', value: 'unit'},
                { text: 'Section', value: 'section'},
                { text: 'Designation', value: 'designation'},
                { text: 'Division', value: 'division'},
                { text: 'Quantity', value: 'quantity'},
                { text: 'Agreed Salary', value: 'agreedSalary'}
            ],
            selectedEntries: [],
            apiEndpoint: 'api/pr',
            apiBudgetEntry: 'api/budget-entry'
        }
    },
    computed: {
         ...mapState('employeeStatusFile', {
            employeeStatusFiles: state => state.employeeStatusFiles,
            empStatusHeaders: state => state.headers
        }),
        ...mapState('personnelReqType', {
            prtData: state => state.prtData,
            prtHeaders: state => state.headers
        })
    },
    methods: {
        ...mapActions('employeeStatusFile', [
            'getAllEmployeeStatusFiles'
        ]),
        ...mapActions('personnelReqType', [
            'getAllPRTData'
        ]),
        searchEmploymentCode() {
            this.employmentStatusDialog = true;
            this.getAllEmployeeStatusFiles();
        },
        selectEmpStatus(item) {
            this.pr.empStatus = item.code
            this.employmentStatusDialog = false
        },
        searchPRTCode() {
            this.prtDialog = true
            this.getAllPRTData();
        },
        selectPRT(item) {
            this.pr.reason = item.personnelReqTypeCode;
            this.prtDialog = false
        },
        searchBudgetEntries() {
            this.budgetEntryDialog = true
            this.getBudgetEntries();
        },
        getBudgetEntries() {
            this.$axios.get(`${this.apiEndpoint}/budget-entries`)
                .then(({ data }) => {
                    this.budgetEntries = data;
                })
                .catch(({ response }) => {
                    toast.show(response.data)
                })
        },
        save() {
            this.$axios.post(`${this.apiEndpoint}`, {
                header: this.pr,
                details: this.selectedEntries,
                isBudgeted: this.radioGroup == 'budgeted' ? true : false
            })
            .then(({ data }) => {
                toast.show(data);
            })
            .catch(({ response }) => {
                toast.show(response.data);
            })
        }
    }
}
</script>

