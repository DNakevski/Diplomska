// Pocetok-Otstranuvanje na panelot za kategorija (/Kategorija/Index)
$(document).on('click', '.btn-delete', function (e) {
    e.preventDefault();
    $(this).parents(".panel").remove();
});
// Kraj-Otstranuvanje na panelot za kategorija (/Kategorija/Index)
$(document).ready(function () {
    $(".panel").on('hide.bs.collapse', function () {
        $(this).find(".glyphicon-chevron-down").addClass("displayNone");
        $(this).find(".glyphicon-chevron-right").removeClass("displayNone");
    });
    $('.panel').on('show.bs.collapse', function () {
        $(this).find(".glyphicon-chevron-down").removeClass("displayNone");
        $(this).find(".glyphicon-chevron-right").addClass("displayNone");
    })
    IzlistajKategorii();
    PanelAccordionCollapsedOut();
    //Pocetok- Dodavanje na nov panel/kategorija (/Kategorija/Index/DodadiKategorija-GET)
    $(".dodadi").click(function () {
        var id = $(this).attr("id");
        PanelAccordionCollapsedIn(id);
        id = id.replace("dodadi", "");
        CreateRootNode(id);
    });

    //Kraj- Dodavanje na nov panel (/Kategorija/Index/DodadiKategorija-GET)
});
//Pocetok- Dodavanje na RootKategorija  (/Kategorija/Index/)
function CreateRootNode(id) {
    $("#jstree" + id).jstree(true).create_node("#", { 'text': 'Nova kategorija' });
}
//Kraj- Dodavanje na RootKategorija vo baza (/Kategorija/Index/)

//Pocetok-Listanje na kategorii
function IzlistajKategorii() {
    $.get("/Kategorija/IzlistajKategorii/", function (data) {
        for (i = 0; i < data.length; i++) {
           // $("a[href='#collapse" + data[i].IdKatalog + "']").text(data[i].NazivNaKatalog+"ok");
            $("#jstree" + data[i].IdKatalog)
                .on('rename_node.jstree', function (e, dataF) {
                    var $id = dataF.node.id;
                    var $novnaziv = dataF.node.text;
                    var url = "/Kategorija/IzmeniKategorija/";
                    $.post(url, { id: $id, naziv: $novnaziv }, function (dataFF) { });
                })
                .on('delete_node.jstree', function (e, dataF) {
                    var $id = dataF.node.id;
                    var url = "/Kategorija/IzbrisiKategorija/" + $id;
                    $.get(url, function (dataFF) { });
                })
                .on('create_node.jstree', function (e, dataF) {
                    var naziv = dataF.node.text;
                    var oldId = dataF.node.id;
                    var parentid = dataF.parent;
                    if (parentid == "#") {
                        parentid = "0";
                    }
                    
                    var jsTreeId = $(this).attr("id");
                    
                    jsTreeId = jsTreeId.replace("jstree", "");

                    var url = "/Kategorija/DodadiKategorija/";
                    $.post(url, { naziv: naziv, roditel: parentid, katalogId: jsTreeId }, function (result) {
                        var newid = result;
                        var tree = $.jstree.reference("#jstree" + jsTreeId);
                        tree.set_id(oldId, newid);
                    });
                })
                .jstree({
                    'core': {
                    "check_callback": true,
                    'data': data[i].Trees 
                    },
                    "plugins": ["contextmenu", "search"],
                    "contextmenu": {
                        items: {
                            "create": {
                                "separator_before": false,
                                "separator_after": true,
                                "_disabled": false, //(this.check("create_node", data.reference, {}, "last")),
                                "label": "Додади",
                                "action": function (paramCreate) {
                                    var inst = $.jstree.reference(paramCreate.reference),
                                        obj = inst.get_node(paramCreate.reference);
                                    inst.create_node(obj, {}, "last", function (new_node) {
                                        setTimeout(function () { inst.edit(new_node); }, 0);
                                    });
                                }
                            },//end of create
                            "rename": {
                                "separator_before": false,
                                "separator_after": false,
                                "_disabled": false, //(this.check("rename_node", data.reference, this.get_parent(data.reference), "")),
                                "label": "Преименувај",
                                /*
                                "shortcut"			: 113,
                                "shortcut_label"	: 'F2',
                                "icon"				: "glyphicon glyphicon-leaf",
                                */
                                "action": function (paramRename) {
                                    var inst = $.jstree.reference(paramRename.reference),
                                        obj = inst.get_node(paramRename.reference);
                                    inst.edit(obj);
                                }
                            },//end of rename
                            "remove": {
                                "separator_before": false,
                                "icon": false,
                                "separator_after": false,
                                "_disabled": false, //(this.check("delete_node", data.reference, this.get_parent(data.reference), "")),
                                "label": "Избриши",
                                "action": function (data) {
                                    var inst = $.jstree.reference(data.reference),
                                        obj = inst.get_node(data.reference);
                                    if (inst.is_selected(obj)) {
                                        inst.delete_node(inst.get_selected());
                                    }
                                    else {
                                        inst.delete_node(obj);
                                    }
                                }
                            }//end of delete
                        }
                    }
                });
        };
    });

}
//Kraj-Listanje na kategorii
function PanelAccordionCollapsedOut() {
    $(".panel-accordion .navbar-katalog").attr("aria-expanded", "false");
    $(".panel-accordion .navbar-katalog").addClass("collapsed");

    $(".panel-accordion .panel-collapse").removeClass("in");
    $(".panel-accordion .panel-collapse").attr("aria-expanded", "false");
}
function PanelAccordionCollapsedIn(id) {
    PanelAccordionCollapsedOut();
    $("#" + id).parents(".panel").find(".navbar-katalog").attr("aria-expanded", "true");
    $("#" + id).parents(".panel").find(".navbar-katalog").removeClass("collapsed");

    $("#" + id).parents(".panel").find(".panel-collapse").addClass("in");
    $("#" + id).parents(" .panel").find(".panel-collapse").attr("aria-expanded", "true");
}
