function DoWithoutRefresh(btn,url) {
    $(document).on("click", btn, function (e) {
        e.preventDefault()
        let parent = $(this).parent().parent();
        let id = $(this).attr("data-id");
        let data = { id: id };

        $.ajax({
            url: url,
            type: "Post",
            data: data,
            success: function () {
                parent.addClass("d-none");

            }
        })
    })
}

DoWithoutRefresh(".delete-from-card", "Basket/Delete");
DoWithoutRefresh(".delete-item", "Product/Delete");
DoWithoutRefresh(".archive-item", "Product/Archive");
DoWithoutRefresh(".restore-item", "Archive/GetArchivedProduct");
   


$(document).on("click", ".add-card", function (e) {
    e.preventDefault()
    let id = $(this).attr("data-id");
    let data = { id: id };
    console.log(id)
    $.ajax({
        url: "Shop/AddBasket",
        type: "Post",
        data: data,
        success: function () {
           console.log("ok")

        }
    })
})

$('.reload-page').click(function () {
    location.reload();
});
$(".delete-from-card").click(function () {
    $('.reload-page').removeClass("d-none")
});
