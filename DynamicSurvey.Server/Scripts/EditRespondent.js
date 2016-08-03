var EditRespondent = function() {

    var fixRowIndexing = function() {
        $(".answer-choice-row").each(function(rowIndex, rowElement) {
            $(rowElement).find(".choice-id").attr("name", "AnswerChoiceItemViewModels[" + rowIndex + "].Id");
            $(rowElement).find(".text").attr("name", "AnswerChoiceItemViewModels[" + rowIndex + "].Text");
            $(rowElement).find(".coding").attr("name", "AnswerChoiceItemViewModels[" + rowIndex + "].Coding");
            $(rowElement).find(".is-default").attr("name", "AnswerChoiceItemViewModels[" + rowIndex + "].IsDefault");
            $(rowElement).find(".is-default-hidden").attr("name", "AnswerChoiceItemViewModels[" + rowIndex +
                "].IsDefault");
        });
    };

    var addRow = function () {
        var rowTemplate = $(".answer-choice-row:first").clone(true);

        $(".choice-id", rowTemplate).val("");
        $(".text", rowTemplate).val("");
        $(".coding", rowTemplate).val("");

        var isDefaultNameAttribute = (".is-default", rowTemplate).attr("name");

        $(".checkbox-inline", rowTemplate).empty();
        $(".checkbox-inline", rowTemplate).append("<input name=\"AnswerChoiceItemViewModels[" + isDefaultNameAttribute +
            "].IsDefault\" type=\"checkbox\" value=\"true\" class=\"is-default\" />");
        $(".checkbox-inline", rowTemplate).append("<input name=\"AnswerChoiceItemViewModels[" + isDefaultNameAttribute +
            "].IsDefault\" type=\"hidden\" value=\"false\" class=\"is-default-hidden\" />");
        $(".answer-choice-row:last").after(rowTemplate);

        $(".is-default", rowTemplate).uniform();
    };


    var addRow1 = function () {
        var rowTemplate = $(".answer-choice-row-1:first").clone(true);

        $(".choice-id", rowTemplate).val("");
        $(".text", rowTemplate).val("");
        $(".coding", rowTemplate).val("");

        var isDefaultNameAttribute = (".is-default", rowTemplate).attr("name");

        $(".checkbox-inline", rowTemplate).empty();
        $(".checkbox-inline", rowTemplate).append("<input name=\"AnswerChoiceItemViewModels[" + isDefaultNameAttribute +
            "].IsDefault\" type=\"hidden\" value=\"true\" class=\"is-default-hidden\" />");
        $(".checkbox-inline", rowTemplate).append("<input name=\"AnswerChoiceItemViewModels[" + isDefaultNameAttribute +
            "].IsDefault\" type=\"hidden\" value=\"false\" class=\"is-default-hidden\" />");
        $(".answer-choice-row-1:last").after(rowTemplate);

        $(".is-default", rowTemplate).uniform();
    };

    var addRow2 = function () {
        var rowTemplate = $(".answer-choice-row-2:first").clone(true);

        $(".choice-id", rowTemplate).val("");
        $(".text", rowTemplate).val("");
        $(".coding", rowTemplate).val("");

        var isDefaultNameAttribute = (".is-default", rowTemplate).attr("name");

        $(".checkbox-inline", rowTemplate).empty();
        $(".checkbox-inline", rowTemplate).append("<input name=\"AnswerChoiceItemViewModels[" + isDefaultNameAttribute +
            "].IsDefault\" type=\"hidden\" value=\"true\" class=\"is-default-hidden\" />");
        $(".checkbox-inline", rowTemplate).append("<input name=\"AnswerChoiceItemViewModels[" + isDefaultNameAttribute +
            "].IsDefault\" type=\"hidden\" value=\"false\" class=\"is-default-hidden\" />");
        $(".answer-choice-row-2:last").after(rowTemplate);

        $(".is-default", rowTemplate).uniform();
    };

    var showControlsForQuestionFormat = function(questionFormat) {
        $(".question-format-container").hide();
        $("#includeAnnotationContainer").hide();
        $("#allowMultipleValuesContainer").hide();

        switch (questionFormat) {
        case "Text":
        {
            $("#textQuestionFormatContainer").show();
            break;
        }
        case "ChoiceAcross":
        {
            $("#choiceQuestionFormatContainer").show();
            $("#includeAnnotationContainer").show();
            $("#allowMultipleValuesContainer").show();
            break;
        }
        case "ChoiceDown":
        {
            $("#choiceQuestionFormatContainer").show();
            $("#allowMultipleValuesContainer").show();
            break;
        }
        case "DropDown":
        {
            $("#choiceQuestionFormatContainer").show();
            break;
        }
        case "Matrix":
        {
            $("#matrixQuestionFormatContainer").show();
            break;
        }
        case "Slider":
        {
            $("#sliderQuestionFormatContainer").show();
            break;
        }
        default:
        }
    };

    var handleEditQuestionModal = function(questionAction) {
        $("#question").ckeditor();

        var hiddenQuestionFormat = $("#questionFormat").val();
        $(".question-format-container").hide();

        if (questionAction === "Edit") {
            $(".format[value=" + hiddenQuestionFormat + "]").prop("checked", true);
            $.uniform.update(".format");

            showControlsForQuestionFormat(hiddenQuestionFormat);
        }

        $(".format").change(function() {
            var questionFormat = $(this).val();
            showControlsForQuestionFormat(questionFormat);
        });

        $(document).on("click", ".is-default", function() {
            var checked = $(this).is(":checked");

            $(".is-default").prop("checked", false);
            $.uniform.update(".is-default");

            $(this).prop("checked", checked);
            $.uniform.update($(this));
        });

        $("#addRow").click(function () {
            addRow();
        });

        $("#addRow1").click(function () {
            addRow1();
        });

        $("#addRow2").click(function () {
            addRow2();
        });

        $(".move-row-up").click(function() {
            var row = $(this).closest(".answer-choice-row");
            if (row.is(".answer-choice-row:first")) {
                return;
            }

            var beforeRow = row.prev(".answer-choice-row");
            beforeRow.before(row);
        });

        $(".move-row-up-1").click(function () {
            var row = $(this).closest(".answer-choice-row-1");
            if (row.is(".answer-choice-row-1:first")) {
                return;
            }

            var beforeRow = row.prev(".answer-choice-row-1");
            beforeRow.before(row);
        });

        $(".move-row-up-2").click(function () {
            var row = $(this).closest(".answer-choice-row-2");
            if (row.is(".answer-choice-row-2:first")) {
                return;
            }

            var beforeRow = row.prev(".answer-choice-row-2");
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

        $(".move-row-down-1").click(function () {
            var row = $(this).closest(".answer-choice-row-1");
            if (row.is(".answer-choice-row-1:last")) {
                return;
            }

            var nextRow = row.next(".answer-choice-row-1");
            nextRow.after(row);
        });

        $(".move-row-down-2").click(function () {
            var row = $(this).closest(".answer-choice-row-2");
            if (row.is(".answer-choice-row-2:last")) {
                return;
            }

            var nextRow = row.next(".answer-choice-row-2");
            nextRow.after(row);
        });

        $(".delete-row").click(function() {
            $(this).closest(".answer-choice-row").remove();
        });

        $(".delete-row-1").click(function () {
            $(this).closest(".answer-choice-row-1").remove();
        });

        $(".delete-row-2").click(function () {
            $(this).closest(".answer-choice-row-2").remove();
        });

        $("#editQuestionForm").submit(function() {
            fixRowIndexing();
        });

        $("#editQuestionModal").modal("show");
    };

    var initEditRespondentPage = function() {
        var questionAction = $("#questionAction").val();

        if (questionAction) {
            handleEditQuestionModal(questionAction);
        }
    };

    return {
        init: function() {
            initEditRespondentPage();
        }
    };
}();