
      $(document).on("click", ".delete-item", function (e) {
        e.preventDefault()
        let parent = $(this).parent().parent();
        let id = $(this).attr("data-id");
          let data = { id: id };
          console.log(id);
          console.log(parent);
        $.ajax({
            url: "Product/Delete",
            type: "Post",
            data: data,
            success: function () {
                parent.addClass("d-none");
                console.log("salam")
            }
        })
      })
$(document).on("click", ".archive-item", function (e) {
    e.preventDefault()
    let parent = $(this).parent().parent();
    let id = $(this).attr("data-id");
    let data = { id: id };
    $.ajax({
        url: "Product/Archive",
        type: "Post",
        data: data,
        success: function () {
            parent.addClass("d-none");
           
        }
    })
})
$(document).on("click", ".restore-item", function (e) {
    e.preventDefault()
    let parent = $(this).parent().parent();
    let id = $(this).attr("data-id");
    let data = { id: id };
    $.ajax({
        url: "Archive/GetArchivedProduct",
        type: "Post",
        data: data,
        success: function () {
            parent.addClass("d-none");

        }
    })
})

