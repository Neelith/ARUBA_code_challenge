using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class LogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string createLogTableIfNotExists = @"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[logs]') AND type in (N'U'))
                CREATE TABLE [dbo].[logs] (

                   [Id] int IDENTITY(1,1) NOT NULL,
                   [Message] nvarchar(max) NULL,
                   [MessageTemplate] nvarchar(max) NULL,
                   [Level] nvarchar(128) NULL,
                   [TimeStamp] datetime NOT NULL,
                   [Exception] nvarchar(max) NULL,
                   [Properties] nvarchar(max) NULL

                   CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED ([Id] ASC)
                );";
            migrationBuilder.Sql(createLogTableIfNotExists);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string deleteLogTableIfExists = @"
                IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[logs]') AND type in (N'U'))
                DROP TABLE [dbo].[logs]";
            migrationBuilder.Sql(deleteLogTableIfExists);
        }
    }
}
