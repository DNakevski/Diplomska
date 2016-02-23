(function ($) {

    $.fn.paginate = function (options) {

        var settings = $.extend({
            listItemClass: "list-group-item",
            itemsOnPage: 10,
            startingPage:1
        }, options);

        //count list items
        var allListElements = this.find("." + settings.listItemClass);
        var elementsCount = allListElements.length;
        var pagesCount = getNumbetOfPages(elementsCount, settings.itemsOnPage);
        
        //remove previous pagination if exists
        var prevPagination = this.find(".navigation-container");
        if (prevPagination.length != 0) prevPagination.remove();

        //get and append the new pagination
        var html = getPaginationHtml(pagesCount, settings.startingPage);
        this.append(html);

        //show only elements on the active page
        var startingElement = (settings.startingPage * settings.itemsOnPage) - settings.itemsOnPage;
        var endElement = startingElement + settings.itemsOnPage;
        allListElements.hide();
        allListElements.slice(startingElement, endElement).fadeIn("medium");

        //bind click event on pagination links
        this.find("ul.pagination li a").on("click", function () {
            var page = parseInt($(this).html());
            startingElement = (page * settings.itemsOnPage) - settings.itemsOnPage;
            endElement = startingElement + settings.itemsOnPage;

            allListElements.hide();
            allListElements.slice(startingElement, endElement).fadeIn("medium");

            $(this).parent().parent().find("li.active").removeClass("active");
            $(this).parent().addClass("active");
            return false;
        });

        return this;
    };

    function getPaginationHtml(pages, activePage)
    {
        var html = "<div class='navigation-container' style='text-align:right; padding-top:0px'>";
        html += "<ul class='pagination' style='margin-top:0px!important;'>"
        for (var i = 1; i <= pages; i++) {
            if (i == activePage)
                html += "<li class='active'><a href='#'>" + i + "</a></li>";
            else
                html += "<li><a href='#'>" + i + "</a></li>";
        }
        html += "</ul>";
        html += "</div>";

        return html;
    }

    function getNumbetOfPages(elementsCount, itemsOnPage)
    {
        var pages = Math.floor(elementsCount / itemsOnPage);
        if ((elementsCount % itemsOnPage) != 0)
            pages++;

        return pages;
    }

}(jQuery));