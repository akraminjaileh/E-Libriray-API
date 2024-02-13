using E_Libriray_API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace E_Libriray_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        #region test1
        #region getdata
        [HttpGet]
        [Route("[Action]")]
        public IActionResult NormalTest(double d, float f, char c, bool b, string s)
        {
            return Ok($"{d},{f},{c},{b},{s}");
        }
        [HttpGet]
        [Route("[Action]")]
        public IActionResult NullableTest(double? d, float? f, char? c, bool? b, string? s)
        {
            return Ok($"{d},{f},{c},{b},{s}");
        }
        #endregion
        [HttpGet]
        [Route("[Action]")]
        public IActionResult DefalutTest(double d = 1.5, float f = 1.5f, char c = 'A', bool b = true, string s = "Akram")
        {
            return Ok($"{d},{f},{c},{b},{s}");
        }

        //[HttpGet]
        //[Route("[Action]")]
        //public IActionResult GetRequestdate(DateOnly a, DateOnly? b, DateTime c = (2024, 11, 11))
        //{

        //    return Ok($"{a} - {b} - {c}");
        //}

        [HttpDelete]
        [Route("[action]")]
        public IActionResult deletetest(string name, string aouthrName, string des, float price)
        {
            return Ok(name);
        }
        #endregion


        #region Get Data From Query
        [HttpGet]
        [Route("GetDataFromQuery")]
        public IActionResult GetRequestAnddataFromUrl([FromHeader] string accessKey, [FromQuery] int a, int? b, int c = 5)
        {

            return Ok($"{a} - {b} - {c}");
        }
        [HttpGet]
        [Route("GetStringDataFromQuery")]
        public IActionResult GetRequestAndStringdataFromUrl([FromHeader] string accessKey, string a, string? b, string c = "5")
        {

            return Ok($"{a} - {b} - {c}");
        }
        [HttpGet]
        [Route("GetDateDataFromQuery")]
        public IActionResult GetRequestAndDatedataFromUrl(DateTime a, DateTime c =
            new DateTime())
        {
            if (c.Equals(new DateTime()))
            {
                c = c.AddYears(2018).AddMonths(4).AddDays(16);
                c = c.AddYears(-1).AddMonths(-1).AddDays(-1);
            }
            //c= DateTime.Now;
            /*{b} - {c}*/
            return Ok($"{a} - {c}");
        }
        #endregion

        #region Get Data From Route
        [HttpGet]
        [Route("[action]/{name}/{number}")]
        public IActionResult GetParmsFromRoute([FromRoute] int number, [FromRoute] string name, [FromHeader] string accessKey)
        {
            return Ok(number + " - " + name);
        }
        //[HttpGet]
        //[Route("[action]/{name}/{number}")]
        //public IActionResult GetNullableParmsFromRoute([FromRoute] int? number, [FromRoute] string? name)
        //{
        //    return Ok(number + " - " + name);
        //}
        [HttpGet]
        [Route("[action]/{name}/{number}")]
        public IActionResult GetDefaultParmsFromRoute([FromRoute] int number = 20, [FromRoute] string name = "test")
        {
            return Ok(number + " - " + name);
        }
        [HttpGet]
        [Route("[action]/{number}")]
        public IActionResult GetParmsFromBoth([FromRoute] int number, int number2, [FromHeader] string accessKey)
        {
            return Ok(number + " - " + number2);
        }
        [HttpGet]
        [Route("[action]/{number2}")]
        public IActionResult GetParmsFromBoth2(int number, [FromRoute] int number2, [FromHeader] string accessKey)
        {
            return Ok(number + " - " + number2);
        }
        [HttpGet]
        [Route("[action]/{number2}")]
        public IActionResult GetParmsFromBoth3(int number, int number2, [FromHeader] string accessKey)
        {
            return Ok(number + " - " + number2);
        }
        #endregion

        #region Get Data With Post Request

        [HttpPost]
        [Route("[action]")]
        public IActionResult GetRawData([FromBody] GetBodyDataDTO dto, [FromHeader] string accessKey)
        {
            return Ok(dto.authorName + "/ " + dto.name + " / "
                + dto.description + "" + dto.Price);
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult GetBodyWithPutRequest(GetBodyDataDTO dto, [FromHeader] string accessKey)
        {
            return Ok(dto.name);
        }




        [HttpDelete]
        [Route("[action]/{d}")]
        public IActionResult DeleteBook(int d)

        {
            string query = "DELETE FROM BOOK WHERE BookId=@d";

            SqlConnection connection = new SqlConnection("Server =DESKTOP-VK5UVN3\\SQLEXPRESS;Database=E-Libriray;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True");
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            cmd.Parameters.AddWithValue("@d", d);
            int rows = cmd.ExecuteNonQuery();
            connection.Close();
            return Ok(rows > 0 ? "Delete Book Done" : "Failed Delete  Book");
        }






        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateBook([FromBody] CreateBookDTO dto)
        {
            string query = @"
            UPDATE BOOK 
            SET [Name] = COALESCE(@titel, [Name]), 
                Price = COALESCE(@price, Price), 
                Description = COALESCE(@description, Description) 
            WHERE BOOKID = 5";
            //string query = $"DECLARE @Name VARCHAR(50) DECLARE @Price FLOAT DECLARE @Descriptipon VARCHAR(50)UPDATE BOOK SET[Name] = CASE WHEN @Name IS NOT NULL THEN @Name ELSE[Name] END, Price = CASE WHEN @Price IS NOT NULL THEN @Price ELSE Price END, Descriptipon = CASE WHEN @Descriptipon IS NOT NULL THEN @Descriptipon ELSE Descriptipon END WHERE BOOKID = 0";
            //Setupconnection
            SqlConnection connection = new SqlConnection("Server=DESKTOP-VK5UVN3\\SQLEXPRESS;Database=E-Libriray;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True");
            //setup command
            SqlCommand cmd = new SqlCommand(query, connection);
            //command.Parameters.AddWithValue("@name", dto.Name);
            //command.Parameters.AddWithValue("@price", dto.Price);
            //command.Parameters.AddWithValue("@desc", dto.Description);
            //open connection
            connection.Open();
            //execute query
            cmd.Parameters.AddWithValue("@titel", dto.titel);
            cmd.Parameters.AddWithValue("@price", dto.price);
            cmd.Parameters.AddWithValue("@description", dto.description);

            int rows = cmd.ExecuteNonQuery();
            //close connection
            connection.Close();
            return Ok(rows > 0 ? "Update New Book Done" : "Failed Update New Book");
        }

        #endregion
        #region Excute Query 

        [NonAction]
        public DataTable ExecuteQuery(string query)
        {

            SqlConnection conn = new SqlConnection("Server=DESKTOP-VK5UVN3\\SQLEXPRESS;Database=E-Libriray;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True");
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable datatable = new DataTable();
            dataAdapter.Fill(datatable);
            return datatable;
        }

        #endregion

        #region test execute query
        [HttpGet]
        [Route("[action]")]
        public DataTable GetBooks_viewallbook()
        {
            string query = $"select * from viewallbook";

            SqlConnection conn = new SqlConnection("Server=DESKTOP-VK5UVN3\\SQLEXPRESS;Database=E-Libriray;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True");
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable datatable = new DataTable();
            cmd.CommandType = CommandType.Text;

            dataAdapter.Fill(datatable);
            return datatable;
        }
        #endregion

    }
}

