# cd C:/Work/Work/My/DynamicSurveys/database/
# mysql --host=mysql2.gear.host --user=dbsurveys --password=In0Dd~uc!A55
# mysql --user=root --password=00000
# # DROP DATABASE DBSurveys;
# source init.sql

SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = 'DBSurveys';
CREATE DATABASE IF NOT EXISTS DBSurveys;

USE DBSurveys;

source structure/init.sql
# source stored_procedures/init.sql
source seed/init.sql
