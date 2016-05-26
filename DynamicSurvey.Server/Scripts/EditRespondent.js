var EditRespondent = function() {

    var fixRowIndexing = function() {
        $(".answer-choice-row").each(function(rowIndex, rowElement) {
            $(rowElement).find(".text").attr("name", "AnswerChoiceItemViewModels[" + rowIndex + "].Text");
            $(rowElement).find(".coding").attr("name", "AnswerChoiceItemViewModels[" + rowIndex + "].Coding");
            $(rowElement).find(".is-default").attr("name", "AnswerChoiceItemViewModels[" + rowIndex + "].IsDefault");
            $(rowElement).find(".is-default-hidden").attr("name", "AnswerChoiceItemViewModels[" + rowIndex +
                "].IsDefault");
        });
    };

    var addRow = function() {
        var rowTemplate = $(".answer-choice-row:first").clone(true);

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

        if (questionAction === "Add") {
            $(".question-format-container").hide();
        }

        $(".format").change(function() {
            var questionFormat = $(this).val();

            $(".question-format-container").hide();
            $(".allow-multiple-values-container").hide();

            switch (questionFormat) {
            case "Text":
            {
                $("#textQuestionFormatContainer").show();
                break;
            }
            case "ChoiceAcross":
            {
                $("#choiceQuestionFormatContainer").show();
                $(".allow-multiple-values-container").show();
                break;
            }
            case "ChoiceDown":
            {
                $("#choiceQuestionFormatContainer").show();
                $(".allow-multiple-values-container").show();
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