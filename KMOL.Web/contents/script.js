$(function () {
    $(".product-item").hover(function () { $(this).find(".p-view").show() }, function () { $(this).find(".p-view").hide() });

});


function lazyLoad() {
    $("img.lazy").lazyload({
        //threshold: 300,
        effect: "fadeIn"
    });
}

