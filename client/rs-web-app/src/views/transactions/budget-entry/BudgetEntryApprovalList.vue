<template>
    <div>
        <v-container>
            <h1>{{title}}</h1>
            <!-- <v-layout row wrap> -->
                <v-data-table
                    :headers="headers"
                    :items="budgetEntryData">
                    <template slot="items" slot-scope="props">
                        <td>
                            <v-btn flat icon 
                                @click.prevent="$router.push(`budget-entry-approval/${props.item.transactionNo}`)">
                                <v-icon>arrow_right_alt</v-icon>
                            </v-btn>
                        </td>
                        <td>{{props.item.transactionNo}}</td>
                        <td>{{props.item.description}}</td>
                        <td>{{props.item.status}}</td>
                    </template>
                </v-data-table>
            <!-- </v-layout> -->
        </v-container>
    </div>
</template>
<script>
import { mapState, mapActions } from "vuex"
export default {
    data()  {
        return {
            title: 'Budget Entry Approval',
            headers: [
                { text: '', value: 'actions', sortable: false},
                { text: 'Transaction No.', value: 'actions', sortable: false},
                { text: 'Description', value: 'actions', sortable: true},
                { text: 'Status', value: 'actions', sortable: false}
            ]
        }
    },
    methods: {
        ...mapActions('budgetEntryApp', [
            'getAllBudgetEntryData'
        ])
    },
    computed: {
        ...mapState('budgetEntryApp', {
            budgetEntryData: state => state.budgetEntryData
        })
    },
    created() {
        this.getAllBudgetEntryData();
    }
}
</script>

