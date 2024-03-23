to run this project, you should
1. modify connection string
2. delete all files in Migration folder and the database in SQL
3. ensure default project is: EntityService and  execute this commands
add-migration SchoolSystem
update-database -verbose
