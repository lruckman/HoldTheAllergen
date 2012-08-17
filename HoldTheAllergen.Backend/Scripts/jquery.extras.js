(function ($) {
    $.fn.omgModal = function () {
        $(this).on('click', '.omg-modal', function (e) {
            e.preventDefault();
            var $link = $(this);
            var title = $link.text().trim();
            if (title.length === 0) {
                title = $link.attr('title');
            }
            var $dialog = $('<div class="modal fade"><div class="modal-header no-border"><a class="close" data-dismiss="modal">×</a></div><div class="modal-body"></div><div class="modal-footer"><a data-dismiss="modal" class="btn">Close</a><a href="#" class="btn btn-primary">' + title + '</a></div></div>');
            var formLoaded = function () {
                var $form = $dialog.find('form');
                $form.find(':input:visible:first').focus();
                $form.submit(function (e) {
                    e.preventDefault();
                    $form.validate();
                    if ($form.valid()) {
                        var url = $form.attr('action');
                        var data = $form.serialize();
                        $.post(url, data, function (result) {
                            if (result.Success) {
                                $dialog.modal('hide');
                                $('.row-fluid .span9').prepend('<div class="alert alert-success"><a class="close" data-dismiss="alert">×</a>' + result.Data + '</div>');
                                if (window.modalSaved) {
                                    window.modalSaved();
                                }
                            } else {
                                $form.replaceWith(result);
                                formLoaded();
                            }
                        });
                        $form.find('div.control-group').each(function () {
                            if ($(this).find('span.field-validation-error').length == 0) {
                                $(this).removeClass('error');
                            }
                        });
                    } else {
                        $form.find('div.control-group').each(function () {
                            if ($(this).find('span.field-validation-error').length > 0) {
                                $(this).addClass('error');
                            }
                        });
                    }
                });
                $form
                            .find('span.field-validation-valid, span.field-validation-error')
                            .each(function () {
                                $(this).addClass('help-inline');
                            });
                $form.find('div.control-group').each(function () {
                    if ($(this).find('span.field-validation-error').length > 0) {
                        $(this).addClass('error');
                    }
                });
            };
            $('body').append($dialog);
            $dialog
                    .modal()
                    .on('shown', function () {
                        $dialog.find('.modal-body').load($link.attr('href'), function () {
                            formLoaded();
                        });
                    })
                    .on('hidden', function () {
                        $dialog.empty().remove();
                    });
            $dialog
                    .find('.btn.btn-primary')
                    .click(function (e) {
                        e.preventDefault();
                        $dialog.find('form').submit();
                    });
        });
    };
})(jQuery);