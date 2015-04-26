// jQuery to collapse the navbar on scroll
$(window).scroll(function() {
    if ($(".navbar").offset().top > 50) {
        $(".navbar-fixed-top").addClass("top-nav-collapse");
    } else {
        $(".navbar-fixed-top").removeClass("top-nav-collapse");
    }
});

// jQuery for page scrolling feature - requires jQuery Easing plugin
$(function() {
    $('a.page-scroll').bind('click', function(event) {
        var $anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: $($anchor.attr('href')).offset().top
        }, 1500, 'easeInOutExpo');
        event.preventDefault();
    });
});

// Closes the Responsive Menu on Menu Item Click
$('.navbar-collapse ul li a').click(function() {
    $('.navbar-toggle:visible').click();
});

$.getJSON("http://petrol-web.herokuapp.com/stations?sorted=true", function(data) {
    var items = '',
        index = 1;
    data.forEach(function(listItem) {

        items += "<tr>" +
            "<td>" +
            "<span" + (index <= 3 ? " class='rank'>" : ">") + index + "</span>" +
            "</td>" +
            "<td>" +
            "<span class='station-name'>" + listItem.name + "</span>" +
            "<span class='address'>" + listItem.city + ", " + listItem.address + "</span>" +
            "</td>" +
            "<td>" + Math.round(listItem.liters * 10) / 10 + "l." + "</td>" +
            "<td>" + listItem.kilometers + "km." + "</td>" +
            "<td>" +
            "<span class='avg'>" + Math.round((listItem.liters / listItem.kilometers) * 1000) / 10 + "</span>" +
            "</td>" +
            "</tr>";
        index++;
    });

    $(".rank-list tbody").append(items);

});
