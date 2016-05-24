#--user=CodeClient --password=In0Dd~uc!A55
DROP USER 'CodeClient'@'localhost';
FLUSH PRIVILEGES;
CREATE USER 'CodeClient'@'localhost' IDENTIFIED BY 'In0Dd~uc!A55';

DROP USER 'CodeClient'@'%';
FLUSH PRIVILEGES;
CREATE USER 'CodeClient'@'%' IDENTIFIED BY 'In0Dd~uc!A55';

GRANT EXECUTE ON DbSurveys.* TO 'CodeClient'@'%';

GRANT SELECT ON DbSurveys.vw_survey_template TO 'CodeClient'@'%';
GRANT SELECT ON DbSurveys.vw_survey_template_pages TO 'CodeClient'@'%';
GRANT SELECT ON DbSurveys.vw_survey_template_fields TO 'CodeClient'@'%';
GRANT SELECT ON DbSurveys.vw_survey_template_field_default_values TO 'CodeClient'@'%';
GRANT SELECT ON DbSurveys.vw_user TO 'CodeClient'@'%';
GRANT SELECT ON DbSurveys.vw_field_type_view TO 'CodeClient'@'%';