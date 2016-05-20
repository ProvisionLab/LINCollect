cd database
"C:\Program Files\MySQL\MySQL Server 5.7\bin\mysql.exe" "--defaults-file=C:\ProgramData\MySQL\MySQL Server 5.7\my.ini" "-uroot" "-p" "--default-character-set=utf8" "--host=localhost" "--user=root" "--password=00000" < db_bootstrapper.txt
cd ..
.\DynamicSurvey.Bootstrapper\bin\Debug\DynamicSurvey.Bootstrapper.exe connection_string="server=localhost;user id=CodeClient;password=In0Dd~uc!A55;persistsecurityinfo=True;database=dbsurveys;" create_database=True
PAUSE