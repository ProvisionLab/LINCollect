var EditSurvey = function() {

    var initEditSurveyPage = function() {
        $("#surveyIntroduction").ckeditor();
        $("#surveyThankYouText").ckeditor();
        $("#surveyLandingPageText").ckeditor();
    };

    return {
        init: function() {
            initEditSurveyPage();
        }
    };
}();