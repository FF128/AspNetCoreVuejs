<template>
  <div>
    <v-container>
      <h1>{{title}}</h1>
      <v-form>
        <v-container>
          <v-layout row>
            <v-radio-group v-model="radioGroup" row>
              <v-radio label="Search" value="search"></v-radio>
              <v-radio label="All" value="all"></v-radio>
            </v-radio-group>
          </v-layout>
        </v-container>

        <v-container>
          <v-layout wrap>
            <v-flex xs12 sm6 md3>
              <v-text-field
                v-model="pr.prfNo"
                label="PRF No."
                :append-icon="'search'"
                @click:append="searchTransactions"
                readonly
                :disabled="radioGroup == 'all'"
              ></v-text-field>
            </v-flex>
            <v-flex xs12 sm6 md3>
              <v-text-field v-model="pr.description" label="Description" readonly></v-text-field>
            </v-flex>
            <v-flex xs12 sm6 md3>
              <v-text-field v-model="pr.durationFrom" label="Date Requested" type="date" readonly></v-text-field>
            </v-flex>
            <v-flex xs12 sm6 md3>
              <v-text-field v-model="pr.durationFrom" label="Duration From" type="date" readonly></v-text-field>
            </v-flex>
            <v-flex xs12 sm6 md3>
              <v-text-field v-model="pr.durationTo" type="date" label="Duration To" readonly></v-text-field>
            </v-flex>
            <v-flex xs12 sm6 md3>
              <v-text-field v-model="pr.dateRequired" type="date" label="Date Required" readonly></v-text-field>
            </v-flex>
            <v-flex xs12 sm6 md3>
              <v-text-field v-model="pr.requestedBy" label="Requested By" readonly></v-text-field>
            </v-flex>
            <v-flex xs12 sm6 md3>
              <v-text-field v-model="pr.empStatus" label="Employee Status" readonly></v-text-field>
            </v-flex>
            <v-flex xs12 sm6 md3>
              <v-text-field v-model="pr.reason" label="Reason for Personnel Request" readonly></v-text-field>
            </v-flex>
            <v-flex xs12 sm6 md3>
              <v-text-field v-model="pr.approverRemarks" label="Remarks" readonly></v-text-field>
            </v-flex>
          </v-layout>
        </v-container>
      </v-form>
      <!-- Selected Transaction -->
      <v-data-table
        :items="items"
        :headers="bedHeaders"
        :loading="tableLoading"
        v-if="radioGroup == 'search'"
      >
        <template slot="items" slot-scope="props">
          <td>{{props.item.id}}</td>
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
      <!-- All -->
      <v-data-table
        :items="allItems"
        :headers="bedHeaders"
        :loading="tableLoading"
        :rows-per-page-items="pagination.rowsPerPageItems"
        :pagination.sync="pagination"
        :total-items="totalData"
        v-if="radioGroup == 'all'"
      >
        <template slot="items" slot-scope="props">
          <td>{{props.item.id}}</td>
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

      <look-up-dialog :dialog="transDialog" :title="'PR History'" v-if="transDialog">
        <template slot="table">
          <v-data-table
            :headers="transHeaders"
            :items="prfHeaderMaintItems"
            class="elevation-1"
            :rows-per-page-items="paginationHeader.rowsPerPageItems"
            :pagination.sync="paginationHeader"
            :loading="prfLoading"
            :total-items="prfHeaderMaintTotalData"
          >
            <template slot="items" slot-scope="props">
              <td>
                <v-btn icon @click.prevent="selectPR(props.item)">
                  <v-icon>mdi-magnify-plus</v-icon>
                </v-btn>
              </td>
              <td>{{ props.item.prfNo }}</td>
              <td class="text-xs-left">{{ props.item.description }}</td>
              <td class="text-xs-left">{{ props.item.dateRequested | dateFormat}}</td>
              <td class="text-xs-left">{{ props.item.durationFrom | dateFormat}}</td>
              <td class="text-xs-left">{{ props.item.durationTo | dateFormat}}</td>
              <td class="text-xs-left">{{ props.item.dateRequired | dateFormat}}</td>
              <td class="text-xs-left">{{ props.item.requestedBy}}</td>
              <td class="text-xs-left">{{ props.item.empStatus}}</td>
              <td class="text-xs-left">{{ props.item.reason}}</td>
              <td class="text-xs-left">{{ props.item.remarks }}</td>
            </template>
          </v-data-table>
        </template>
        <template slot="card-actions">
          <v-btn flat="flat" @click.prevent="transDialog = false">Close</v-btn>
        </template>
      </look-up-dialog>
    </v-container>
  </div>
</template>
<script>
import moment from "moment";
import Toast from "@/project-modules/toast";
import LookUpDialog from "@/components/LookUpDialog";

let toast = new Toast();
export default {
  components: {
    LookUpDialog
  },
  data() {
    return {
      title: "PR History",
      pr: this.initializePR(),
      allItems: [],
      items: [],
      bedHeaders: [
        { text: "ID", value: "id" },
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
      pagination: {
        page: 1,
        rowsPerPage: 10,
        rowsPerPageItems: [10, 25, 50]
      },

      totalData: 0,
      radioGroup: "search",
      transactions: [],
      transHeaders: [
        { text: "", value: "actions" },
        { text: "PR No.", value: "prfNo", sortable: true },
        { text: "Description", value: "description", sortable: true },
        { text: "Date Requested", value: "dateRequested", sortable: true },
        { text: "Duration From", value: "durationFrom", sortable: true },
        { text: "Duration To", value: "durationTo", sortable: true },
        { text: "Date Required", value: "dateRequired", sortable: true },
        { text: "Requested By", value: "requestedBy", sortable: true },
        { text: "Emp status", value: "empStatus", sortable: true },
        { text: "Reason", value: "reason", sortable: true },
        { text: "Remarks", value: "remarks", sortable: true }
      ],
      tableLoading: false,
      transDialog: false,
      prfHeaderMaintItems: [],
      prfHeaderMaintTotalData: 0,
      paginationHeader: {
        page: 1,
        rowsPerPage: 10,
        rowsPerPageItems: [10, 25, 50]
      },
      prfLoading: false,
      apiEndpoint: "api/pr"
    };
  },
  methods: {
    searchTransactions() {
      this.transDialog = true;
      this.getMainHeader();
    },
    initializePR() {
      return {
        durationFrom: moment(new Date()).format("YYYY-MM-DD"),
        durationTo: moment(new Date()).format("YYYY-MM-DD"),
        dateRequired: moment(new Date()).format("YYYY-MM-DD"),
        dateRequested: moment(new Date()).format("YYYY-MM-DD")
      };
    },
    // getAllItems() {
    //     this.tableLoading = true
    //     this.$axios.get(`${this.apiEndpoint}/details`)
    //         .then(({ data }) => {
    //             this.items = data;
    //             this.tableLoading = false
    //         })
    //         .catch(({ response }) => {
    //             toast.show(response.data);
    //         })
    // },
    getAllItemsPagination() {
      this.tableLoading = true;
      this.$axios
        .get(
          `${this.apiEndpoint}/details/pageNum/${
            this.pagination.page
          }/pageSize/${this.pagination.rowsPerPage}/query/${this.query}`
        )
        .then(({ data }) => {
          this.allItems = data;
          this.totalData = data[0].totalRecords;
          this.tableLoading = false;
        })
        .catch(({ response }) => {
          toast.show(response.data);
        });
    },
    getItemsByPRFNo() {
      this.tableLoading = true;

      this.$axios
        .get(`${this.apiEndpoint}/details/${this.pr.prfNo}`)
        .then(({ data }) => {
          this.items = data;
          this.tableLoading = false;
        })
        .catch(({ response }) => {
          toast.show(response.data);
        });
    },
    getMainHeader() {
      //   this.$axios
      //     .get(`${this.apiEndpoint}/main`)
      //     .then(({ data }) => {
      //       this.transactions = data;
      //     })
      //     .catch(({ response }) => {
      //       toast.show(response.data);
      //     });
      this.$axios
        .get(
          `${this.apiEndpoint}/header/pageNum/${
            this.paginationHeader.page
          }/pageSize/${this.paginationHeader.rowsPerPage}/query/${
            this.queryHeader
          }`
        )
        .then(({ data }) => {
          this.prfHeaderMaintItems = data;
          this.prfHeaderMaintTotalData = data[0].totalRecords;
          this.prfLoading = false;
        })
        .catch(({ response }) => {
          toast.show(response.data);
        });
    },
    selectPR(item) {
      this.pr = item;
      this.transDialog = false;
      this.getItemsByPRFNo(); // Get PR Details by PRF No.
    }
  },
  watch: {
    pr(val) {
      val.durationFrom = moment(val.durationFrom).format("YYYY-MM-DD");
      val.durationTo = moment(val.durationTo).format("YYYY-MM-DD");
      val.dateRequired = moment(val.dateRequired).format("YYYY-MM-DD");
      val.dateRequested = moment(val.dateRequested).format("YYYY-MM-DD");
    },
    radioGroup(val) {
      if (val == "search") {
        this.pr = {};
        this.items = [];
      }
      if (val == "all") {
        this.pr = {};
        this.items = [];

        //this.getAllItems();
        this.getAllItemsPagination();
      }
    },
    pagination: {
      handler(val) {
        if (val.rowsPerPage == -1) {
          val.rowsPerPage = this.totalData;
        }
        this.getAllItemsPagination();
      },
      deep: true
    },
    paginationHeader: {
      handler(val) {
        if (val.rowsPerPage == -1) {
          val.rowsPerPage = this.prfHeaderMaintTotalData;
        }
        this.getMainHeader();
      },
      deep: true
    }
  }
};
</script>

