using UTS.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Identity.Client;
using System.Transactions;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UTS.Datos
{
    public class LoginUsuario
    {
        //metodo para ver si existe el correo


        public bool existeCorreo(string correo)
        {
            string eCorreo = "";
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ValidarCorreo", conexion);
                cmd.Parameters.AddWithValue("correo", correo);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        eCorreo = dr["correo"].ToString();
                    }
                }
            }
            if (eCorreo != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //creacion del metodo registro
        public bool Registro(UsuarioModel model)
        {
            bool respuesta;
            if (!existeCorreo(model.correo))
            {
                try
                {
                    var cn = new Conexion();
                    using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
                    { conexion.Open();
                        SqlCommand cmd = new SqlCommand("SP_insertar_usuario", conexion);
                        cmd.Parameters.AddWithValue("clave_empleado", model.clave_empleado);
                        cmd.Parameters.AddWithValue("nombre", model.nombre);
                        cmd.Parameters.AddWithValue("apellidos", model.apellidos);
                        cmd.Parameters.AddWithValue("contraseña", model.contraseña);
                       cmd.Parameters.AddWithValue("telefono", model.telefono);
                        cmd.Parameters.AddWithValue("correo", model.correo);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();

                    }
                    respuesta = true;
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    respuesta = false;
                }
            }
            else
            {
                respuesta = false;
            }
            return respuesta;
        }

        

        //metodo validar usuario

        public UsuarioModel ValidarUsuario(string correo, string contraseña)
        {
            UsuarioModel usuario= new UsuarioModel();
            var cn = new Conexion();
            using( var conexion=new SqlConnection(cn.getAulasUTSContext()))
            { 
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", conexion);
                cmd.Parameters.AddWithValue("correo", correo);
                cmd.Parameters.AddWithValue("contraseña", contraseña);
                cmd.CommandType= CommandType.StoredProcedure;
                using(var dr=cmd.ExecuteReader())
                {
                    while (dr.Read()) 
                    {
                        usuario.clave_empleado=Convert.ToInt32(dr["clave_empleado"]);
                        usuario.nombre=dr["nombre"].ToString();
                        usuario.apellidos = dr["apellidos"].ToString();
                        usuario.contraseña = dr["contraseña"].ToString();
                        usuario.telefono = dr["telefono"].ToString();
                        usuario.correo = dr["correo"].ToString();
                        
                    }
                }
            }
            return usuario;
        }

        //crear el metodo de CambiarClave
        public bool CambiarClave(string correo, string contraseña)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_CambiarClave", conexion);
                    cmd.Parameters.AddWithValue("correo", correo);
                    cmd.Parameters.AddWithValue("contraseña", contraseña);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception ex) 
            {
                string error = ex.Message;
                respuesta = false;
            }
            return respuesta;
        }

    }
}
