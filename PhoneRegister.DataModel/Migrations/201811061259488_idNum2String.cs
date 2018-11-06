namespace PhoneRegister.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idNum2String : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PhoneRecords", "IdentificationNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PhoneRecords", "IdentificationNumber", c => c.Int(nullable: false));
        }
    }
}
