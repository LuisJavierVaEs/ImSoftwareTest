using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace ImSoftwareApiSencilla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UsuariosController(IConfiguration configuration){
            _configuration = configuration;
        }

        [HttpPost]//---------------------------------------------------------------------------------
        public void PostUsuarios(string nombre, string edad, string email){
            if (validarNombre(nombre) && validarEdad(edad) && validarEmail(email)){
                try{
                    using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                    {
                        SqlCommand command = new SqlCommand($"nuevoUsuario", con);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Nombre", nombre));
                        command.Parameters.Add(new SqlParameter("@Edad", edad));
                        command.Parameters.Add(new SqlParameter("@Email", email));
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                    }
                }catch (Exception) { }
            }
        }

        [HttpGet]//--------------------------------------------------------------------------------
        public List<Usuarios> GetUsuarios(){
            List<Usuario> lstUsuarios = new List<Usuario>();
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ApiConnection")))
                {
                    SqlCommand comando = new SqlCommand("SELECT * FROM Usuario", con);
                    comando.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        lstUsuarios.Add(new Usuario(Convert.ToInt32(reader["id"]),
                                                    Convert.ToString(reader["Nombre"]),
                                                    Convert.ToInt32(reader["Edad"]),
                                                    Convert.ToString(reader["Email"])
                                                    ));
                    }
                    con.Close();
                    return lstUsuarios.ToList();
                }
            }
            catch (Exception ex){}
        }

        //------------VALIDACIONES-----------------------------------------------------------------
        private bool validarEmail(string email){
            string expresionRegular = "^[a-zA-Z0-9._]+@[a-zA-Z0-9]\\.[a-zA-Z]$";
            if (Regex.IsMatch(email, expresionRegular))
                return true;
            else
                return false;
        }

        private bool validarEdad(string edad){
            try{
                Int32.Parse(edad);
                return true;
            }catch{ return false; }
        }

        private bool validarNombre(string nombre){
            if (nombre.Length < 50)
                return true;
            else
                return false;
        }
    }
}