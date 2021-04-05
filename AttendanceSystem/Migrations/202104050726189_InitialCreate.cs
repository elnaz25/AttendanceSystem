namespace AttendanceSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        Attendance_ID = c.Int(nullable: false, identity: true),
                        ComingTime = c.DateTime(nullable: false),
                        DateOfDay = c.DateTime(nullable: false),
                        LeaveTime = c.DateTime(),
                        User_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Attendance_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 30),
                        LastName = c.String(maxLength: 30),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BirthDate = c.DateTime(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(),
                        UserRole = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Attendances");
        }
    }
}
