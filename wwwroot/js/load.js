$(document).ready(function () {

    $(document).on("click", ".load-more", function () {

        let parent = $(".parent-products-elem");

        let skip = $(parent).children().length;

        let datas = $(parent).attr("data-count");
        console.log(parent);
        console.log(datas);

        $.ajax({
            url: `shop/loadmore?skip=${skip}`,
            type: "Get",
            success: function (res) {

                $(parent).append(res);
                console.log(res);

                skip = $(parent).children().length;

                if (skip >= datas) {
                    $(".load-more").addClass("d-none");
                    $(".show-less").removeClass("d-none");
                }
            }

        })
    });
    $(document).on("click", ".load-more-home", function () {

        let parent = $(".parent-products-elem");

        let skip = $(parent).children().length;

        let datas = $(parent).attr("data-count");
        console.log(parent);
        console.log(datas);

        $.ajax({
            url: `shop/loadmore?skip=${skip}`,
            type: "Get",
            success: function (res) {

                $(parent).append(res);
                console.log(res);

                skip = $(parent).children().length;

                if (skip >= datas) {
                    $(".load-more").addClass("d-none");
                    $(".show-less").removeClass("d-none");
                }
            }

        })
    });

})