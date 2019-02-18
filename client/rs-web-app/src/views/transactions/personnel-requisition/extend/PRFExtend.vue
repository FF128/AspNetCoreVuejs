<template>
  <div>
    <v-container>
      <v-layout>
        <v-flex xs12 sm12 md12>
          <v-btn icon @click.prevent="$router.back()">
            <v-icon>keyboard_backspace</v-icon>
          </v-btn>
        </v-flex>
      </v-layout>
      <h1>{{title}}</h1>
      <v-form>
        <v-container>
          <v-layout row wrap>
            <v-flex xs12 sm6 md3>
              <v-text-field label="PRF No." v-model="pr.prfNo" readonly></v-text-field>
            </v-flex>
            <v-flex xs12 sm6 md3>
              <v-text-field label="Description" v-model="pr.description"></v-text-field>
            </v-flex>
            <v-flex xs12 sm6 md3>
              <v-text-field label="Date Required" type="date" v-model="pr.dateRequired"></v-text-field>
            </v-flex>
            <v-flex xs12 sm6 md3>
              <v-text-field label="Duration From" type="date" v-model="pr.durationFrom"></v-text-field>
            </v-flex>
            <v-flex xs12 sm6 md3>
              <v-text-field label="Duration To" type="date" v-model="pr.durationTo"></v-text-field>
            </v-flex>
            <v-flex xs12 sm6 md3>
              <v-text-field label="Employee Status" v-model="pr.empStatus"></v-text-field>
            </v-flex>
            <v-flex xs12 sm6 md3>
              <v-text-field v-model="pr.reason" label="Personnel Request Type"></v-text-field>
            </v-flex>
            <v-flex xs12 sm6 md3>
              <v-text-field v-model="pr.remarks" label="Remarks"></v-text-field>
            </v-flex>
          </v-layout>
        </v-container>
      </v-form>
      <v-data-table
        :headers="prEntryHeaders"
        :items="prDetails"
        class="elevation-1"
        hide-actions
        v-if="prDetails[0] != null"
      >
        <template slot="items" slot-scope="props">
          <td>{{ props.item.prfNo }}</td>
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
      <v-layout>
        <v-btn color="success" v-if="status" @click="save">Submit</v-btn>
      </v-layout>
      <v-card class="ma-1 py-3">
        <v-card-title>Return Comments</v-card-title>
        <v-data-table :items="comments" :headers="commentsHeaders">
          <template slot="items" slot-scope="props">
            <td>{{props.item.comment}}</td>
            <td>{{props.item.commentedDate | dateFormat}}</td>
            <td>{{props.item.commentedBy}}</td>
          </template>
        </v-data-table>
      </v-card>
      <v-card class="ma-1 py-3">
        <v-card-title>Attachments</v-card-title>
        <v-data-table hide-actions class="elevation-1" :items="attachments">
          <template slot="items" slot-scope="props">
            <td>{{ props.item.name }}</td>
            <td>
              <v-btn icon @click.prevent="attachments.splice(props.index, 1)">
                <v-icon color="red">delete</v-icon>
              </v-btn>
            </td>
          </template>
        </v-data-table>
      </v-card>
    </v-container>
  </div>
</template>
<script>
import moment from "moment";
import Toast from "@/project-modules/toast";
let toast = new Toast();
export default {
  data() {
    return {
      title: "Extend PRF",
      pr: {},
      prDetails: [],
      prEntryHeaders: [
        { text: "PR No.", value: "prfNo" },
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
      comments: [],
      commentsHeaders: [],
      attachments: [],
      status: false,
      apiEndpoint: "api/pr"
    };
  },
  methods: {
    getPREntryDetails() {
      this.$axios
        .get(`${this.apiEndpoint}/${this.$route.params.prfNo}`)
        .then(({ data }) => {
          this.pr = data.header;
          this.prDetails = data.details;
          this.attachments = data.attachments;
        })
        .catch(({ response }) => {
          toast.show(response.data);
        });
    },
    getReturnedComments() {
      this.$axios
        .get(`${this.apiEndpoint}/comments/${this.$route.params.prfNo}`)
        .then(({ data }) => {
          this.comments = data;
        })
        .catch(({ response }) => {
          toast.show(response.data);
        });
    },
    checkStatus() {
      this.$axios
        .get(`${this.apiEndpoint}/extend/${this.$route.params.prfNo}`)
        .then(({ data }) => {
          this.status = data;
        })
        .catch(({ response }) => {
          toast.show(response.data);
        });
    },
    save() {
      this.$axios
        .post(`${this.apiEndpoint}/extend`, {
          newDateRequired: this.pr.dateRequired,
          newDateFrom: this.pr.durationFrom,
          newDateTo: this.pr.durationTo,
          prfNo: this.pr.prfNo
        })
        .then(({ data }) => {
          toast.show(data);
        })
        .catch(({ response }) => {
          toast.show(response.data);
        });
    }
  },
  watch: {
    pr(val) {
      val.dateRequired = moment(val.dateRequired).format("YYYY-MM-DD");
      val.durationFrom = moment(val.durationFrom).format("YYYY-MM-DD");
      val.durationTo = moment(val.durationTo).format("YYYY-MM-DD");
      val.dateRequested = moment(val.dateRequested).format("YYYY-MM-DD");
    }
  },
  created() {
    this.getPREntryDetails();
    this.getReturnedComments();
    this.checkStatus();
  }
};
</script>
