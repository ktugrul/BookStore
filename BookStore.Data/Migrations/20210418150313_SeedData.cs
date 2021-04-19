using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Authors (Name) Values ('Robert C. Martin')");
            migrationBuilder.Sql("INSERT INTO Authors (Name) Values ('Kent Beck')");
            migrationBuilder.Sql("INSERT INTO Authors (Name) Values ('Martin Fowler')");
            migrationBuilder.Sql("INSERT INTO Authors (Name) Values ('Sam Newman')");



            migrationBuilder.Sql("INSERT INTO Books (Name, AuthorId) Values ('The Clean Coder', (SELECT Id FROM Authors WHERE Name = 'Robert C. Martin'))");
            migrationBuilder.Sql("INSERT INTO Books (Name, AuthorId) Values ('Clean Code', (SELECT Id FROM Authors WHERE Name = 'Robert C. Martin'))");
            migrationBuilder.Sql("INSERT INTO Books (Name, AuthorId) Values ('Clean Architecture', (SELECT Id FROM Authors WHERE Name = 'Robert C. Martin'))");

            migrationBuilder.Sql("INSERT INTO Books (Name, AuthorId) Values ('Test-Driven Development', (SELECT Id FROM Authors WHERE Name = 'Kent Beck'))");
            migrationBuilder.Sql("INSERT INTO Books (Name, AuthorId) Values ('Extreme Programming Explained', (SELECT Id FROM Authors WHERE Name = 'Kent Beck'))");
            migrationBuilder.Sql("INSERT INTO Books (Name, AuthorId) Values ('Manifesto for Agile Software Development', (SELECT Id FROM Authors WHERE Name = 'Kent Beck'))");

            migrationBuilder.Sql("INSERT INTO Books (Name, AuthorId) Values ('Refactoring', (SELECT Id FROM Authors WHERE Name = 'Martin Fowler'))");
            migrationBuilder.Sql("INSERT INTO Books (Name, AuthorId) Values ('Patterns of Enterprise Application Architecture', (SELECT Id FROM Authors WHERE Name = 'Martin Fowler'))");
            migrationBuilder.Sql("INSERT INTO Books (Name, AuthorId) Values ('UML Distilled', (SELECT Id FROM Authors WHERE Name = 'Martin Fowler'))");

            migrationBuilder.Sql("INSERT INTO Books (Name, AuthorId) Values ('Monolith to Microservices', (SELECT Id FROM Authors WHERE Name = 'Sam Newman'))");
            migrationBuilder.Sql("INSERT INTO Books (Name, AuthorId) Values ('Building Microservices', (SELECT Id FROM Authors WHERE Name = 'Sam Newman'))");
            migrationBuilder.Sql("INSERT INTO Books (Name, AuthorId) Values ('Lightweight Systems for Realtime Monitoring', (SELECT Id FROM Authors WHERE Name = 'Sam Newman'))");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Books");
            migrationBuilder.Sql("DELETE FROM Authors");
        }
    }
}
