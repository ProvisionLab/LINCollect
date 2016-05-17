USE DBSurveys;


select 'vw_survey_template_pages' as '';


DROP VIEW IF EXISTS vw_survey_template_pages;

CREATE VIEW vw_survey_template_pages as 
SELECT 
	st.id 							as 'TemplateId'
    ,sp.id							as 'PageId'
    ,sp.page_index 					as 'PageIndex'
    ,sp.page_title 					as 'PageLabel'
from survey_page sp 
JOIN survey_template st 
	ON st.id = sp.fk_survey_template_parent_id