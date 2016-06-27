$('#spot').on('click', function() {
    if($('#spot').val() == 1) {
        $('#spot').val(0);
        $('#total').find('span').text("55");
    } else {
        $('#spot').val(1);
    }

    if ($(this).is(':checked')) {
        $camping = $('.camping .hide');
        $camping.removeClass('hide').addClass('display').hide().fadeIn('fast');
    } else {
        $camping = $('.display');
        $camping.hide().removeClass('display').addClass('hide');
    }
});

$('.the-other-one').on('click', function() {
    $(this).addClass('hide');
    $('.camp-pic').removeClass('camp-pic').addClass('hide');
});

$('#toggle').on('click', function() {
    $('#this-one').removeClass('hide').addClass('camp-pic');
    $('#this-one img').removeClass('hide');
});

$('#75').on('click', function() {
    $('#total').find('span').text("75");
});

$('#85').on('click', function() {
    $('#total').find('span').text("85");
});