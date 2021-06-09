using System;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Data.SqlClient;
using ImSoftwareApi.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Web.Http.Description;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace ImSoftwareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UsuariosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/Usuarios
        [HttpGet]
        public List<Usuario> GetUsuarios()
        {
            return LoadListFromDB();
        }

        // GET: api/Usuarios/ID
        [HttpGet("{id}")]
        public Usuario GetUsuariosById(int id)
        {
            return LoadListFromDB().Where(e => e.id == id).ToList().FirstOrDefault();
        }

        // POST: api/Usuarios
        [HttpPost]
        [ResponseType(typeof(string))]
        public HttpResponseMessage PostUsuarios(Usuario user)
        {
            string respuesta = InsertUserInDB(user);
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(respuesta);
            return response;
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public string DeleteUsuario(int id)
        {
            int x = DeleteUserFromDB(id);
            if (x > 0)
                return "Usuario eliminado exitosamente";
            else
                return "Error al eliminar el usuario";
        }

        // PUT: api/Usuarioss/ID
        [HttpPut("{id}")]
        public string PutUsuario(int id, Usuario usuario)
        {
            return EditUserFromDB(id, usuario);
        }

        private bool UsuarioExists(int id)
        {
            List<Usuario> userslst = this.LoadListFromDB();
            return userslst.Any(e => e.id== id);
        }

        private List<Usuario> LoadListFromDB()
        {
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
            catch (Exception ex)
            {
                throw new Exception("Error al Consultar Base De Datos", ex);
            }
        }

        private string InsertUserInDB(Usuario user)
        {
            string response = "";
            bool flag = false;
            if (validarNombre(user.Nombre))
            {
                if (validarEmail(user.Email))
                {
                    if(validarEdad(user.Edad))
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                        response = response + "El valor de edad debe de ser numérico\n";
                    }
                }
                else
                {
                    flag = false;
                    response = response + "El formato del Email es incorrecto\n";
                }
            }
            else
            {
                flag = false;
                response = response + "Nombre no debe sobrepasar los 50 carácteres\n";
            }
            if (flag)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ApiConnection")))
                    {
                        SqlCommand command = new SqlCommand($"nuevoUsuario", con);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Nombre", user.Nombre));
                        command.Parameters.Add(new SqlParameter("@Edad", user.Edad));
                        command.Parameters.Add(new SqlParameter("@Email", user.Email));
                        con.Open();
                        int x = (Int32)command.ExecuteNonQuery();
                        con.Close();
                        if (x > 0)
                            response = "Usuario agregado correctamente";
                        else
                            response = "El usuario no ha sido agregado";
                        return response;
                    }
                }
                catch (Exception)
                {
                    response = "Error agregando al usuario";
                    /*-spAgregarEstado
                       CREATE PROC nuevoUsuario(
                        @Nombre as varchar(50),
                        @Edad as int,
                        @Email as varchar(50)
                        )
                        as
                        BEGIN
                            INSERT INTO Usuario VALUES(@Nombre, @Edad, @Email)
                        END

                        EXEC nuevoUsuario 'JAVIER',26,'ing.luis.vazquez.e@outlook.com'
                     */
                    return response;
                }
            }
            return response;
        }

        private int DeleteUserFromDB(int id)
        {
            string query = $"DELETE FROM Usuario WHERE id={id}";
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ApiConnection")))
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.CommandType = CommandType.Text;
                    con.Open();
                    int x = (Int32)command.ExecuteNonQuery();
                    con.Close();
                    return x;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private string EditUserFromDB(int id, Usuario u)
        {
            string response = "";
            bool flag = false;
            if (validarNombre(u.Nombre))
            {
                if (validarEmail(u.Email))
                {
                    if (validarEdad(u.Edad))
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                        response = response + "El valor de edad debe de ser numérico\n";
                    }
                }
                else
                {
                    flag = false;
                    response = response + "El formato del Email es incorrecto\n";
                }
            }
            else
            {
                flag = false;
                response = response + "Nombre no debe sobrepasar los 50 carácteres\n";
            }
            if (flag)
            {
                string query = $"UPDATE Usuario SET Nombre='{u.Nombre}', Edad={u.Edad}, Email='{u.Email}'  WHERE id={id}";
                try
                {
                    using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ApiConnection")))
                    {
                        SqlCommand command = new SqlCommand(query, con);
                        command.CommandType = CommandType.Text;
                        con.Open();
                        int x = (Int32)command.ExecuteNonQuery();
                        con.Close();
                        if (x > 0)
                            response = "Usuario actualizado exitosamente";
                        else
                            response = "El usuario no ha sido actualizado";
                        return response;
                    }
                }
                catch (Exception)
                {
                    response = "Error actualizando al usuario";
                    return response;
                }
            }
            return response;
        }

        private bool validarNombre(string nombre)
        {
            bool flag = false;
            if (nombre.Length < 50)
                flag = true;
            return flag;
        }

        private bool validarEmail(string email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool validarEdad(int edad)
        {
            /* El dato siempre sera verdadero pues estamos usando funcionalidad tipo JSON
             * Si se desea enviar los datos en tipo texto se debe modificar la configuracion del sistema lauchSettings.json
             * Pero eso sería poco óptimo, sin embargo en un ejemplo donde la edad sea un valor de texto tipo string
             * se podría usar la siguiente subrutina
             */
            string _edad = edad.ToString();
            try
            {
                Int32.Parse(_edad);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
