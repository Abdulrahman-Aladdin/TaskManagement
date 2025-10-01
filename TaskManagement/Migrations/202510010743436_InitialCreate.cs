namespace TaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        MemberName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.MemberId);
            
            CreateTable(
                "dbo.TaskAssignments",
                c => new
                    {
                        AssignmentId = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        TaskId = c.Int(nullable: false),
                        CompletedHours = c.Double(nullable: false),
                        RemainingHours = c.Double(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AssignmentId)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .ForeignKey("dbo.AppTasks", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.MemberId)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.AppTasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        TaskName = c.String(nullable: false, maxLength: 150),
                        ExpectedHours = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.TaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskAssignments", "TaskId", "dbo.AppTasks");
            DropForeignKey("dbo.TaskAssignments", "MemberId", "dbo.Members");
            DropIndex("dbo.TaskAssignments", new[] { "TaskId" });
            DropIndex("dbo.TaskAssignments", new[] { "MemberId" });
            DropTable("dbo.AppTasks");
            DropTable("dbo.TaskAssignments");
            DropTable("dbo.Members");
        }
    }
}
