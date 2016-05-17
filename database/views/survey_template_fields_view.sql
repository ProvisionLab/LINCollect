USE DBSurveys;


select 'vw_survey_template_fields' as '';


DROP VIEW IF EXISTS vw_survey_template_fields;

CREATE VIEW vw_survey_template_fields 	as 
SELECT 
	st.id 							as 'TemplateId'
    ,sp.id							as 'PageId'
    ,sf.label 						as 'FieldLabel'
    ,sf.id							as 'FieldId'
	,sf.field_index					as 'FieldIndex'
    ,sfParentGroup.field_index		as 'ParentGroupBoxIndex'
    ,sft.field_type					as 'FieldType'
	,sft.id							as 'FieldTypeId'
FROM survey_field sf 
JOIN survey_page sp 
	ON sp.id = sf.fk_parent_page_id    
JOIN survey_template st 
	ON st.id = sp.fk_survey_template_parent_id
JOIN survey_field_type sft 
	ON sft.id = sf.fk_survey_field_type_id
LEFT JOIN survey_field sfParentGroup
	ON sf.fk_group_id = sfParentGroup.id;