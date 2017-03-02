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
function removeUnicode(str) {
    str = str.toLowerCase();
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/!|@@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_/g, "-");

    str = str.replace(/-+-/g, "-"); //thay thế 2- thành 1- 
    str = str.replace(/^\-+|\-+$/g, "");

    return str;

}

$.views.converters("trimName", function (val) { if (val.length > 40) return val.substring(0, 40) + "..."; else return val });
$.views.converters("formatPrice", function (val) {
    return val.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
});
$.views.converters("urlEncode", function (val) {
    return removeUnicode(val);
});
