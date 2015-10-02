$(document).ready(function () {
   
    //Pocetok- Podesuvanje na efektot Slide na User drop-down meni (_Layout) 
    $(".dropdown").hover(function () {
        $(".dropdown-menu").stop(true, true).slideDown();
    }, function () {
        $(".dropdown-menu").stop(true, true).slideUp()
    });
    $(".dropdown").click(function () {
        $(".dropdown-menu").slideToggle();
    });
    //Kraj- Podesuvanje na efektot Slide na User drop-down meni (_Layout) 
    setActiveLink();
    $(window).on("load resize", function () {
        $(".table-container").css("min-height", window.innerHeight - $(".navigation-bar").height());
    });
});


// Pocetok- Dodavanje na clasata "active" na glavnoto meni (_Layout) 
function setActiveLink() {
    var activeLink = $("#activeLink").val();

    if (activeLink != null) {
        $("#" + activeLink).addClass("active");
    }
}
// Kraj- Dodavanje na clasata "active" na glavnoto meni (_Layout) 


