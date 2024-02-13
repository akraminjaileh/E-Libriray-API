//Akram Injaileh

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;


namespace E_Libriray_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class find : ControllerBase
    {
        string conn = "Server=DESKTOP-VK5UVN3\\SQLEXPRESS;Database=E-Libriray;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True";
        [Route ("Books")]
        [HttpGet]
        public IActionResult GetAllBooks(int ? bookId)
        {
            //Do not send any value to fetch all Books
            string query = $"SELECT * FROM BOOK WHERE BookId={bookId}";
            if (bookId == default) { 
            query = "SELECT * FROM BOOK ";
            }
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(query,connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable result = new DataTable();
            adapter.Fill(result);
            return Ok(result);
        }

        [Route("Client")]
        [HttpGet]
        public IActionResult GetAllClient(int ? ClientId)
        {
            //Do not send any value to fetch all Clients
            string query = $"SELECT * FROM Client WHERE ClientId={ClientId}";
            if (ClientId == default)
            {
              query = "SELECT * FROM Client";
            } 
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable result = new DataTable();
            adapter.Fill(result);
            return Ok(result);
        }



    }




}
