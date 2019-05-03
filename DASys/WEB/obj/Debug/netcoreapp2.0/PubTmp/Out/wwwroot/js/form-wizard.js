//function scroll_to_class(element_class, removed_height) {
//    var scroll_to = $(element_class).offset().top - removed_height;
//    if ($(window).scrollTop() != scroll_to) {
//        $('html, body').stop().animate({ scrollTop: scroll_to }, 0);
//    }
//}

function bar_progress(progress_line_object, direction) {
    var number_of_steps = progress_line_object.data('number-of-steps');
    var now_value = progress_line_object.data('now-value');
    var new_value = 0;
    if (direction == 'right') {
        new_value = now_value + (100 / (number_of_steps-1));
    }
    else if (direction == 'left') {
        new_value = now_value - (100 /( number_of_steps-1));
    }
    progress_line_object.attr('style', 'width: ' + new_value + '%;').data('now-value', new_value);
}

jQuery(document).ready(function () {
    
    $('.f1 fieldset:first').fadeIn('slow');

    // next step
    $('.f1 .btn-next').on('click', function () {
        
        var id = $(this).parents("fieldset").eq(0).attr("id");
        var result = 0;
        var total = 0;
        $("#" + id).find(':input, select')
            .each(function () {
                result = webApp.validateInputsSelect('#' + $(this).attr('id'));
                if (result == false) {
                    total++;
                }
            });
        if (total<=0) {
            webApp.error = 0;
            var parent_fieldset = $(this).parents('fieldset');
            var current_active_step = $(this).parents('.f1').find('.f1-step.active');
            var progress_line = $(this).parents('.f1').find('.f1-progress-line');
            $(this).parents('fieldset').fadeOut(400, function () {
                current_active_step.removeClass('active').addClass('activated').next().addClass('active');
                bar_progress(progress_line, 'right');
                $(this).next().fadeIn();
            });
        }
    });
    // previous step
    $('.f1 .btn-previous').on('click', function () {
        var current_active_step = $(this).parents('.f1').find('.f1-step.active');
        var progress_line = $(this).parents('.f1').find('.f1-progress-line');
        $(this).parents('fieldset').fadeOut(400, function () {
            current_active_step.removeClass('active').prev().removeClass('activated').addClass('active');
            bar_progress(progress_line, 'left');
            $(this).prev().fadeIn();
            // scroll window to beginning of the form
            //scroll_to_class( $('.f1'), 20 );
       
        });
    });

});