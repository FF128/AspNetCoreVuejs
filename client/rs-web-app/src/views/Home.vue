<template>
  <v-container>
    <v-layout>
      <h1>On development...</h1>
    </v-layout>
    
  </v-container>
</template>

<script>
import toast from "@/components/CustomToast";

export default {
  components: {},
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
