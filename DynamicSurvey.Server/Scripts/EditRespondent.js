var EditRespondent = function() {

    var addRow = function() {
        var rowTemplate = $(".answer-choice-row:first").clone(true);
        $(".answer-choice-row:last").after(rowTemplate);
    };

    var handleEditQuestionModal = function() {
        $("#question").ckeditor();

        $("#addRow").click(function() {
            addRow();
        });

        $(".move-row-up").click(function() {
            var row = $(this).closest(".answer-choice-row");
            if (row.is(".answer-choice-row:first")) {
                return;
            }

            var beforeRow = row.prev(".answer-choice-row");
            beforeRow.before(row);
        });

        $(".move-row-down").click(function() {
            var row = $(this).closest(".answer-choice-row");
            if (row.is(".answer-choice-row:last")) {
                return;
            }

            var nextRow = row.next(".answer-choice-row");
            nextRow.after(row);
        });

        $(".delete-row").click(function() {
            $(this).closest(".answer-choice-row").remove();
        });

        $("#editQuestionModal").modal("show");
    };

    var initEditRespondentPage = function() {
        var questionAction = $("#questionAction").val();

        if (questionAction) {
            handleEditQuestionModal();
        }
    };

    return {
        init: function() {
            initEditRespondentPage();
        }
    };
}();