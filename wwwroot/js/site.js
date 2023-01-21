//add activate class when web is active
window.onload = function (e) {
    e.stopImmediatePropagation();
    var all_links = document.querySelector("nav ul").getElementsByTagName("a"),
        i = 0, len = all_links.length,
        full_path = location.href.split('#')[0]; //Ignore hashes?

    // Loop through each link.
    for (; i < len; i++) {
        if (all_links[i].href.split("#")[0] == full_path) {
            all_links[i].parentElement.className += " active";
        }
    }

    //Preloader
    $(".se-pre-con").fadeOut("slow");
}