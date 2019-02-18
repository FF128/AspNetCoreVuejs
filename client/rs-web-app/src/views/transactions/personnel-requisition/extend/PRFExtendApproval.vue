<template>
  <div>
    <v-container>
      <h1>{{title}}</h1>
      <v-data-table :items="items" :headers="headers">
        <template slot="items" slot-scope="{item}">
          <td>
            <v-btn flat icon @click.prevent="approve(item.id)">
              <v-icon color="success">check_circle</v-icon>
            </v-btn>
            <v-btn flat icon @click.prevent="decline(item.id)">
              <v-icon color="error">close</v-icon>
            </v-btn>
          </td>
          <td>{{item.prfNo}}</td>
          <td>{{item.oldDateFrom | dateFormat }}</td>
          <td>{{item.oldDateTo | dateFormat }}</td>
          <td>{{item.oldDateRequired | dateFormat }}</td>
          <td>{{item.newDateFrom | dateFormat }}</td>
          <td>{{item.newDateTo | dateFormat }}</td>
          <td>{{item.newDateRequired | dateFormat }}</td>
          <td>{{item.status}}</td>
          <td>{{item.createdBy}}</td>
          <td>{{item.createdDate | dateFormat}}</td>
        </template>
      </v-data-table>
    </v-container>
  </div>
</template>
<script>
import Toast from "@/project-modules/toast";

let toast = new Toast();
export default {
  data() {
    return {
      title: "Extend PRF Approval",
      headers: [
        { text: "", value: "actions" },
        { text: "PRF No.", value: "prfNo" },
        { text: "Old Date From", value: "oldDateFrom" },
        { text: "Old Date To", value: "oldDateTo" },
        { text: "Old Date Required", value: "oldDateRequired" },
        { text: "New Date From", value: "newDateFrom" },
        { text: "New Date To", value: "newDateTo" },
        { text: "New Date Required", value: "newDateRequired" },
        { text: "Status", value: "status" },
        { text: "Created By", value: "createdBy" },
        { text: "Created Date", value: "createdDate" }
      ],
      items: [],
      apiEndpoint: "api/pr"
    };
  },
  methods: {
    getAllPRFExtend() {
      this.$axios
        .get(`${this.apiEndpoint}/extend/waiting`)
        .then(({ data }) => {
          this.items = data;
        })
        .catch(({ response }) => {
          toast.show(response.data);
        });
    },
    approve(id) {
      this.$axios
        .post(`${this.apiEndpoint}/extend/approved/${id}`)
        .then(({ data }) => {
          this.getAllPRFExtend();
          toast.show(data);
        })
        .catch(({ response }) => {
          toast.show(response.data);
        });
    },
    decline(id) {}
  },
  created() {
    this.getAllPRFExtend();
  }
};
</script>
