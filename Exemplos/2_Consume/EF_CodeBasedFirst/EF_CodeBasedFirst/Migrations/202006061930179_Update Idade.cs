namespace EF_CodeBasedFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIdade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alunoes", "Idade", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Alunoes", "Idade");
        }
    }
}
