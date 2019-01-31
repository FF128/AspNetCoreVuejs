<template>
  <v-container>
    <h1>Under Development...</h1>
    <v-layout row>
      <pagination></pagination>
    </v-layout>
    
  </v-container>
</template>

<script>
import toast from "@/components/CustomToast";
import Pagination from "@/components/Pagination"
export default {
  components: {
    Pagination
  },
  data() {
    return {
      snackbar: true
    };
  },
  created() {},
  methods: {
    show() {
      toast.info("This is an info toast");
    }
  }
};
</script>
