<template>
  <div>
    <v-form>
      <v-container>
        <h1>{{title}}</h1>
        <v-layout row wrap>
          <v-flex xs12 sm6 md6>
            <v-select :items="accountType" label="Account Type"></v-select>
            <v-text-field
              v-model="newUser.empCode"
              label="Employee"
              :append-icon="'search'"
              @click:append="searchEmployee"
              readonly
            ></v-text-field>
          </v-flex>
        </v-layout>
      </v-container>
      <!-- Payroll Location -->
      <v-container>
        <h3>Payroll Location</h3>
        <v-data-table
          :items="payLocData"
          class="elevation-1"
          v-model="selectedPayLoc"
          :headers="payLocHeaders"
          hide-actions
          select-all
          item-key="locId"
        >
          <template slot="items" slot-scope="props">
            <td>
              <v-checkbox v-model="props.selected" primary hide-details></v-checkbox>
            </td>
            <td>{{ props.item.locId }}</td>
            <td class="text-xs">{{ props.item.locName }}</td>
          </template>
        </v-data-table>
      </v-container>
      <!-- Department -->
      <v-container>
        <h3>Deparment</h3>
        <v-data-table
          :items="departments"
          class="elevation-1"
          v-model="selectedPayLoc"
          :headers="payLocHeaders"
          hide-actions
          select-all
          item-key="departmentCode"
        >
          <template slot="items" slot-scope="props">
            <td>
              <v-checkbox v-model="props.selected" primary hide-details></v-checkbox>
            </td>
            <td>{{ props.item.departmentCode }}</td>
            <td class="text-xs">{{ props.item.departmentDesc }}</td>
          </template>
        </v-data-table>
      </v-container>
      <v-container>
        <v-layout row wrap>
          <v-flex xs12 sm6 md6>
            <v-text-field v-model="newUser.firstName" label="Firstname"></v-text-field>
            <v-text-field v-model="newUser.lastName" label="Lastname"></v-text-field>
            <v-text-field v-model="newUser.userName" label="Username"></v-text-field>
            <v-text-field
              v-model="newUser.password"
              :append-icon="showPassword ? 'visibility_off' : 'visibility'"
              @click:append="showPassword = !showPassword"
              :type="showPassword ? 'text' : 'password'"
              label="Password"
            ></v-text-field>
            <v-text-field
              v-model="newUser.confirmPass"
              :append-icon="showConfirmPassword ? 'visibility_off' : 'visibility'"
              @click:append="showConfirmPassword = !showConfirmPassword"
              :type="showConfirmPassword ? 'text' : 'password'"
              label="Confirm Password"
            ></v-text-field>
            <v-checkbox v-model="newUser.allowExpiDate" label="Allow Expiration Date"></v-checkbox>
            <v-text-field
              v-model="newUser.expirationDate"
              label="Expiration Date"
              type="date"
              :disabled="!newUser.allowExpiDate"
            ></v-text-field>
            <v-text-field v-model="newUser.email" label="Email Address"></v-text-field>
          </v-flex>
          <v-flex xs12 sm12 md12>
            <v-btn color="success" click.prevent="add">Add</v-btn>
          </v-flex>
        </v-layout>
      </v-container>
    </v-form>
    <look-up-dialog :title="'Employee Masterfile'" :dialog="empDialog" v-if="empDialog">
      <template slot="table">
        <v-data-table
          :items="empData"
          :headers="empHeaders"
          :pagination.sync="pagination"
          :rows-per-page-items="pagination.rowsPerPageItems"
          :total-items="totalData"
          :loading="loading"
        >
          <template slot="items" slot-scope="props">
            <td>
              <v-btn flat>Select</v-btn>
            </td>
            <td>{{props.item.empCode}}</td>
            <td>{{props.item.lName}}</td>
            <td>{{props.item.fName}}</td>
            <td>{{props.item.mName}}</td>
          </template>
        </v-data-table>
      </template>
      <template slot="card-actions">
        <v-btn flat color="error" @click.prevent="empDialog = false">Close</v-btn>
      </template>
    </look-up-dialog>
  </div>
</template>
<script>
import { mapState, mapActions } from "vuex";
import LookUpDialog from "@/components/LookUpDialog";
import requiredRules from "@/rules/requiredRules";
import Toast from "@/project-modules/toast";
let toast = new Toast();
export default {
  components: {
    LookUpDialog
  },
  data() {
    return {
      title: "Add User",
      newUser: {},
      accountType: ["Employee", "Guest"],
      payLocData: [],
      payLocHeaders: [
        { text: "Code", value: "locId" },
        { text: "Description", value: "locName" }
      ],
      selectedPayLoc: [],
      showPassword: false,
      showConfirmPassword: false,
      empDialog: false,
      empHeaders: [
        { text: "", value: "action", sortable: false },
        { text: "Code", value: "empCode" },
        { text: "Lastname", value: "lname" },
        { text: "Firstname", value: "fname" },
        { text: "Middlename", value: "mname" }
      ],
      empData: [],
      loading: false,
      pagination: {
        page: 1,
        rowsPerPage: 5,
        rowsPerPageItems: [5, 10, 25, 50, 100]
      },
      totalData: 0,
      query: "",
      apiEndpointPayLoc: "api/payloc",
      apiEndpointEmp: "api/employee"
    };
  },
  computed: {
    ...mapState("department", {
      departments: state => state.departments
    })
  },
  methods: {
    ...mapActions("department", ["getAllDepartments"]),
    searchEmployee() {
      this.empDialog = true;
      this.getAllEmployees();
    },
    getPayLocation() {
      this.$axios
        .get(`${this.apiEndpointPayLoc}`)
        .then(({ data }) => {
          this.payLocData = data;
        })
        .catch(({ response }) => {
          toast.show(response.data);
        });
    },
    getAllEmployees() {
      this.loading = true;
      this.$axios
        .get(
          `${this.apiEndpointEmp}?pageSize=${
            this.pagination.rowsPerPage
          }&pageNum=${this.pagination.page}&query=${this.query}`
        )
        .then(({ data }) => {
          this.empData = data;
          this.totalData = data[0].totalRecords;
          console.log(data);
          this.loading = false;
        })
        .catch(({ response }) => {
          toast.show(response.data);
        });
    },
    add() {}
  },
  created() {
    this.getPayLocation();
    this.getAllDepartments();
  },
  watch: {
    pagination: {
      handler(val) {
        if (val.rowsPerPage == -1) {
          val.rowsPerPage = this.totalData;
        }
        this.getAllEmployees();
      },
      deep: true
    }
  }
};
</script>


