USE DBSurveys;


select 'vw_field_type_view' as '';


DROP VIEW IF EXISTS vw_field_type_view;

CREATE VIEW vw_field_type_view 	as 
SELECT 
	sft.Id 				as 'Id',
    sft.field_type		as 'FieldType'
 from survey_field_type sft;
 