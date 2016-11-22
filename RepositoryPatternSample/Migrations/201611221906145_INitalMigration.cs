namespace RepositoryPatternSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class INitalMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Age = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Dangerous = c.Boolean(nullable: false),
                        DangerScale = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Animals");
        }
    }
}
