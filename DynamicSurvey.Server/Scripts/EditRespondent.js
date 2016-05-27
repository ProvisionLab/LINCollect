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

    var addRow = function() {
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

    var handleEditQuestionModal = function(questionAction) {
        $("#question").ckeditor();

        var hiddenQuestionFormat = $("#questionFormat").val();
        $(".question-format-container").hide();

        if (questionAction === "Edit") {
            switch (hiddenQuestionFormat) {
            case "Text":
            {
                $("#questionFormatText").prop("checked", true);
                $("#textQuestionFormatContainer").show();
                break;
            }
            case "ChoiceAcross":
            {
                $("#questionFormatChoiceAcross").prop("checked", true);
                $("#choiceQuestionFormatContainer").show();
                $("#includeAnnotationContainer").show();
                $("#allowMultipleValuesContainer").show();
                break;
            }
            case "ChoiceDown":
            {
                $("#questionFormatChoiceDown").prop("checked", true);
                $("#choiceQuestionFormatContainer").show();
                break;
            }
            case "DropDown":
            {
                $("#questionFormatDropDown").prop("checked", true);
                $("#choiceQuestionFormatContainer").show();
                $("#allowMultipleValuesContainer").show();
                break;
            }
            case "Matrix":
            {
                $("#questionFormatMatrix").prop("checked", true);
                $("#matrixQuestionFormatContainer").show();
                break;
            }
            case "Slider":
            {
                $("#questionFormatSlider").prop("checked", true);
                $("#sliderQuestionFormatContainer").show();
                break;
            }
            default:
            }

            $.uniform.update(".format");
        }

        $(".format").change(function() {
            var questionFormat = $(this).val();

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
        });

        $(document).on("click", ".is-default", function() {
            var checked = $(this).is(":checked");

            $(".is-default").prop("checked", false);
            $.uniform.update(".is-default");

            $(this).prop("checked", checked);
            $.uniform.update($(this));
        });

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