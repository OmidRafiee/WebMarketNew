$(document).ready(function() {
    $('.navbar ul a').click(function(ev) {
        ev.preventDefault();
        var $this = $(this);
        
        $('html, body').animate({
            scrollTop: $($this.attr('href')).offset().top
        });
    });
});
