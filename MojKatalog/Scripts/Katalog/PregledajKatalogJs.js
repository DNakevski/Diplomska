$(document).ready(function () {
    var idKatalog = $("#IdKatalozi").val();
   // alert(idKatalog);
    $.get("/Katalog/VratiDrvoZaKatalog", { id: idKatalog }, function (data) {
        initializeJsTree(idKatalog, data.Trees);
    });
});

// Pocetok- Inicijaliziranje na drvoto za pretstavuvanje na Kategorii (/Kategorija/Index)
function initializeJsTree(id, trees) {
    //alert(JSON.stringify(trees));
    $("#jstree" + id)
                .jstree({
                    "core": {
                        // so that create works
                        "check_callback": false,
                        "data": trees
                    },
                    "plugins": ["search"]
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