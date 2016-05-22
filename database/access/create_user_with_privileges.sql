DROP USER 'CodeClient'@'localhost';
FLUSH PRIVILEGES;
CREATE USER 'CodeClient'@'localhost' IDENTIFIED BY 'In0Dd~uc!A55';

DROP USER 'CodeClient'@'%';
FLUSH PRIVILEGES;
CREATE USER 'CodeClient'@'%' IDENTIFIED BY 'In0Dd~uc!A55';

GRANT ALL PRIVILEGES ON DbSurveys.* TO 'CodeClient'@'%';
FLUSH PRIVILEGES;