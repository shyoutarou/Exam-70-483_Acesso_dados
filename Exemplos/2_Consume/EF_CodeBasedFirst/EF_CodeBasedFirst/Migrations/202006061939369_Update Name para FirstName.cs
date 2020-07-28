namespace EF_CodeBasedFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UpdateNameparaFirstName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alunoes", "FirstName", c => c.String());
            Sql("update Alunoes set FirstName = 'Bob'");
            DropColumn("dbo.Alunoes", "Name");
        }

        public override void Down()
        {
            AddColumn("dbo.Alunoes", "Name", c => c.String());
            DropColumn("dbo.Alunoes", "FirstName");
        }
    }
}
