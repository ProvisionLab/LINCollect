USE DBSurveys;


select 'views/survey_template_page_details_view started' as '';


DROP VIEW IF EXISTS vw_survey_template_page_details;

CREATE VIEW vw_survey_template_page_details 	as 
SELECT 
	st.id 				as 'TemplateId'
    ,sp.id				as 'PageId'
    ,sp.page_index 		as 'PageIndex'
    ,sp.page_title 		as 'PageLabel'
    ,sf.label 			as 'FieldLabel'
    ,sf.id				as 'FieldId'
    ,sf.fk_group_id		as 'FieldGroupId'
    ,sft.field_type		as 'FieldType'
FROM survey_field sf 
JOIN survey_page sp 
	ON sp.id = sf.fk_parent_page_id    
JOIN survey_template st 
	ON st.id = sp.fk_survey_template_parent_id
JOIN survey_field_type sft 
	ON sft.id = sf.fk_survey_field_type_id;