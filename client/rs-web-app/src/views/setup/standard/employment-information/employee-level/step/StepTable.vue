<template>
     <v-data-table
        :headers="headers"
        :items="stepData"
        class="elevation-1">

        <template slot="items" slot-scope="props">
            <td>{{ props.item.code }}</td>
            <td class="text-xs-left">{{ props.item.description }}</td>
            <td>
                <v-btn icon
                    @click.prevent="edit(props.item)">
                    <v-icon>edit</v-icon>
                </v-btn>
                <v-btn icon
                    @click.prevent="deleteInfo(props.item)">
                    <v-icon color="red">delete</v-icon>
                </v-btn>
            </td>
        </template>
    </v-data-table>
</template>
<script>
export default {
    data() {
        return {
            
        }
    }
}
</script>
