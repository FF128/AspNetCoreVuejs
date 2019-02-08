<template>
    <div>
        <v-container>
            <h1>{{title}}</h1>
            <v-data-table
                :headers="headers"
                :items="auditTrailData"
                :pagination.sync="pagination"
                :rows-per-page-items="pagination.rowsPerPageItems"
                :total-items="totalData"
                :loading="loading"
                class="elevation-1"
                >
                <template slot="items" slot-scope="props">
                    <td>{{ props.item.id }}</td>
                    <td class="text-xs-right">{{ props.item.trans }}</td>
                    <td class="text-xs-right">{{ props.item.module }}</td>
                    <td class="text-xs-right">{{ props.item.message }}</td>
                </template>
            </v-data-table>
        </v-container>
    </div>
</template>
<script>
export default {
    data() {
        return {
            title: 'Audit Trail',
            totalData: 0,
            auditTrailData: [],
            loading: false,
            pagination: {
                page: 1,
                rowsPerPage: 10,
                rowsPerPageItems: [10, 25, 50, 100]
            },
            headers: [
                { text: 'Id', value: 'id' },
                { text: 'Transaction', value: 'trans' },
                { text: 'Module', value: 'module' },
                { text: 'Message', value: 'message' },
            ],
            query: 'a',
            apiEndpoint: 'api/audit-trail'
        }
    },
    methods: {
        getAuditTrail() {
            this.loading = true
            this.$axios.get(`${this.apiEndpoint}/pageNum/${this.pagination.page}/pageSize/${this.pagination.rowsPerPage}/query/${this.query}`)
                .then(({ data }) => {
                    this.auditTrailData = data;
                    this.totalData = data[0].totalRecords
                    // this.pagination.totalItems = data[0].TotalRecords
                    this.loading = false
                })
                .catch(err => {
                    console.log(err);
                });
        }
    },
    watch: {
      pagination: {
        handler (val) {
        //   this.getDataFromApi()
        //     .then(data => {
        //       this.desserts = data.items
        //       this.totalDesserts = data.total
        //     })
            if(val.rowsPerPage == -1){
                val.rowsPerPage = this.totalData
            }
            this.getAuditTrail();
        },
        deep: true
      }
    },
    mounted() {
        this.getAuditTrail();
    }
}
</script>
