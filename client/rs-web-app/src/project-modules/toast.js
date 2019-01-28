import toast from "@/components/CustomToast";
class Toast {
  success(msg) {
    toast.show({
      text: msg,
      color: "success",
      timeout: 3000
    });
  }

  error(msg) {
    toast.show({
      text: msg,
      color: "error",
      timeout: 3000
    });
  }

  show(msg, hasError) {
    if (hasError) {
      this.error(msg);
    } else {
      this.success(msg);
    }
  }
  show(data) {
    let { message, hasError } = data;
    if (hasError) {
      this.error(message);
    } else {
      this.success(message);
    }
  }
  validate(data) {
    let { message, errors, hasError} = data;
    let error = "";
    errors.filter(item => {
      error += `${item}\n`
    });
    if (hasError) {
      this.error(error);
    } else {
      this.success(error);
    }
  }

  // info(msg) {
  //     return vue.$toast.open({
  //         message: msg,
  //         position: "is-bottom-right",
  //         type: "is-info",
  //         duration: 5000
  //     });
  // }
}

export default Toast;
