<template>
   <div>
        <v-form>
            <v-container>
                <v-layout>
                    <v-flex xs12 sm12 md12>
                        <v-btn icon @click.prevent="$router.back()">
                            <v-icon>keyboard_backspace</v-icon>
                        </v-btn>    
                    </v-flex>
                </v-layout>
                <h1>{{title}}</h1>
                <v-layout row wrap>
                    
                    <v-flex xs12 sm6 md4>
                        <v-text-field
                            v-model="prMain.prfNo"
                            label="PRF No."
                            readonly>

                        </v-text-field>

                    </v-flex>
                    <v-flex xs12 sm6 md4>
                        <v-text-field
                            v-model="prMain.dateRequested"
                            label="Date Requested"
                            type="date"
                            readonly>
                        </v-text-field>
                    </v-flex>
                    <v-flex xs12 sm6 md4>
                        <v-text-field
                            v-model="prMain.durationFrom"
                            label="Duration From"
                            type="date"
                            readonly>

                        </v-text-field>

                    </v-flex>
                    <v-flex xs12 sm6 md4>
                        <v-text-field
                            v-model="prMain.durationTo"
                            label="Duration To"
                            type="date"
                            readonly>

                        </v-text-field>
                    </v-flex>
                    <v-flex xs12 sm6 md4>
                        <v-text-field
                            v-model="prMain.dateRequired"
                            label="Date Required"
                            type="date"
                            readonly>

                        </v-text-field>
                    </v-flex>
                    <v-flex xs12 sm6 md4>
                        <v-text-field
                            v-model="prMain.requestedBy"
                            label="Requested By"
                            readonly>

                        </v-text-field>
                    </v-flex>
                    <v-flex xs12 sm6 md4>
                        <v-text-field
                            v-model="prMain.empStatus"
                            label="Employment Status"
                            readonly>
                        </v-text-field>
                    </v-flex>
                    <v-flex xs12 sm6 md4>
                        <v-text-field
                            v-model="prMain.reason"
                            label="Reason for Personnel Request"
                            readonly>
                        </v-text-field>
                    </v-flex>
                    <v-flex xs12 sm6 md4>
                        <v-text-field
                            v-model="prMain.remarks"
                            label="Remarks"
                            readonly>
                        </v-text-field>
                    </v-flex>
                </v-layout>

                <v-data-table
                    :headers="prEntryHeaders"
                    :items="prDetails"
                    class="elevation-1"
                    hide-actions
                    v-if="prDetails[0] != null">
                    <template slot="items" slot-scope="props">
                        <td>
                            <v-btn icon
                                @click.prevent="edit(props.item)">
                                <v-icon color="blue">edit</v-icon>
                            </v-btn>
                        </td>
                        <td>{{ props.item.locName }}</td>
                        <td>{{ props.item.department }}</td>
                        <td>{{ props.item.location }}</td>
                        <td>{{ props.item.group }}</td>
                        <td>{{ props.item.division }}</td>
                        <td>{{ props.item.branch }}</td>
                        <td>{{ props.item.unit }}</td>
                        <td>{{ props.item.section }}</td>
                        <td>{{ props.item.designation }}</td>
                        <td>{{ props.item.agreedSalary }}</td>
                        <td>{{ props.item.quantity }}</td>
                        <td>{{ props.item.approverRemarks }}</td>
                    </template>
                </v-data-table>
                <v-spacer></v-spacer>
                &nbsp;
                <v-card v-if="typeof attachments[0] != 'undefined'">
                    <v-card-title>Attachments</v-card-title>
                    <v-list>
                        
                        <template v-for="(item, key) in attachments">
                            <v-divider :key="key"></v-divider>
                            <v-list-tile
                                :key="item.fileName"
                                @click="openFile(item)">
                                
                                <v-list-tile-content>
                                    <v-list-tile-title>{{item.fileName}}</v-list-tile-title>
                                </v-list-tile-content>
                            </v-list-tile>
                        </template>
                    </v-list>
                </v-card>
                &nbsp;
                <!-- List of Return Comments -->
                <v-card v-if="typeof comments[0] != 'undefined'">
                    <v-card-title>Return Comments</v-card-title>
                    <v-data-table
                        :items="comments"
                        :headers="commentsHeaders">
                        <template slot="items" slot-scope="props">
                            <td>{{props.item.comment}}</td>
                            <td>{{props.item.commentedDate | dateFormat}}</td>
                            <td>{{props.item.commentedBy}}</td>
                        </template>
                    </v-data-table>
                </v-card>

                <v-textarea
                    name="input-7-1"
                    label="Return Comment"
                    v-model="comment"
                ></v-textarea>
                <v-layout>
                    <v-flex xs12 sm12 md12>
                        <v-btn color="success" @click.prevent="accept">Accept</v-btn>
                        <v-btn color="error" @click.prevent="decline">Decline</v-btn>
                        <v-btn @click.prevent="returnEntry">Return</v-btn>
                    </v-flex>
                </v-layout>
                <v-layout row justify-center>
                    <v-dialog v-model="editDialog" persistent max-width="600px">
                        <v-card>
                            <v-card-title>
                                <span class="headline">Approver Remarks</span>
                            </v-card-title>
                            <v-card-text>
                                <v-flex xs12 sm12 md12>
                                    <v-text-field label="" v-model="remarks">

                                    </v-text-field>
                                </v-flex>
                            </v-card-text>
                            <v-card-actions>
                                <v-spacer></v-spacer>
                                <v-btn flat @click="editDialog = false">Close</v-btn>
                                <v-btn color="blue darken-1" flat @click="updateRemarks">Save</v-btn>
                            </v-card-actions>
                        </v-card>
                    </v-dialog>
                </v-layout>
            </v-container>
        </v-form>
   </div>
</template>
<script>
import Toast from "@/project-modules/toast"
import moment from "moment"
import appConfig from "@/config/config";

let toast = new Toast();
export default {
    data() {
        return {
            title: 'Personnel Requisition Entry - Approval',
            bed: [],
            apiEndpoint: 'api/pr',
            prMain: {},
            prDetails: [],
            attachments: [],
            comment: '',
            selectedDetails: {},
            prEntryHeaders: [
                { text: "", value: "actions" },
                {
                    text: "Payroll Location",
                    align: "left",
                    sortable: false,
                    value: "locName"
                },
                {
                    text: "Department",
                    align: "left",
                    sortable: false,
                    value: "department"
                },
                {
                    text: "Location",
                    align: "left",
                    sortable: false,
                    value: "location"
                },
                {
                    text: "Group",
                    align: "left",
                    sortable: false,
                    value: "group"
                },
                {
                    text: "Division",
                    align: "left",
                    sortable: false,
                    value: "division"
                },
                {
                    text: "Branch",
                    align: "left",
                    sortable: false,
                    value: "branch"
                },
                {
                    text: "Unit",
                    align: "left",
                    sortable: false,
                    value: "unit"
                },
                {
                    text: "Section",
                    align: "left",
                    sortable: false,
                    value: "section"
                },
                {
                    text: "Designation",
                    align: "left",
                    sortable: false,
                    value: "designation"
                },
                {
                    text: "Agreed Salary",
                    align: "left",
                    sortable: false,
                    value: "budgetSalary"
                },
                {
                    text: "Quantity",
                    align: "left",
                    sortable: false,
                    value: "quantity"
                },
                {
                    text: "Approver Remarks",
                    align: "left",
                    sortable: false,
                    value: "approverRemarks"
                }
                
            ],
            editDialog: false,
            remarks: '',
            comments: [],
            commentsHeaders: [
                { text: 'Comment', value: 'comment'},
                { text: 'Comment Date', value: 'commentedDate'},
                { text: 'Commented By', value: 'commentedBy'},
            ],
        }
    },
    methods: {
        getBudgetEntry() {
            this.$axios.get(`${this.apiEndpoint}/${this.$route.params.prfNo}`)
                .then(({ data }) => {
                    this.prMain = data.header;
                    this.prDetails = data.details
                })
                .catch(err => {

                });
        },
        accept() {
            this.$axios.post(`${this.apiEndpoint}/accept`, this.prMain)
                .then(({ data }) => {
                    toast.show(data);
                })
                .catch(({ response }) => {
                    toast.show(response.data);
                });
        },
        decline() {
            this.$axios.post(`${this.apiEndpoint}/decline`, this.prMain)
                .then(({ data }) => {
                    toast.show(data);
                })
                .catch(({ response }) => {
                    toast.show(response.data);
                });
        },
        returnEntry() {
            this.$axios.post(`${this.apiEndpoint}/return`,
                 { transactionNo: this.prMain.transactionNo, comment: this.comment }
                 )
                 .then(({ data }) => {
                     toast.show(data);
                     this.$router.back();
                 })
                 .catch(({ response }) => {
                      toast.show(response.data);
                 })
        },
        openFile(item) {
            window.open(`${appConfig.apiBaseUrl}/file/${item.transactionNo}/${item.fileName}/${item.folderName}`, '_blank');
        },
        edit(item) {
            this.selectedDetails = item
            this.remarks = item.approverRemarks
            this.editDialog = true
        },
        updateRemarks() {
            this.$axios.put(`${this.apiEndpoint}/remarks`,
                    { id: this.selectedDetails.id, remarks: this.remarks }
                ).then(({ data }) => {
                    toast.show(data);
                    this.editDialog = false;
                    this.getBudgetEntry();
                })
                .catch(({ response }) => {
                    toast.show(response.data);
                });
        },
        getReturnedComments() {
            this.$axios.get(`${this.apiEndpoint}/comments/${this.$route.params.prfNo}`)
                .then(({data }) => {
                    this.comments = data;
                })
                .catch(({response}) => {
                    toast.show(response.data);
                })
        }
    },
    created() {
        this.getBudgetEntry();
        // this.getReturnedComments();
    },
    watch: {
        prMain(val) {
            val.dateRequired = moment(val.dateRequired).format('YYYY-MM-DD')
            val.durationFrom = moment(val.durationFrom).format('YYYY-MM-DD')
            val.durationTo = moment(val.durationTo).format('YYYY-MM-DD')
            val.dateRequested = moment(val.dateRequested).format('YYYY-MM-DD')
        }
    }
}
</script>

