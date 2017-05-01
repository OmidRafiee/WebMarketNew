(function ($) {
    $.fn.price = function () {
        return this.each(function () {
            var allow1 = true;
            var chars1 = new Array(11);
            $(this).on("keypress", function (event) {
                var charCode = (event.which) ? event.which : window.event.keyCode;
                var returned = false;
                if ($(this).val() == "") {
                    allow1 = true;
                }
                chars1[0] = 49;
                chars1[1] = 50;
                chars1[2] = 51;
                chars1[3] = 52;
                chars1[4] = 53;
                chars1[5] = 54;
                chars1[6] = 55;
                chars1[7] = 56;
                chars1[8] = 57;
                chars1[9] = 48;
                chars1[10] = 8;
                if (allow1 == true) {
                    for (var i = 0; i < chars1.length; i++) {
                        if (chars1[i] == charCode) {
                            returned = true;
                            break;
                        }
                        else {
                            returned = false;
                        }
                    }
                }
                return returned;
            });
        });
    };
})(jQuery);