//Akram Injaileh
using E_Libriray_API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;

namespace E_Libriray_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ManageRecord : ControllerBase
    {
        #region All non query
        //private readonly IConfiguration _configuration;

        //public Controllers(IConfiguration configuration)
        //{

        //    _configuration = configuration;
        //}

        //public enum NonQueryCommandType
        //{
        //    Insert = 0,
        //    Update = 1,
        //    Delete = 2
        //}

        //public string ExecuteSQLCommandNonQuery(string commandText, NonQueryCommandType type)
        //{
        //    SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        //    try
        //    {
        //        SqlCommand sqlCommand = new SqlCommand(commandText, connection);
        //        connection.Open();
        //        sqlCommand.ExecuteNonQuery();
        //        connection.Close();
        //        if (type == NonQueryCommandType.Insert)
        //        {
        //            return "Insert Done Succefully ";
        //        }
        //        else if (type == NonQueryCommandType.Update)
        //        {
        //            return "Update Done Succefully";
        //        }
        //        else
        //        {
        //            return "Delete Done Succefully";

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Something Went Wrong While Proccess Request";
        //    }



        //}
        #endregion
        [Route("[Action]")]
        [HttpPost]
        public IActionResult CreateAuthor(string Name, string Phone, string Email, string Password, string? Bio)
        {
            string query = "INSERT INTO AUTHOR ([Name], Phone, Email, [Password], Bio) VALUES (@Name,@Phone,@Email,@Password,@Bio)";
            string conn = "Server=DESKTOP-VK5UVN3\\SQLEXPRESS;Database=E-Libriray;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True";
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Name", $"{Name}");
            cmd.Parameters.AddWithValue("@Phone", $"{Phone}");
            cmd.Parameters.AddWithValue("@Email", $"{Email}");
            cmd.Parameters.AddWithValue("@Password", $"{Password}");
            cmd.Parameters.AddWithValue("@Bio", $"{Bio}");
            connection.Open();
            int row = cmd.ExecuteNonQuery();
            connection.Close();
            return Ok(row > 0 ? "The Author has been successfully created" : "Failed to create Author");
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult CreateClient(string Name, string Phone, string Email, string Password)
        {
            string query = "INSERT INTO Client ([Name], Phone, Email, [Password]) VALUES (@Name, @Phone, @Email, @Password)";
            string conn = "Server=DESKTOP-VK5UVN3\\SQLEXPRESS;Database=E-Libriray;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True";
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Name", $"{Name}");
            cmd.Parameters.AddWithValue("@Phone", $"{Phone}");
            cmd.Parameters.AddWithValue("@Email", $"{Email}");
            cmd.Parameters.AddWithValue("@Password", $"{Password}");
            connection.Open();
            int row = cmd.ExecuteNonQuery();
            connection.Close();
            return Ok(row > 0 ? "The Client has been successfully created" : "Failed to create Client");
        }

        //[HttpGet]
        //[Route("[action]")]
        //public IActionResult UPDATE_BOOK(CreateBookDTO dto)
        //{
        //    string query = "INSERT INTO Client ([Name], Phone, Email, [Password]) VALUES (@Name, @Phone, @Email, @Password)";
        //    string conn = "Server=DESKTOP-VK5UVN3\\SQLEXPRESS;Database=E-Libriray;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True";
        //    SqlConnection connection = new SqlConnection(conn);
        //    SqlCommand cmd = new SqlCommand(query, connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    return Ok(dto.authorName + "/ " + dto.name + " / "
        //        + dto.description + "" + dto.Price);
        //}
    }
}
