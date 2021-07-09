using Microsoft.EntityFrameworkCore.Migrations;

namespace curso.api.Migrations
{
    public partial class AlterarDatabaseParaCURSO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_Curso_TB_USUARIO_CodigoUsuario",
                table: "TB_Curso");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_Curso",
                table: "TB_Curso");

            migrationBuilder.RenameTable(
                name: "TB_Curso",
                newName: "TB_CURSO");

            migrationBuilder.RenameIndex(
                name: "IX_TB_Curso_CodigoUsuario",
                table: "TB_CURSO",
                newName: "IX_TB_CURSO_CodigoUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_CURSO",
                table: "TB_CURSO",
                column: "Codigo");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_CURSO_TB_USUARIO_CodigoUsuario",
                table: "TB_CURSO",
                column: "CodigoUsuario",
                principalTable: "TB_USUARIO",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_CURSO_TB_USUARIO_CodigoUsuario",
                table: "TB_CURSO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_CURSO",
                table: "TB_CURSO");

            migrationBuilder.RenameTable(
                name: "TB_CURSO",
                newName: "TB_Curso");

            migrationBuilder.RenameIndex(
                name: "IX_TB_CURSO_CodigoUsuario",
                table: "TB_Curso",
                newName: "IX_TB_Curso_CodigoUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_Curso",
                table: "TB_Curso",
                column: "Codigo");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Curso_TB_USUARIO_CodigoUsuario",
                table: "TB_Curso",
                column: "CodigoUsuario",
                principalTable: "TB_USUARIO",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
