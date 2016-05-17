USE DBSurveys;

select 'seed started' as '';
source seed\seed_user_right.sql
source seed\seed_user.sql
source seed\seed_field_type.sql
source seed\seed_user_language.sql
source seed\seed_survey_template.sql