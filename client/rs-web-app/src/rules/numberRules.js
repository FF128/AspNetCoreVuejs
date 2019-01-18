export default  [
    (v) => !!v || 'Code is required',
    (v) => v && v.length <= 10 || 'Code must be less than 10 characters'
]