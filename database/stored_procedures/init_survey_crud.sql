USE DBSurveys;
select 'procedures\init_survey_crud started' as '';

DROP PROCEDURE IF EXISTS add_or_edit_survey_field;
DELIMITER // 
CREATE PROCEDURE `add_or_edit_survey_field` (
	
) 
proc_label:BEGIN
	
END//
DELIMITER ;

select 'procedures\init_survey_crud finished' as '';