DROP DATABASE IF EXISTS DBSurveys;

SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = 'DBSurveys';
CREATE DATABASE IF NOT EXISTS DBSurveys;

USE DBSurveys;

source structure/init.sql
source seed/init.sql