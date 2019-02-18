<template>
  <div>
    <v-container>
      <h1>{{title}}</h1>
      <v-data-table
        :items="transactions"
        :headers="transactionHeaders"
        class="elevation-1"
        :rows-per-page-items="pagination.rowsPerPageItems"
        :pagination.sync="pagination"
        :loading="loading"
        :total-items="totalData"
      >
        <template slot="items" slot-scope="{ item }">
          <td>
            <v-btn flat icon @click.prevent="$router.push(`prf-extend/${item.prfNo}`)">
              <v-icon>arrow_right_alt</v-icon>
            </v-btn>
          </td>
          <td>{{item.prfNo}}</td>
          <td>{{item.description}}</td>
          <td>{{item.status}}</td>
          <td>{{item.headerStatus}}</td>
          <td>{{item.extendStatus}}</td>
        </template>
      </v-data-table>
    </v-container>
  </div>
</template>
<script>
export default {
  data() {
    return {
      title: "Extend PRF",
      transactions: [],
      transactionHeaders: [
        { text: "", value: "action" },
        { text: "PRF No.", value: "prfNo" },
        { text: "Description", value: "description" },
        { text: "Status", value: "status" },
        { text: "Header Status", value: "headerStatus" },
        { text: "Extend Status", value: "extendStatus" }
      ],
      pagination: {
        page: 1,
        rowsPerPage: 10,
        rowsPerPageItems: [10, 25, 50]
      },
      totalData: 0,
      loading: false,
      query: "",
      apiEndpoint: "api/pr"
    };
  },
  methods: {
    getAllTransactions() {
      this.loading = true;
      this.$axios
        .get(
          `${this.apiEndpoint}/extend/pageNum/${
            this.pagination.page
          }/pageSize/${this.pagination.rowsPerPage}/query/${this.query}`
        )
        .then(({ data }) => {
          this.transactions = data;
          this.totalData = data[0].totalRecords;
          this.loading = false;
        })
        .catch(({ response }) => {});
    }
  },
  watch: {
    pagination: {
      handler(val) {
        if (val.rowsPerPage == -1) {
          val.rowsPerPage = this.totalData;
        }
        this.getAllTransactions();
      },
      deep: true
    }
  },
  created() {
    this.getAllTransactions();
  }
};
</script>
