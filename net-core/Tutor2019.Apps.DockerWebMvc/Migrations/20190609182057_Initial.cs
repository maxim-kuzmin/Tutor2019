using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tutor2019.Apps.DockerWebMvc.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Category = table.Column<string>(maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Category", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Category-3", "Name-1", 1001.01m },
                    { 73, "Category-8", "Name-73", 1073.73m },
                    { 72, "Category-9", "Name-72", 1072.72m },
                    { 71, "Category-7", "Name-71", 1071.71m },
                    { 70, "Category-6", "Name-70", 1070.7m },
                    { 69, "Category-8", "Name-69", 1069.69m },
                    { 68, "Category-7", "Name-68", 1068.68m },
                    { 67, "Category-5", "Name-67", 1067.67m },
                    { 66, "Category-3", "Name-66", 1066.66m },
                    { 65, "Category-7", "Name-65", 1065.65m },
                    { 64, "Category-4", "Name-64", 1064.64m },
                    { 63, "Category-4", "Name-63", 1063.63m },
                    { 62, "Category-2", "Name-62", 1062.62m },
                    { 61, "Category-6", "Name-61", 1061.61m },
                    { 60, "Category-4", "Name-60", 1060.6m },
                    { 59, "Category-3", "Name-59", 1059.59m },
                    { 58, "Category-9", "Name-58", 1058.58m },
                    { 57, "Category-2", "Name-57", 1057.57m },
                    { 56, "Category-6", "Name-56", 1056.56m },
                    { 55, "Category-6", "Name-55", 1055.55m },
                    { 54, "Category-1", "Name-54", 1054.54m },
                    { 53, "Category-1", "Name-53", 1053.53m },
                    { 74, "Category-2", "Name-74", 1074.74m },
                    { 52, "Category-7", "Name-52", 1052.52m },
                    { 75, "Category-6", "Name-75", 1075.75m },
                    { 77, "Category-5", "Name-77", 1077.77m },
                    { 98, "Category-6", "Name-98", 1098.98m },
                    { 97, "Category-6", "Name-97", 1097.97m },
                    { 96, "Category-1", "Name-96", 1096.96m },
                    { 95, "Category-8", "Name-95", 1095.95m },
                    { 94, "Category-5", "Name-94", 1094.94m },
                    { 93, "Category-8", "Name-93", 1093.93m },
                    { 92, "Category-5", "Name-92", 1092.92m },
                    { 91, "Category-3", "Name-91", 1091.91m },
                    { 90, "Category-8", "Name-90", 1090.9m },
                    { 89, "Category-7", "Name-89", 1089.89m },
                    { 88, "Category-5", "Name-88", 1088.88m },
                    { 87, "Category-5", "Name-87", 1087.87m },
                    { 86, "Category-7", "Name-86", 1086.86m },
                    { 85, "Category-4", "Name-85", 1085.85m },
                    { 84, "Category-6", "Name-84", 1084.84m },
                    { 83, "Category-3", "Name-83", 1083.83m },
                    { 82, "Category-6", "Name-82", 1082.82m },
                    { 81, "Category-1", "Name-81", 1081.81m },
                    { 80, "Category-7", "Name-80", 1080.8m },
                    { 79, "Category-6", "Name-79", 1079.79m },
                    { 78, "Category-7", "Name-78", 1078.78m },
                    { 76, "Category-1", "Name-76", 1076.76m },
                    { 51, "Category-8", "Name-51", 1051.51m },
                    { 50, "Category-5", "Name-50", 1050.5m },
                    { 49, "Category-2", "Name-49", 1049.49m },
                    { 22, "Category-1", "Name-22", 1022.22m },
                    { 21, "Category-1", "Name-21", 1021.21m },
                    { 20, "Category-1", "Name-20", 1020.2m },
                    { 19, "Category-7", "Name-19", 1019.19m },
                    { 18, "Category-4", "Name-18", 1018.18m },
                    { 17, "Category-6", "Name-17", 1017.17m },
                    { 16, "Category-2", "Name-16", 1016.16m },
                    { 15, "Category-9", "Name-15", 1015.15m },
                    { 14, "Category-2", "Name-14", 1014.14m },
                    { 13, "Category-6", "Name-13", 1013.13m },
                    { 12, "Category-3", "Name-12", 1012.12m },
                    { 11, "Category-2", "Name-11", 1011.11m },
                    { 10, "Category-4", "Name-10", 1010.1m },
                    { 9, "Category-9", "Name-9", 1009.09m },
                    { 8, "Category-6", "Name-8", 1008.08m },
                    { 7, "Category-1", "Name-7", 1007.07m },
                    { 6, "Category-4", "Name-6", 1006.06m },
                    { 5, "Category-2", "Name-5", 1005.05m },
                    { 4, "Category-3", "Name-4", 1004.04m },
                    { 3, "Category-3", "Name-3", 1003.03m },
                    { 2, "Category-8", "Name-2", 1002.02m },
                    { 23, "Category-4", "Name-23", 1023.23m },
                    { 24, "Category-8", "Name-24", 1024.24m },
                    { 25, "Category-2", "Name-25", 1025.25m },
                    { 26, "Category-7", "Name-26", 1026.26m },
                    { 48, "Category-4", "Name-48", 1048.48m },
                    { 47, "Category-5", "Name-47", 1047.47m },
                    { 46, "Category-3", "Name-46", 1046.46m },
                    { 45, "Category-6", "Name-45", 1045.45m },
                    { 44, "Category-1", "Name-44", 1044.44m },
                    { 43, "Category-4", "Name-43", 1043.43m },
                    { 42, "Category-3", "Name-42", 1042.42m },
                    { 41, "Category-1", "Name-41", 1041.41m },
                    { 40, "Category-5", "Name-40", 1040.4m },
                    { 39, "Category-5", "Name-39", 1039.39m },
                    { 99, "Category-9", "Name-99", 1099.99m },
                    { 38, "Category-5", "Name-38", 1038.38m },
                    { 36, "Category-9", "Name-36", 1036.36m },
                    { 35, "Category-5", "Name-35", 1035.35m },
                    { 34, "Category-8", "Name-34", 1034.34m },
                    { 33, "Category-7", "Name-33", 1033.33m },
                    { 32, "Category-3", "Name-32", 1032.32m },
                    { 31, "Category-4", "Name-31", 1031.31m },
                    { 30, "Category-7", "Name-30", 1030.3m },
                    { 29, "Category-6", "Name-29", 1029.29m },
                    { 28, "Category-1", "Name-28", 1028.28m },
                    { 27, "Category-6", "Name-27", 1027.27m },
                    { 37, "Category-2", "Name-37", 1037.37m },
                    { 100, "Category-8", "Name-100", 1101m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
