<template>
     <v-dialog
        v-model="dialog"
        max-width="600px"
        lazy
        persistent>
        <v-card>
            <v-card-title class="headline">{{title}}</v-card-title>

            <v-card-text>
                <v-layout>
                    <v-container>
                        <slot name="table"></slot>
                    </v-container>
                </v-layout>
            </v-card-text>

            <v-card-actions>
                <v-spacer></v-spacer>
                <slot name="card-actions"></slot>
            </v-card-actions>
        </v-card>
    </v-dialog>

</template>
<script>
export default {
    props: {
        dialog: Boolean,
        title: String
    }
}
</script>
