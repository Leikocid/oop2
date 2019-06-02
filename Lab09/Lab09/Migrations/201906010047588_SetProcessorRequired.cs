namespace Lab09.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetProcessorRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Computers", "Processor_Id", "dbo.Processors");
            DropIndex("dbo.Computers", new[] { "Processor_Id" });
            AlterColumn("dbo.Computers", "Processor_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Computers", "Processor_Id");
            AddForeignKey("dbo.Computers", "Processor_Id", "dbo.Processors", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Computers", "Processor_Id", "dbo.Processors");
            DropIndex("dbo.Computers", new[] { "Processor_Id" });
            AlterColumn("dbo.Computers", "Processor_Id", c => c.Int());
            CreateIndex("dbo.Computers", "Processor_Id");
            AddForeignKey("dbo.Computers", "Processor_Id", "dbo.Processors", "Id");
        }
    }
}
