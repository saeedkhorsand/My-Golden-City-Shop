namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Trigger : DbMigration
    {
        public override void Up()
        {
            Sql("CREATE TRIGGER [OnCategoryDelete] ON Categories INSTEAD OF DELETE AS" +
                " CREATE TABLE #Table(Id bigint) INSERT INTO #Table (Id) SELECT  Id FROM    deleted DECLARE @c INT SET @c = 0" +
                " WHILE @c <> (SELECT COUNT(Id) FROM #Table) BEGIN   SELECT @c = COUNT(Id) FROM #Table  INSERT INTO #Table (Id) SELECT  Categories.Id" +
                "  FROM    Categories LEFT OUTER JOIN #Table ON Categories.Id = #Table.Id WHERE   ParentId IN (SELECT Id FROM #Table)   AND     #Table.Id IS NULL" +
                " END DELETE From Products Where Products.CategoryId in(select Id From #Table); DELETE  Categories  FROM    Categories INNER JOIN #Table ON Categories.Id = #Table.Id"
                );

            Sql("CREATE TRIGGER [OnCommentDelete] ON Comments INSTEAD OF DELETE AS" +
              " CREATE TABLE #Table( Id  bigint ) INSERT INTO #Table (Id) SELECT  Id FROM    deleted DECLARE @c INT SET @c = 0" +
              " WHILE @c <> (SELECT COUNT(Id) FROM #Table) BEGIN   SELECT @c = COUNT(Id) FROM #Table  INSERT INTO #Table (Id) SELECT  Comments.Id" +
              "  FROM    Comments LEFT OUTER JOIN #Table ON Comments.Id = #Table.Id WHERE   ParentId IN (SELECT Id FROM #Table)   AND     #Table.Id IS NULL" +
              " END  DELETE  Comments  FROM    Comments INNER JOIN #Table ON Comments.Id = #Table.Id"
              );

            Sql("CREATE TRIGGER [OnProductDelete] ON Products INSTEAD OF DELETE as" +
                " Delete From Comments Where ProductId in(select Id from deleted); Delete From Products Where Id in (select Id from Deleted);"
                );
        }

        public override void Down()
        {
            Sql("DROP TRIGGER [OnCategoryDelete]");
            Sql("DROP TRIGGER [OnCommentDelete]");
            Sql("DROP TRIGGER [OnProductDelete]");
        }
    }
}
