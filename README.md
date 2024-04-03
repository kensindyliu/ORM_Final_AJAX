prject reply on packegs:<br>
Microsoft.EntityFrameworkCore.tools<br>
Microsoft.EntityFrameworkCore<br>
Microsoft.EntityFrameworkCore.tools.SqlServer<br>
and MVC prject needs <br>
Microsoft.EntityFrameworkCore.Design.<br>


to run this project, you should<br>
1. modify connection string<br>
2. delete all files in Migration folder and the database in SQL<br>
3. ensure default project is: EntityService and  execute this commands:<br>
add-migration SchoolSystem<br>
update-database -verbose<br>
