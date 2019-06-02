namespace Lab09.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Computers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                        Logo = c.Binary(),
                        MemorySize = c.Int(nullable: false),
                        VideoCard = c.String(nullable: false),
                        DiskSize = c.String(nullable: false),
                        Processor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Processors", t => t.Processor_Id)
                .Index(t => t.Processor_Id);
            
            CreateTable(
                "dbo.Processors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(nullable: false),
                        Developer = c.String(nullable: false),
                        CoresCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Computers", "Processor_Id", "dbo.Processors");
            DropIndex("dbo.Computers", new[] { "Processor_Id" });
            DropTable("dbo.Processors");
            DropTable("dbo.Computers");
        }
    }
}
