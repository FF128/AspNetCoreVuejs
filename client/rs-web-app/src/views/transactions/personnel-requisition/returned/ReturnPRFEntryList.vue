<template>
  <div>
    <v-container>
      <h1>{{title}}</h1>
      <!-- <v-layout row wrap> -->
      <v-data-table :headers="headers" :items="returnedEntryData">
        <template slot="items" slot-scope="props">
          <td>
            <v-btn flat icon @click.prevent="$router.push(`returned-pr-entry/${props.item.prfNo}`)">
              <v-icon>arrow_right_alt</v-icon>
            </v-btn>
          </td>
          <td>{{props.item.prfNo}}</td>
          <td>{{props.item.description}}</td>
          <td>{{props.item.status}}</td>
        </template>
      </v-data-table>
      <!-- </v-layout> -->
    </v-container>
  </div>
</template>
<script>
import { mapState, mapActions } from "vuex";
export default {
  data() {
    return {
      title: "Returned PRF",
      headers: [
        { text: "", value: "actions", sortable: false },
        { text: "Transaction No.", value: "actions", sortable: false },
        { text: "Description", value: "actions", sortable: true },
        { text: "Status", value: "actions", sortable: false }
      ]
    };
  },
  methods: {
    ...mapActions("prEntry", ["getAllReturnedPREntryData"])
  },
  computed: {
    ...mapState("prEntry", {
      returnedEntryData: state => state.returnedEntryData
    })
  },
  created() {
    this.getAllReturnedPREntryData();
  }
};
</script>

