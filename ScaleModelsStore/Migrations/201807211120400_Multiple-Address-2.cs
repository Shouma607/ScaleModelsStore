namespace ScaleModelsStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MultipleAddress2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Addresses", "ShortDescription", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Addresses", new[] { "ShortDescription" });
        }
    }
}
