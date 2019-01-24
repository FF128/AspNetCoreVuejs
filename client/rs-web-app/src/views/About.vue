<template>
  <div class="section">
    <label v-for="(value, key) in values" v-bind:key="key">{{value}}</label>
    <div class="columns">
      <div class="column is-4">
          <label class="label is-small">Lastname</label>
          <input class="input is-small" type="text" v-model="member.lastName" placeholder="Lastname">
      </div>
      <div class="column is-4">
          <div class="field">
              <label class="label is-small">Firstname</label>
              <div class="control">
                  <input class="input is-small" type="text" v-model="member.firstName" placeholder="Firstname">
              </div>
          </div>
      </div>
      <div class="column is-4">
          <div class="field">
              <label class="label is-small">Middlename</label>
              <div class="control">
                  <input class="input is-small" type="text" v-model="member.middleName" placeholder="Middlename">
              </div>
          </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      member: {},
      values: []
    };
  },
  methods: {
    getValues() {
      this.$axios
        .get("api/values")
        .then(response => {
          this.values = response.data;
        })
        .catch(err => {
          console.log(err);
        });
    }
  },
  created() {
    this.getValues();
  }
};
</script>
