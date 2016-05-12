USE DBSurveys;


select 'views/survey_template_view started' as '';


DROP VIEW IF EXISTS survey_template_view;

CREATE VIEW survey_template_view as 
Select * from user;

select '-----test-----';
select * from survey_template_view;
select 'views/survey_template_view finished' as '';