<template>
  <div>
    <v-form>
      <v-container>
        <h1>{{title}}</h1>
        <v-layout row wrap>
          <v-flex xs12 sm6 md6>
            <v-select :items="transactions" v-model="transUser.trans" label="Transactions"></v-select>
            <v-text-field
              v-model="transUser.user"
              label="User"
              :append-icon="'search'"
              @click:append="searchUsers"
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
        <v-flex xs12 sm12 md12>
          <v-btn color="success" @click.prevent="add">Add</v-btn>
        </v-flex>
      </v-container>
    </v-form>
    <v-container>
      <v-data-table :items="approversData" :headers="approversHeaders">
        <template slot="item" slot-scope="props"></template>
      </v-data-table>
    </v-container>
    <look-up-dialog :dialog="userDialog" :title="'Users'" v-if="userDialog">
      <template slot="table">
        <v-data-table :headers="userHeaders" :items="userData" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td>
              <v-btn icon @click.prevent="selectUser(props.item)">
                <v-icon>mdi-magnify-plus</v-icon>
              </v-btn>
            </td>
            <td>{{ props.item.id }}</td>
            <td class="text-xs-left">{{ props.item.empCode }}</td>
            <td class="text-xs-left">{{ props.item.username }}</td>
            <td class="text-xs-left">{{ props.item.fullName }}</td>
          </template>
        </v-data-table>
      </template>
      <template slot="card-actions">
        <v-btn flat="flat" @click.prevent="userDialog = false">Close</v-btn>
      </template>
    </look-up-dialog>
  </div>
</template>
<script>
import { mapState, mapActions } from "vuex";
import Toast from "@/project-modules/toast";
import LookUpDialog from "@/components/LookUpDialog";
let toast = new Toast();
export default {
  components: {
    LookUpDialog
  },
  data() {
    return {
      title: "Assign Users for Approving",
      transUser: {},
      transactions: ["Budget Approval", "PRF Approval"],
      userDialog: false,
      userHeaders: [
        { text: "", value: "action" },
        { text: "ID", value: "id" },
        { text: "Code", value: "empCode" },
        { text: "Username", value: "username" },
        { text: "Name", value: "fullName" }
      ],
      userData: [],
      selectedUser: {},
      payLocData: [],
      payLocHeaders: [
        { text: "Code", value: "locId" },
        { text: "Description", value: "locName" }
      ],
      selectedDepartment: [],
      selectedPayLoc: [],
      empData: [],
      selectedEmployee: {},
      approversData: [],
      approversHeaders: [
        { text: "", value: "action" },
        { text: "Emp Code", value: "action" },
        { text: "Name", value: "action" },
        { text: "Active", value: "action" },
        { text: "Order", value: "action" },
        { text: "Payroll Location", value: "action" },
        { text: "Department", value: "action" }
      ],
      apiEndpointPayLoc: "api/payloc",
      apiEndpointUser: "api/users",
      apiEndpointTransUser: "api/transuser"
    };
  },
  computed: {
    ...mapState("department", {
      departments: state => state.departments
    })
  },
  methods: {
    ...mapActions("department", ["getAllDepartments"]),
    searchUsers() {
      this.userDialog = true;
      this.getAllUsers();
    },
    selectUser(item) {
      this.userDialog = false;
      this.selectedUser = item;
      this.transUser.user = item.fullName;
      this.transUser.empCode = item.empCode;
    },
    getAllUsers() {
      this.$axios
        .get(`${this.apiEndpointUser}`)
        .then(({ data }) => {
          this.userData = data;
        })
        .catch(({ response }) => {
          toast.show(response.data);
        });
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
    add() {
      this.$axios
        .post(`${this.apiEndpointTransUser}`, {
          TransUser: this.transUser,
          TransUserPaylocations: this.selectedPayLoc,
          TransUserDepartments: this.selectedDepartment
        })
        .then(({ data }) => {
          toast.show(data);
        })
        .catch(({ response }) => {
          toast.show(response.data);
        });
    }
  },
  created() {
    this.getPayLocation();
    this.getAllDepartments();
  }
};
</script>
