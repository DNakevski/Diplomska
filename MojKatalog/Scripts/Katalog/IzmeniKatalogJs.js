$(document).ready(function(){
    var idKatalog = $("#IdKatalozi").val();
    $.get("/Katalog/VratiDrvoZaKatalog", { id: idKatalog }, function (data) {
        //alert(JSON.stringify(data.Trees));
        initializeJsTree(idKatalog, data.Trees);
    });
});

// Pocetok- Inicijaliziranje na drvoto za pretstavuvanje na Kategorii (/Kategorija/Index)
function initializeJsTree(id, trees) {
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
                        "check_callback": true,
                        "data" : trees
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