<template>
  <v-container>
    <v-layout>
      <v-btn @click="show">Show</v-btn>
    </v-layout>
    
  </v-container>
</template>

<script>
import toast from "@/components/CustomToast"


  export default {
    components: {
    },
    data() {
      return {
        snackbar: true,
      }
    },
    created() {
      
    },
    methods: {
      show() {
        toast.info('This is an info toast');
      }
    }
  } 
</script>
