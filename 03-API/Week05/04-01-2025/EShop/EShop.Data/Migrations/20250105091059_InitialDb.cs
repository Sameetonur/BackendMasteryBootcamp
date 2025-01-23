using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateofBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ProductCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "91d8a274-5d3d-46d6-b45e-bf8e2fc57315", null, "Normal Kullanıcı rolü", "user", "USER" },
                    { "aee93803-cc4e-4214-a5c4-39aa6262d8d9", null, "Yönetici rolü", "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "City", "ConcurrencyStamp", "DateofBirth", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a8ef0d00-9e4f-4b83-a6ae-0e9e20d23b2c", 0, "Ataşehir", "İstanbul", "354bacb2-0dd1-4fd8-b038-fe57b7f87bf4", new DateTime(1995, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "adminuser@gmail.com", true, "Ali", 3, "Cabbar", false, null, "ADMINUSER@GMAİL.COM", "ADMINUSER@GMAİL.COM", "AQAAAAIAAYagAAAAEHO4RFj0K8JbqfxQ/HDLXiH0F1/8x9QUBd9ctOoJ3dt5/1gwNQzMCYG2ypYK6wOe8A==", null, false, "2b40df94-9a53-4b87-9edd-ba147dc1d3ff", false, "adminuser@gmail.com" },
                    { "d488e437-44a3-4d9e-8e94-30b9e2562068", 0, "Kadıköy", "İstanbul", "7159cfae-86b1-4740-8f2d-489f3c8bae47", new DateTime(1995, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "normalnuser@gmail.com", true, "Esin", 2, "Çelik", false, null, "NORMALUSER@GMAİL.COM", "NORMALUSER@GMAİL.COM", "AQAAAAIAAYagAAAAEFL0n7jHn4025bDIyFuHODiej2rOmG/8CZIJIHlfprcQ01NstYRLfps8de4zmLB7uA==", null, false, "274e9bfd-99aa-4126-b991-bcece5e33097", false, "normalnuser@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreateDate", "Description", "ImageUrl", "IsActive", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3078), "Bilgisayarlar, telefonlar ve diğer elektronik ürünler.", "/images/categories/elektronik.jpg", true, false, "Elektronik", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3081) },
                    { 2, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3084), "Kadın, erkek ve çocuk giyim ürünleri.", "/images/categories/moda.jpg", true, false, "Moda", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3085) },
                    { 3, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3086), "Ev dekorasyonu ve yaşam alanları için ürünler.", "/images/categories/ev-ve-yasam.jpg", true, false, "Ev & Yaşam", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3086) },
                    { 4, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3088), "Outdoor ve spor yaparken kullanabileceğiniz ekipmanlar.", "/images/categories/spor-outdoor.jpg", true, false, "Spor & Outdoor", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3088) },
                    { 5, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3089), "Araba aksesuarları ve yedek parçalar.", "/images/categories/otomotiv.jpg", false, false, "Otomotiv", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3090) },
                    { 6, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3091), "Farklı kategorilerde kitaplar.", "/images/categories/kitaplar.jpg", true, false, "Kitaplar", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3091) },
                    { 7, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3092), "Sağlık ve güzellik ürünleri.", "/images/categories/saglik-kozmetik.jpg", true, false, "Sağlık & Kozmetik", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3093) },
                    { 8, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3094), "Yiyecek ve içecek ürünleri.", "/images/categories/gida.jpg", true, false, "Gıda", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3094) },
                    { 9, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3095), "Hobi, oyun ve eğlence ürünleri.", "/images/categories/hobi-eglence.jpg", false, false, "Hobi & Eğlence", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3095) },
                    { 10, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3097), "Buzdolapları, çamaşır makineleri ve diğer büyük ev aletleri.", "/images/categories/beyaz-esya.jpg", true, true, "Beyaz Eşya", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3097) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreateDate", "ImageUrl", "IsActive", "IsDeleted", "Name", "Price", "Properties", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3754), "/images/products/laptop.jpg", true, false, "Laptop", 1500.00m, "16GB RAM, 512GB SSD", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3755) },
                    { 2, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3759), "/images/products/smartphone.jpg", true, false, "Smartphone", 800.00m, "128GB Storage, 6GB RAM", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3759) },
                    { 3, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3761), "/images/products/tshirt.jpg", true, false, "T-Shirt", 20.00m, "100% Cotton, Size M", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3761) },
                    { 4, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3762), "/images/products/running_shoes.jpg", true, false, "Running Shoes", 60.00m, "Size 42, Black", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3762) },
                    { 5, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3763), "/images/products/refrigerator.jpg", true, false, "Refrigerator", 500.00m, "300L, Energy Class A++", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3764) },
                    { 6, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3765), "/images/products/novel_book.jpg", true, false, "Novel Book", 15.00m, "Fiction, 300 pages", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3765) },
                    { 7, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3767), "/images/products/face_cream.jpg", true, false, "Face Cream", 25.00m, "50ml, Anti-aging", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3767) },
                    { 8, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3769), "/images/products/organic_apple.jpg", true, false, "Organic Apple", 3.00m, "1kg, Fresh", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3769) },
                    { 9, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3770), "/images/products/guitar.jpg", true, false, "Guitar", 120.00m, "Acoustic, 6 strings", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3770) },
                    { 10, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3771), "/images/products/car_tire.jpg", true, false, "Car Tire", 70.00m, "195/65 R15", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3772) },
                    { 11, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3773), "/images/products/smartwatch.jpg", true, false, "Smartwatch", 200.00m, "Heart Rate Monitor, GPS", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3773) },
                    { 12, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3774), "/images/products/tablet.jpg", true, false, "Tablet", 300.00m, "10.1 inch, 64GB", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3775) },
                    { 13, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3776), "/images/products/headphones.jpg", true, false, "Headphones", 150.00m, "Noise Cancelling", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3776) },
                    { 14, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3777), "/images/products/blender.jpg", true, false, "Blender", 50.00m, "500W, 1.5L", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3777) },
                    { 15, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3779), "/images/products/microwave.jpg", true, false, "Microwave", 100.00m, "800W, 20L", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3779) },
                    { 16, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3781), "/images/products/camera.jpg", true, false, "Camera", 700.00m, "24MP, 4K Video", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3782) },
                    { 17, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3784), "/images/products/watch.jpg", true, false, "Watch", 80.00m, "Analog, Water Resistant", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3784) },
                    { 18, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3785), "/images/products/backpack.jpg", true, false, "Backpack", 40.00m, "30L, Waterproof", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3785) },
                    { 19, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3787), "/images/products/desk_lamp.jpg", true, false, "Desk Lamp", 25.00m, "LED, Adjustable", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3787) },
                    { 20, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3789), "/images/products/electric_kettle.jpg", true, false, "Electric Kettle", 30.00m, "1.7L, 2200W", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3789) },
                    { 21, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3791), "/images/products/gaming_chair.jpg", true, false, "Gaming Chair", 200.00m, "Ergonomic, Adjustable", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3791) },
                    { 22, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3794), "/images/products/sunglasses.jpg", true, false, "Sunglasses", 50.00m, "UV Protection", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3794) },
                    { 23, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3796), "/images/products/sneakers.jpg", true, false, "Sneakers", 70.00m, "Size 43, White", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3796) },
                    { 24, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3798), "/images/products/coffee_maker.jpg", true, false, "Coffee Maker", 80.00m, "12 Cups, Programmable", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3799) },
                    { 25, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3800), "/images/products/vacuum_cleaner.jpg", true, false, "Vacuum Cleaner", 150.00m, "Bagless, 2000W", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3800) },
                    { 26, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3802), "/images/products/air_conditioner.jpg", true, false, "Air Conditioner", 600.00m, "12000 BTU, Inverter", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3802) },
                    { 27, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3805), "/images/products/electric_toothbrush.jpg", true, false, "Electric Toothbrush", 40.00m, "Rechargeable, 3 Modes", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3805) },
                    { 28, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3806), "/images/products/hair_dryer.jpg", true, false, "Hair Dryer", 30.00m, "2000W, Ionic", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3806) },
                    { 29, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3807), "/images/products/smart_tv.jpg", true, false, "Smart TV", 700.00m, "55 inch, 4K UHD", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3808) },
                    { 30, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3809), "/images/products/gaming_console.jpg", true, false, "Gaming Console", 500.00m, "1TB, 4K HDR", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3809) },
                    { 31, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3811), "/images/products/wireless_mouse.jpg", true, false, "Wireless Mouse", 25.00m, "Ergonomic, 1600 DPI", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3811) },
                    { 32, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3812), "/images/products/keyboard.jpg", true, false, "Keyboard", 80.00m, "Mechanical, RGB", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3812) },
                    { 33, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3814), "/images/products/monitor.jpg", true, false, "Monitor", 300.00m, "27 inch, 144Hz", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3814) },
                    { 34, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3816), "/images/products/printer.jpg", true, false, "Printer", 150.00m, "All-in-One, Wireless", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3816) },
                    { 35, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3817), "/images/products/router.jpg", true, false, "Router", 100.00m, "Dual Band, Gigabit", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3817) },
                    { 36, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3818), "/images/products/external_hard_drive.jpg", true, false, "External Hard Drive", 80.00m, "2TB, USB 3.0", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3819) },
                    { 37, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3820), "/images/products/flash_drive.jpg", true, false, "Flash Drive", 20.00m, "64GB, USB 3.0", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3820) },
                    { 38, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3821), "/images/products/power_bank.jpg", true, false, "Power Bank", 30.00m, "10000mAh, Fast Charging", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3821) },
                    { 39, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3823), "/images/products/wireless_charger.jpg", true, false, "Wireless Charger", 25.00m, "10W, Fast Charging", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3823) },
                    { 40, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3824), "/images/products/smart_light_bulb.jpg", true, false, "Smart Light Bulb", 20.00m, "RGB, WiFi", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3824) },
                    { 41, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3826), "/images/products/security_camera.jpg", true, false, "Security Camera", 60.00m, "1080p, Night Vision", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3826) },
                    { 42, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3827), "/images/products/fitness_tracker.jpg", true, false, "Fitness Tracker", 50.00m, "Heart Rate Monitor, GPS", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3828) },
                    { 43, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3829), "/images/products/electric_scooter.jpg", true, false, "Electric Scooter", 400.00m, "25km/h, 30km Range", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3829) },
                    { 44, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3830), "/images/products/drone.jpg", true, false, "Drone", 500.00m, "4K Camera, GPS", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3831) },
                    { 45, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3832), "/images/products/action_camera.jpg", true, false, "Action Camera", 150.00m, "4K, Waterproof", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3832) },
                    { 46, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3833), "/images/products/electric_shaver.jpg", true, false, "Electric Shaver", 60.00m, "Rechargeable, Wet & Dry", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3833) },
                    { 47, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3866), "/images/products/hair_straightener.jpg", true, false, "Hair Straightener", 40.00m, "Ceramic, 200°C", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3866) },
                    { 48, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3867), "/images/products/electric_grill.jpg", true, false, "Electric Grill", 70.00m, "2000W, Non-Stick", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3868) },
                    { 49, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3869), "/images/products/rice_cooker.jpg", true, false, "Rice Cooker", 50.00m, "1.8L, Non-Stick", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3869) },
                    { 50, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3870), "/images/products/air_fryer.jpg", true, false, "Air Fryer", 100.00m, "3.5L, Digital", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3871) },
                    { 51, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3873), "/images/products/electric_blanket.jpg", true, false, "Electric Blanket", 40.00m, "150x200cm, 3 Heat Settings", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3873) },
                    { 52, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3874), "/images/products/water_filter.jpg", true, false, "Water Filter", 30.00m, "10L, 5-Stage", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3874) },
                    { 53, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3876), "/images/products/electric_heater.jpg", true, false, "Electric Heater", 50.00m, "2000W, Adjustable Thermostat", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3876) },
                    { 54, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3877), "/images/products/dehumidifier.jpg", true, false, "Dehumidifier", 150.00m, "20L, Digital", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3877) },
                    { 55, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3878), "/images/products/humidifier.jpg", true, false, "Humidifier", 40.00m, "5L, Ultrasonic", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3879) },
                    { 56, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3880), "/images/products/electric_fan.jpg", true, false, "Electric Fan", 30.00m, "16 inch, Oscillating", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3880) },
                    { 57, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3881), "/images/products/electric_iron.jpg", true, false, "Electric Iron", 40.00m, "2400W, Steam", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3881) },
                    { 58, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3883), "/images/products/sewing_machine.jpg", true, false, "Sewing Machine", 100.00m, "Portable, 12 Stitches", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3883) },
                    { 59, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3884), "/images/products/electric_screwdriver.jpg", true, false, "Electric Screwdriver", 30.00m, "Rechargeable, 3.6V", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3884) },
                    { 60, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3885), "/images/products/cordless_drill.jpg", true, false, "Cordless Drill", 80.00m, "18V, 2 Batteries", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3885) },
                    { 61, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3888), "/images/products/tool_set.jpg", true, false, "Tool Set", 50.00m, "100 Pieces, Chrome Vanadium", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3888) },
                    { 62, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3889), "/images/products/lawn_mower.jpg", true, false, "Lawn Mower", 150.00m, "Electric, 1600W", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3889) },
                    { 63, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3891), "/images/products/garden_hose.jpg", true, false, "Garden Hose", 40.00m, "30m, Expandable", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3891) },
                    { 64, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3892), "/images/products/bbq_grill.jpg", true, false, "BBQ Grill", 70.00m, "Charcoal, Portable", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3892) },
                    { 65, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3893), "/images/products/tent.jpg", true, false, "Tent", 100.00m, "4 Person, Waterproof", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3894) },
                    { 66, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3895), "/images/products/sleeping_bag.jpg", true, false, "Sleeping Bag", 50.00m, "3 Season, Mummy", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3895) },
                    { 67, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3897), "/images/products/camping_stove.jpg", true, false, "Camping Stove", 30.00m, "Portable, Gas", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3897) },
                    { 68, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3898), "/images/products/hiking_backpack.jpg", true, false, "Hiking Backpack", 60.00m, "50L, Waterproof", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3899) },
                    { 69, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3900), "/images/products/binoculars.jpg", true, false, "Binoculars", 80.00m, "10x50, Waterproof", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3900) },
                    { 70, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3901), "/images/products/fishing_rod.jpg", true, false, "Fishing Rod", 40.00m, "Carbon Fiber, 2.1m", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3901) },
                    { 71, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3902), "/images/products/yoga_mat.jpg", true, false, "Yoga Mat", 20.00m, "6mm, Non-Slip", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3903) },
                    { 72, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3904), "/images/products/dumbbell_set.jpg", true, false, "Dumbbell Set", 50.00m, "20kg, Adjustable", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3904) },
                    { 73, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3905), "/images/products/treadmill.jpg", true, false, "Treadmill", 500.00m, "Folding, 2.5HP", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3905) },
                    { 74, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3906), "/images/products/exercise_bike.jpg", true, false, "Exercise Bike", 200.00m, "Magnetic, 8 Resistance Levels", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3907) },
                    { 75, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3908), "/images/products/rowing_machine.jpg", true, false, "Rowing Machine", 300.00m, "Magnetic, Foldable", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3908) },
                    { 76, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3909), "/images/products/elliptical_trainer.jpg", true, false, "Elliptical Trainer", 400.00m, "Magnetic, 8 Resistance Levels", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3909) },
                    { 77, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3911), "/images/products/weight_bench.jpg", true, false, "Weight Bench", 100.00m, "Adjustable, Foldable", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3911) },
                    { 78, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3912), "/images/products/pull_up_bar.jpg", true, false, "Pull-Up Bar", 30.00m, "Doorway, Adjustable", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3912) },
                    { 79, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3913), "/images/products/resistance_bands.jpg", true, false, "Resistance Bands", 20.00m, "Set of 5, Different Resistance Levels", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3913) },
                    { 80, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3915), "/images/products/jump_rope.jpg", true, false, "Jump Rope", 10.00m, "Adjustable, Speed", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3915) },
                    { 81, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3916), "/images/products/basketball.jpg", true, false, "Basketball", 25.00m, "Size 7, Indoor/Outdoor", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3916) },
                    { 82, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3917), "/images/products/soccer_ball.jpg", true, false, "Soccer Ball", 20.00m, "Size 5, Synthetic Leather", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3918) },
                    { 83, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3920), "/images/products/tennis_racket.jpg", true, false, "Tennis Racket", 50.00m, "Graphite, Lightweight", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3920) },
                    { 84, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3921), "/images/products/badminton_set.jpg", true, false, "Badminton Set", 30.00m, "2 Rackets, 3 Shuttlecocks", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3921) },
                    { 85, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3922), "/images/products/golf_clubs.jpg", true, false, "Golf Clubs", 500.00m, "Set of 12, Graphite", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3922) },
                    { 86, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3924), "/images/products/skateboard.jpg", true, false, "Skateboard", 40.00m, "31 inch, Maple", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3924) },
                    { 87, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3925), "/images/products/roller_skates.jpg", true, false, "Roller Skates", 60.00m, "Adjustable, Size 38-42", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3925) },
                    { 88, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3926), "/images/products/helmet.jpg", true, false, "Helmet", 30.00m, "Bike, Size M", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3927) },
                    { 89, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3928), "/images/products/knee_pads.jpg", true, false, "Knee Pads", 20.00m, "Set of 2, Adjustable", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3928) },
                    { 90, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3929), "/images/products/elbow_pads.jpg", true, false, "Elbow Pads", 20.00m, "Set of 2, Adjustable", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3929) },
                    { 91, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3931), "/images/products/wrist_guards.jpg", true, false, "Wrist Guards", 20.00m, "Set of 2, Adjustable", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3931) },
                    { 92, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3932), "/images/products/bike_lock.jpg", true, false, "Bike Lock", 25.00m, "Combination, Heavy Duty", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3932) },
                    { 93, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3933), "/images/products/bike_pump.jpg", true, false, "Bike Pump", 20.00m, "Portable, High Pressure", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3934) },
                    { 94, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3935), "/images/products/bike_light.jpg", true, false, "Bike Light", 30.00m, "Front and Rear, USB Rechargeable", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3935) },
                    { 95, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3936), "/images/products/bike_bell.jpg", true, false, "Bike Bell", 10.00m, "Loud, Easy to Install", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3936) },
                    { 96, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3938), "/images/products/bike_basket.jpg", true, false, "Bike Basket", 40.00m, "Front, Wicker", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3938) },
                    { 97, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3939), "/images/products/bike_rack.jpg", true, false, "Bike Rack", 50.00m, "Rear, Adjustable", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3939) },
                    { 98, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3940), "/images/products/bike_seat.jpg", true, false, "Bike Seat", 30.00m, "Comfort, Gel", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3941) },
                    { 99, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3942), "/images/products/bike_gloves.jpg", true, false, "Bike Gloves", 20.00m, "Padded, Size L", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3942) },
                    { 100, new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3943), "/images/products/bike_shorts.jpg", true, false, "Bike Shorts", 40.00m, "Padded, Size M", new DateTime(2025, 1, 5, 9, 10, 58, 815, DateTimeKind.Utc).AddTicks(3943) }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "aee93803-cc4e-4214-a5c4-39aa6262d8d9", "a8ef0d00-9e4f-4b83-a6ae-0e9e20d23b2c" },
                    { "91d8a274-5d3d-46d6-b45e-bf8e2fc57315", "d488e437-44a3-4d9e-8e94-30b9e2562068" }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "ApplicationUserId", "CreateDate", "IsActive", "IsDeleted", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "d488e437-44a3-4d9e-8e94-30b9e2562068", new DateTime(2025, 1, 5, 9, 10, 58, 950, DateTimeKind.Utc).AddTicks(6515), true, false, new DateTime(2025, 1, 5, 9, 10, 58, 950, DateTimeKind.Utc).AddTicks(6520) },
                    { 2, "a8ef0d00-9e4f-4b83-a6ae-0e9e20d23b2c", new DateTime(2025, 1, 5, 9, 10, 58, 950, DateTimeKind.Utc).AddTicks(6522), true, false, new DateTime(2025, 1, 5, 9, 10, 58, 950, DateTimeKind.Utc).AddTicks(6523) }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 4, 4 },
                    { 10, 5 },
                    { 6, 6 },
                    { 7, 7 },
                    { 8, 8 },
                    { 9, 9 },
                    { 5, 10 },
                    { 1, 11 },
                    { 1, 12 },
                    { 1, 13 },
                    { 10, 14 },
                    { 10, 15 },
                    { 1, 16 },
                    { 1, 17 },
                    { 4, 18 },
                    { 3, 19 },
                    { 10, 20 },
                    { 4, 21 },
                    { 2, 22 },
                    { 2, 23 },
                    { 10, 24 },
                    { 10, 25 },
                    { 10, 26 },
                    { 7, 27 },
                    { 7, 28 },
                    { 1, 29 },
                    { 1, 30 },
                    { 1, 31 },
                    { 1, 32 },
                    { 1, 33 },
                    { 1, 34 },
                    { 1, 35 },
                    { 1, 36 },
                    { 1, 37 },
                    { 1, 38 },
                    { 1, 39 },
                    { 1, 40 },
                    { 1, 41 },
                    { 4, 42 },
                    { 4, 43 },
                    { 4, 44 },
                    { 4, 45 },
                    { 7, 46 },
                    { 7, 47 },
                    { 10, 48 },
                    { 10, 49 },
                    { 10, 50 },
                    { 10, 51 },
                    { 10, 52 },
                    { 10, 53 },
                    { 10, 54 },
                    { 10, 55 },
                    { 10, 56 },
                    { 10, 57 },
                    { 10, 58 },
                    { 10, 59 },
                    { 10, 60 },
                    { 10, 61 },
                    { 10, 62 },
                    { 10, 63 },
                    { 10, 64 },
                    { 4, 65 },
                    { 4, 66 },
                    { 4, 67 },
                    { 4, 68 },
                    { 4, 69 },
                    { 4, 70 },
                    { 4, 71 },
                    { 4, 72 },
                    { 4, 73 },
                    { 4, 74 },
                    { 4, 75 },
                    { 4, 76 },
                    { 4, 77 },
                    { 4, 78 },
                    { 4, 79 },
                    { 4, 80 },
                    { 4, 81 },
                    { 4, 82 },
                    { 4, 83 },
                    { 4, 84 },
                    { 4, 85 },
                    { 4, 86 },
                    { 4, 87 },
                    { 4, 88 },
                    { 4, 89 },
                    { 4, 90 },
                    { 4, 91 },
                    { 4, 92 },
                    { 4, 93 },
                    { 4, 94 },
                    { 4, 95 },
                    { 4, 96 },
                    { 4, 97 },
                    { 4, 98 },
                    { 4, 99 },
                    { 4, 100 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ApplicationUserId",
                table: "Carts",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ApplicationUserId",
                table: "Orders",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CategoryId",
                table: "ProductCategories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
