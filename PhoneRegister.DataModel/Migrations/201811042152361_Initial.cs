namespace PhoneRegister.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhoneRecords",
                c => new
                    {
                        PhoneRecordId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        PhoneNumber = c.String(),
                        IdentificationNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PhoneRecordId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PhoneRecords");
        }
    }
}
