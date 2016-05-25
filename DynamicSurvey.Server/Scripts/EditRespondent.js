var EditRespondent = function() {

    var initEditRespondentPage = function() {
        var questionAction = $("#questionAction").val();

        if (questionAction) {
            $("#editQuestionModal").modal("show");
        }
    };

    return {
        init: function() {
            initEditRespondentPage();
        }
    };
}();