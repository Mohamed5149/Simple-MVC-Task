function Delete(id) {
    let result = confirm("Are You Sure ?")
    if (result) {
        $.ajax({
            url: `/Employee/DeleteEmployee/${id}`,
            type: "GET",
            success: function (res) {
                if (res) {
                    $(`#${id}`).remove();
                }
            },
            error: function (err) {
                Console.log(err);
            }
        })
    }
}