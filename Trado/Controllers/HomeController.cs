using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Trado.Models;

namespace Trado.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Record()
        {
            return View();
        }

        [HttpPost]
        public void inserintoDb(string Name,string Gender,string Hobby)
        {
            string connectionString = _configuration.GetConnectionString("myDb1");
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("insertrecord", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name",Name);
            cmd.Parameters.AddWithValue("@gender",Gender);
            cmd.Parameters.AddWithValue("@hobby",Hobby);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        [HttpGet]
        public JsonResult getRecord()
        {
            string response="";
            string connectionString = _configuration.GetConnectionString("myDb1");
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("getrecord", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            connection.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);            
            connection.Close();
            response=JsonConvert.SerializeObject(dt);
            return Json(response);
        }


    }
}