USE DBSurveys;

# SURVEY TEMPLATE
# user can create survey_template IF admin
# user can add pages to survey_template IF admin
# user can add sections to survey_template IF admin
# user can add fields to survey_template IF admin
# user can remove fields/pages/sections IF admin



select 'procedures\init_survey_crud started' as '';

DROP PROCEDURE IF EXISTS add_or_edit_survey_field;
DELIMITER // 
CREATE PROCEDURE `add_or_edit_survey_field` (
	
) 
proc_label:BEGIN
	
END//
DELIMITER ;

select 'procedures\init_survey_crud finished' as '';