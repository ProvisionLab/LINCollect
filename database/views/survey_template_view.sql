USE DBSurveys;


select 'views/survey_template_view started' as '';


DROP VIEW IF EXISTS vw_survey_template;

CREATE VIEW vw_survey_template 	as 
SELECT 	st.id					as 'id'
		,st.template_name 		as 'title' 
		,templateLanguage.name	as 'language'
		,templateLanguage.id	as 'languageId'
		,st.created 			as 'date_created'
        ,uCreated.login 		as 'user_created'
		,st.last_modified 		as 'date_modified'
        ,uModified.login		as 'user_modified'
        
FROM 	survey_template st
JOIN 	user uCreated 
	ON 	st.user_created_id = uCreated.id
JOIN 	user uModified 
	ON 	st.user_modified_id = uModified.id
JOIN	user_language templateLanguage 
	ON	st.language_id = templateLanguage.id;