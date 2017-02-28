function lazyLoad() {
    $("img.lazy").lazyload({
        //threshold: 300,
        effect: "fadeIn"
    });
}
function setDateForLink() {
    var date = $("input.used-date").val();
    $(".product-item a").each(function () {
        var href = $(this).attr("href").replace("{used-date}", date);
        $(this).attr("href", href);
    });

};
