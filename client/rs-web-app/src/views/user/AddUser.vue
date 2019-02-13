<template>
  <div>
    <v-form>
      <v-container>
        <h1>{{title}}</h1>
        <v-layout row wrap>
          <v-flex xs12 sm6 md6>
            <v-select :items="accountType" v-model="newUser.accountType" label="Account Type"></v-select>
            <v-text-field
              v-model="newUser.employee"
              label="Employee"
              :append-icon="'search'"
              @click:append="searchEmployee"
              :disabled="accountTypeUndefinedOrGuest"
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
          v-model="selectedDepartment"
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
            <v-text-field
              v-model="newUser.firstName"
              label="Firstname"
              :disabled="accountTypeUndefined"
            ></v-text-field>
            <v-text-field
              v-model="newUser.lastName"
              label="Lastname"
              :disabled="accountTypeUndefined"
            ></v-text-field>
            <v-text-field
              v-model="newUser.userName"
              label="Username"
              :disabled="accountTypeUndefined"
            ></v-text-field>
            <v-text-field
              v-model="newUser.password"
              :append-icon="showPassword ? 'visibility_off' : 'visibility'"
              @click:append="showPassword = !showPassword"
              :type="showPassword ? 'text' : 'password'"
              label="Password"
              :disabled="accountTypeUndefined"
            ></v-text-field>
            <v-text-field
              v-model="newUser.confirmPass"
              :append-icon="showConfirmPassword ? 'visibility_off' : 'visibility'"
              @click:append="showConfirmPassword = !showConfirmPassword"
              :type="showConfirmPassword ? 'text' : 'password'"
              label="Confirm Password"
              :disabled="accountTypeUndefined"
            ></v-text-field>
            <v-checkbox v-model="newUser.allowExpiDate" label="Allow Expiration Date"></v-checkbox>
            <v-text-field
              v-model="newUser.expirationDate"
              label="Expiration Date"
              type="date"
              :disabled="!newUser.allowExpiDate || accountTypeUndefined"
            ></v-text-field>
            <v-text-field
              v-model="newUser.email"
              label="Email Address"
              :disabled="accountTypeUndefined"
            ></v-text-field>
          </v-flex>
          <v-flex xs12 sm12 md12>
            <v-btn color="success" @click.prevent="add" :disabled="accountTypeUndefined">Add</v-btn>
          </v-flex>
        </v-layout>
      </v-container>
    </v-form>
    <look-up-dialog :title="'Employee Masterfile'" :dialog="empDialog" v-if="empDialog">
      <template slot="table">
        <v-text-field label="Search" v-model="query"></v-text-field>
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
              <v-btn flat @click.prevent="selectEmployee(props.item)">Select</v-btn>
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
      selectedDepartment: [],
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
      selectedEmployee: {},
      loading: false,
      pagination: {
        page: 1,
        rowsPerPage: 5,
        rowsPerPageItems: [5, 10, 25, 50, 100]
      },
      totalData: 0,
      query: "",
      passwordRules: [],
      passwordConfirmationRules: [
        v => !!v || "Confirmation E-mail is required",
        v => v == this.newuser.password || "E-mail must match"
      ],
      apiEndpointPayLoc: "api/payloc",
      apiEndpointEmp: "api/employee",
      apiEndpoint: "api/users"
    };
  },
  computed: {
    ...mapState("department", {
      departments: state => state.departments
    }),
    fullName() {
      return `${this.selectedEmployee.lName}, ${this.selectedEmployee.fName} ${
        this.selectedEmployee.mName
      }`;
    },
    accountTypeUndefined() {
      return typeof this.newUser.accountType == "undefined";
    },
    accountTypeGuest() {
      return this.newUser.accountType == "Guest";
    },
    accountTypeUndefinedOrGuest() {
      return (
        typeof this.newUser.accountType == "undefined" ||
        this.newUser.accountType == "Guest"
      );
    }
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
          this.loading = false;
        })
        .catch(({ response }) => {
          toast.show(response.data);
        });
    },
    add() {
      this.$axios.post(`${this.apiEndpoint}/register`, {
        User: this.newUser,
        UserDepartments: this.selectedDepartment,
        UserPayLocations: this.selectedPayLoc
      });
    },
    selectEmployee(item) {
      this.selectedEmployee = item;
      this.empDialog = false;
      this.newUser.empCode = item.empCode;
      this.newUser.employee = this.fullName;
    }
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
    },
    query: {
      handler(val) {
        this.getAllEmployees();
      },
      deep: true
    },
    newUser: {
      handler(val) {},
      deep: true
    }
  }
};
</script>


