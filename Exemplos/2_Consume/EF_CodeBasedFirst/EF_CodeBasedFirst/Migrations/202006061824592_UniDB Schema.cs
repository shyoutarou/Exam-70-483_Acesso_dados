namespace EF_CodeBasedFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniDBSchema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alunoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        Name = c.String(),
                        EnrollmentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Inscricaos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CursoID = c.Int(nullable: false),
                        AlunoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Alunoes", t => t.AlunoID, cascadeDelete: true)
                .ForeignKey("dbo.Cursoes", t => t.CursoID, cascadeDelete: true)
                .Index(t => t.CursoID)
                .Index(t => t.AlunoID);
            
            CreateTable(
                "dbo.Cursoes",
                c => new
                    {
                        CursoID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.CursoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inscricaos", "CursoID", "dbo.Cursoes");
            DropForeignKey("dbo.Inscricaos", "AlunoID", "dbo.Alunoes");
            DropIndex("dbo.Inscricaos", new[] { "AlunoID" });
            DropIndex("dbo.Inscricaos", new[] { "CursoID" });
            DropTable("dbo.Cursoes");
            DropTable("dbo.Inscricaos");
            DropTable("dbo.Alunoes");
        }
    }
}
