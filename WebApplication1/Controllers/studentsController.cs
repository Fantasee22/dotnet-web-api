using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.DTO;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class studentsController : ControllerBase
    {
        string connString = "Server=LAPTOP-6TPEADB9;Database=testSamit;Trusted_Connection=True;";
        public studentsController()
        {

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ResListGeneric>> getAllStudents()
        {
            var table = new DataTable();
            using var conn = new SqlConnection(connString);
            using var cmd = new SqlCommand("SELECT * FROM Student", conn);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (!reader.HasRows)
                return NotFound("Data not found");

            table.Load(reader);

            List<Student> listRes = new List<Student>();
            foreach (DataRow row in table.Rows)
            {
                listRes.Add(new Student
                {
                    Id = Convert.ToInt32(row["id"]),
                    Name = row["name"].ToString(),
                    Class = row["class"].ToString()
                });
            }

            ResListGeneric res = new ResListGeneric();
            res.listRes = listRes;

            return Ok(res);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ResListGeneric>> AddStudents(Student student)
        {

            var table = new DataTable();

            using var conn = new SqlConnection(connString);


            await conn.OpenAsync();
            using var transaction = conn.BeginTransaction();
            using var cmd = new SqlCommand
                (
                "INSERT INTO Student(name, class) VALUES ("
                + "'" + student.Name + "',"
                + "'" + student.Class + "'"
                + ")"
                , conn
                , transaction
                );
            try
            {

                await cmd.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                await transaction.RollbackAsync();
            }


            return Ok();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Student>> editStudents(int id, Student student)
        {
            var table = new DataTable();

            using var conn = new SqlConnection(connString);

            await conn.OpenAsync();
            using var transaction = conn.BeginTransaction();
            using var cmd = new SqlCommand
                (
                "UPDATE Student SET "
                + "Name = '" + student.Name + "',"
                + "Class = '" + student.Class + "' "
                + "Where id = " + id.ToString()
                , conn
                , transaction
                );

            try
            {

                await cmd.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                await transaction.RollbackAsync();
            }

            return Ok(new { Id = id });
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Student>> deleteStudent(int id)
        {
            var table = new DataTable();
            using var conn = new SqlConnection(connString);

            await conn.OpenAsync();
            using var transaction = conn.BeginTransaction();
            using var cmd = new SqlCommand("DELETE FROM Student where id = " + id.ToString(), conn, transaction);
            try
            {
                await cmd.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                await transaction.RollbackAsync();
            }

            return Ok(new { Id = id });
        }

        [HttpGet]
        [Route("{id}/transactions")]
        [AllowAnonymous]
        public async Task<ActionResult<ResListGeneric>> getTransaction(int id)
        {
            var table = new DataTable();
            using var conn = new SqlConnection(connString);
            using var cmd = new SqlCommand("SELECT TOP 1 * FROM Transactions WHERE studentId = " + id.ToString() , conn);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (!reader.HasRows)
                return NotFound("Data not found");

            table.Load(reader);

            

            return Ok(new
            {
                Id = table.Rows[0]["id"],
                StudentId = table.Rows[0]["studentId"],
                Type = table.Rows[0]["type"],
                Amount = table.Rows[0]["amount"],
                Date = table.Rows[0]["date"]
            });
        }

        [HttpPost]
        [Route("{id}/transactions")]
        [AllowAnonymous]
        public async Task<ActionResult<ResListGeneric>> addTransaction(TransactionsObj trx)
        {
            var table = new DataTable();
            using var conn = new SqlConnection(connString);
            await conn.OpenAsync();
            using var transaction = conn.BeginTransaction();

            using var cmd = new SqlCommand
                (
                "INSERT INTO Transactions(studentId, type, amount, date) VALUES ("
                + "" + trx.StudentId + ","
                + "'" + trx.Type + "', "
                + "" + trx.Amount + ", "
                + "'" + trx.Date + "'"
                + ")"
                , conn
                , transaction
                );
            try
            {

                await cmd.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                await transaction.RollbackAsync();
            }


            return Ok();
        }
    }
}
