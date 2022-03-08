window.ShowToastr = (type, message) => {
  if (type === "success") {
    toastr.success(message, "Operation Successful", { timeout: 5000 });
  }
  if (type === "error") {
    toastr.error(message, "Operation Failed!", { timeout: 5000 });
  }
}

window.SweetAlert = (type, message) => {
  if (type === "success") {
    Swal.fire(
      'Success Notification',
      message,
      'success'
    )
  }
  if (type === "error") {
    Swal.fire(
      'Error Notification',
      message,
      'error'
    )
  }
}