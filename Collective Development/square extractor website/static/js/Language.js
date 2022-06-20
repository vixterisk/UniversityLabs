<!--                        Language Change Script-->
function lang_switch(lang) {
    switch (lang) {
        case 'English':
            $('[lang]').hide();
            $('[lang="en"]').show();
            break;
        case 'Русский':
            $('[lang]').hide();
            $('[lang="ru"]').show();
            break;
        case '中国人':
            $('[lang]').hide();
            $('[lang="ch"]').show();
            break;
        case 'Español':
            $('[lang]').hide();
            $('[lang="es"]').show();
            break;
        default:
            $('[lang]').hide();
            $('[lang="en"]').show();
    }
};
$(function () {
    // get language that was chosen earlier to set this language as current
    lang = localStorage.getItem("lang");
    // set last chosen language as current
    lang_switch(lang);
    $('#lang-switch').change(function () { // put onchange event when user select option from select
        lang = $(this).val(); // decide which language to display using switch case.
        localStorage.setItem("lang", lang); //Remember language that was selected by user
        lang_switch(lang); // set selected language as current
    });

});