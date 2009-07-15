/// <reference path="jquery-1.3.2-vsdoc2.js"/>

(function($) {
    var selectAllInstances = [];

    $.fn.selectAll = function(items, options) {
        var opts = $.extend({}, $.fn.selectAll.defaults, options);

        var $self = getChildrenForContainer(this);
        var $items = getChildrenForContainer($(items)).filter(':enabled');
        var itemsLength = $items.length;
        selectAllInstances.push($self);

        $self.checkHeaderCheckbox = function() {
            $self.attr('checked', itemsLength == $items.filter(':checked').length);
        };

        $self.checkHeaderCheckbox();

        $items.click(function() {
            $self.checkHeaderCheckbox();
        });

        return $self.click(function() {
            $items.attr('checked', $self.is(':checked'));

            jQuery.each(selectAllInstances, function(index, value) {
                if (value != $self) {
                    value.checkHeaderCheckbox();
                }
            });
        });
    };

    function getChildrenForContainer($cont) {
        if (!$cont.is(':checkbox')) {
            return $cont.find(':checkbox');
        }

        return $cont;
    }

    $.fn.selectAll.defaults = {};
})(jQuery);