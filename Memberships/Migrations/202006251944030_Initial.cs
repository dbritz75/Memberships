namespace Memberships.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 2048),
                        ItemURL = c.String(maxLength: 1024),
                        ImageURL = c.String(maxLength: 1024),
                        HTML = c.String(),
                        WaitDays = c.Int(nullable: false),
                        ItemTypeID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        SectionID = c.Int(nullable: false),
                        PartID = c.Int(nullable: false),
                        IsFree = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ItemType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Item_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Item", t => t.Item_ID)
                .Index(t => t.Item_ID);
            
            CreateTable(
                "dbo.Part",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Item_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Item", t => t.Item_ID)
                .Index(t => t.Item_ID);
            
            CreateTable(
                "dbo.Section",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Item_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Item", t => t.Item_ID)
                .Index(t => t.Item_ID);
            
            CreateTable(
                "dbo.ProductLinkText",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 2048),
                        ImageURL = c.String(maxLength: 1024),
                        ProductLinkTextID = c.Int(nullable: false),
                        ProductTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Product_Item",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ItemID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductID, t.ItemID });
            
            CreateTable(
                "dbo.ProductType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Subscription",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        RegistrationCode = c.String(maxLength: 20),
                        Description = c.String(maxLength: 2048),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Subscription_Product",
                c => new
                    {
                        SubscriptionID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SubscriptionID, t.ProductID });
            
            CreateTable(
                "dbo.User_Subscription",
                c => new
                    {
                        SubscriptionID = c.Int(nullable: false),
                        UserID = c.String(nullable: false, maxLength: 128),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.SubscriptionID, t.UserID });
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        isActive = c.Boolean(nullable: false),
                        Registered = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Section", "Item_ID", "dbo.Item");
            DropForeignKey("dbo.Part", "Item_ID", "dbo.Item");
            DropForeignKey("dbo.ItemType", "Item_ID", "dbo.Item");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Section", new[] { "Item_ID" });
            DropIndex("dbo.Part", new[] { "Item_ID" });
            DropIndex("dbo.ItemType", new[] { "Item_ID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.User_Subscription");
            DropTable("dbo.Subscription_Product");
            DropTable("dbo.Subscription");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ProductType");
            DropTable("dbo.Product_Item");
            DropTable("dbo.Product");
            DropTable("dbo.ProductLinkText");
            DropTable("dbo.Section");
            DropTable("dbo.Part");
            DropTable("dbo.ItemType");
            DropTable("dbo.Item");
        }
    }
}
