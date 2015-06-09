// Pocetok-Otstranuvanje na panelot za kategorija (/Kategorija/Index)
$(document).on('click', '.btn-delete', function (e) {
    e.preventDefault();
    $(this).parents(".panel").remove();
});
// Kraj-Otstranuvanje na panelot za kategorija (/Kategorija/Index)


$(document).ready(function () {
    IzlistajKategorii();
    PanelAccordionCollapsedOut();
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


    //Pocetok- Dodavanje na nov panel/kategorija (/Kategorija/Index/DodadiKategorija-GET)
    $(".btn-create").click(function () {
        var idAttr = $(".panel-accordion .panel-title:last a:first").attr("href");

        if (idAttr == null) { idAttr = 0; }
        else { idAttr = idAttr.replace("#collapse", ""); }

        PanelAccordionCollapsedOut();

        var url = "/Kategorija/DodadiKategorija/" + idAttr;
        $.get(url, function (data) {
            $(".panel-accordion").append(data);
            initializeJsTree(parseInt(idAttr) + 1);
            CreateRootNode(parseInt(idAttr) + 1);
            var offset = $(".panel-accordion h4:last").offset();
            $("html, body").animate({
                scrollTop: offset.top
            }, 500);
        });
       
    });

    //Kraj- Dodavanje na nov panel (/Kategorija/Index/DodadiKategorija-GET)
    setActiveLink();
});


// Pocetok- Dodavanje na clasata "active" na glavnoto meni (_Layout) 
function setActiveLink()
{
    var activeLink = $("#activeLink").val();

    if (activeLink != null) {
        $("#" + activeLink).addClass("active");
    }
}
// Kraj- Dodavanje na clasata "active" na glavnoto meni (_Layout) 


// Pocetok- Inicijaliziranje na drvoto za pretstavuvanje na Kategorii (/Kategorija/Index)
function initializeJsTree(id) {
    $("#jstree" + id)
                .on('rename_node.jstree', function (e, data) {
                    var $id = data.node.id;
                    var $novnaziv = data.node.text;
                    var url = "/kategorija/izmenikategorija/";
                    $.post(url, { id: $id, naziv: $novnaziv }, function (data) { });
                })
                 .on('delete_node.jstree', function (e, data) {
                    var $id = data.node.id;
                    var url = "/kategorija/izbrisikategorija/" + $id;
                    $.get(url, function (data) { });
                 })
                .on('create_node.jstree', function (e, data) {
                    var naziv = data.node.text;
                    var oldId = data.node.id;
                    var parentid = data.parent;
                    if (parentid == "#") {
                        parentid = "0";
                    }
                    var tree = $.jstree.reference("#jstree" + id);
                    
                    var url = "/kategorija/dodadikategorija/";
                    $.post(url, { naziv: naziv, roditel: parentid }, function (result) {
                        var newid = result;
                        tree.set_id(data.node, newid);
                    });
                })
                .jstree({
                    "core": {
                        // so that create works
                        "check_callback": true
                    },
                    "plugins": ["contextmenu", "search"]
                });

    var to = false;
    var ref = $("#jstree" + id).jstree(true);
    var sel = ref.get_node("root" + id);
    if (sel) {
        ref.edit(sel);
    }
    $("#search_q").keyup(function () {
        if (to) { clearTimeout(to); }
        to = setTimeout(function () {
            var v = $("#search_q").val();
            $("#jstree" + id).jstree(true).search(v);
        }, 250);
    });
}
// Kraj- Inicijaliziranje na drvoto za pretstavuvanje na Kategorii (/Kategorija/Index)


//Pocetok- Dodavanje na RootKategorija  (/Kategorija/Index/)
function CreateRootNode(id) {
    $("#jstree" + id).jstree(true).create_node("#", { 'text': 'Nova kategorija' });
}
//Kraj- Dodavanje na RootKategorija vo baza (/Kategorija/Index/)

//Pocetok-Listanje na kategorii
function IzlistajKategorii() {
    $.get("/Kategorija/IzlistajKategorii/", function (data) {
        // alert(JSON.stringify(data));
       
    
        for (i = 0; i < data.length; i++) {
            $("#jstree" + data[i].IdKatalog).jstree({
                'core': {
                    'data': data[i].Trees
                }
            });
        };
    });
}
//Kraj-Listanje na kategorii
function PanelAccordionCollapsedOut() {
    $(".panel-accordion .panel-title a").attr("aria-expanded", "false");
    $(".panel-accordion .panel-title a").addClass("collapsed");

    $(".panel-accordion .panel-collapse").removeClass("in");
    $(".panel-accordion .panel-collapse").attr("aria-expanded", "false");
}