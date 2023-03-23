function EditAccount() {
    if (confirm("Are you sure you want to delete this item?")) {
        document.querySelectorAll('input').forEach(input => {
            input.readOnly = false;
        });
        console.log("Account is now editable");
    } else {
        document.querySelectorAll('input').forEach(input => {
            input.readOnly = true;
        });
        console.log("Account is now editable");
    }
}

