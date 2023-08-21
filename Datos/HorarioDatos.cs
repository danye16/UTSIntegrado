using UTS.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Identity.Client;
using System.Transactions;
using System.Drawing.Imaging;

namespace UTS.Datos
{
    public class HorarioDatos
    {
        public List<horario_agendaModel> Listar()
        {
            var oLista = new List<horario_agendaModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Sp_Listar", conexion);

                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new horario_agendaModel()
                        {
                            idhorario = Convert.ToInt32(dr["idhorario"]),
                            idaula2 = Convert.ToInt32(dr["idaula2"]),
                            apartado = Convert.ToBoolean(dr["apartado"]),
                            dia = dr["dia"].ToString(),
                            mes = dr["mes"].ToString(),
                            years = dr["years"].ToString(),
                            hora = dr["hora"].ToString()

                        });
                    }
                }
            }
            return oLista;

        }
        public horario_agendaModel ConsultarHorario(int idhorario)
        {
            var oHorario = new horario_agendaModel();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Consultar", conexion);
                cmd.Parameters.AddWithValue("idhorario", idhorario);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oHorario.idhorario = Convert.ToInt32(dr["idhorario"]);
                        oHorario.idaula2 = Convert.ToInt32(dr["idaula2"]);
                        oHorario.apartado = Convert.ToBoolean(dr["apartado"]);
                        oHorario.dia = dr["dia"].ToString();
                        oHorario.mes = dr["mes"].ToString();
                        oHorario.years = dr["years"].ToString();
                        oHorario.hora = dr["hora"].ToString();
                    }
                }
            }
            return oHorario;
        }

        public bool InsertarApartado(horario_agendaModel model)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("Sp_Insertar", conexion);
                    cmd.Parameters.AddWithValue("idaula2", model.idaula2);
                    cmd.Parameters.AddWithValue("apartado", model.apartado);
                    cmd.Parameters.AddWithValue("dia", model.dia);
                    cmd.Parameters.AddWithValue("mes", model.mes);
                    cmd.Parameters.AddWithValue("years", model.years);
                    cmd.Parameters.AddWithValue("hora", model.hora);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception  e) {
                string error = e.Message;
                respuesta = false;
            }

            return respuesta;

        }

        public bool EditarContacto(horario_agendaModel model)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("Sp_Editar", conexion);
                    cmd.Parameters.AddWithValue("idhorario", model.idhorario);
                    cmd.Parameters.AddWithValue("idaula2", model.idaula2);
                    cmd.Parameters.AddWithValue("apartado", model.apartado);
                    cmd.Parameters.AddWithValue("dia", model.dia);
                    cmd.Parameters.AddWithValue("mes", model.mes);
                    cmd.Parameters.AddWithValue("years", model.years);
                    cmd.Parameters.AddWithValue("hora", model.hora);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }

            return respuesta;

        }

        public bool EliminarContacto(int idhorario) 
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("Sp_Eliminar", conexion);
                    cmd.Parameters.AddWithValue("idhorario", idhorario);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch(Exception e) 
            {
                string error = e.Message;
                respuesta = false;
            }
            return respuesta;
        }

    }

}

