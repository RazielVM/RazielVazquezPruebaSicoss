using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Business
{
    public class Historial
    {
        public static Dictionary<string, object> Add(ModelL.Historial historial)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> 
            {
                { "Excepcion", "" },
                { "Resultado", false }
            };

            try
            {
                using(Data.RazielVazquezPruebaSicossEntities context = new Data.RazielVazquezPruebaSicossEntities())
                {
                    int historialRegistrado = context.HistorialAdd(historial.Numero, historial.Resultado, historial.Usuario.IdUsuario);

                    if (historialRegistrado > 0 )
                    {
                        diccionario["Resultado"] = true;
                    }
                }
            }
            catch (Exception e)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = e.Message;
            }
            return diccionario;
        }

        public static Dictionary<string, object> DeleteByUsuario(int idUsuario)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>
            {
                { "Excepcion", "" },
                { "Resultado", false }
            };

            try
            {
                using (Data.RazielVazquezPruebaSicossEntities context = new Data.RazielVazquezPruebaSicossEntities())
                {
                    int historialRegistrado = context.HistorialDeleteByUsuario(idUsuario);

                    if (historialRegistrado > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                }
            }
            catch (Exception e)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = e.Message;
            }
            return diccionario;
        }

        public static Dictionary<string, object> GetByUsuario(int idUsuario)
        {
            ModelL.Historial historial = new ModelL.Historial();
            Dictionary<string, object> diccionario = new Dictionary<string, object>
            {
                { "Excepcion", "" },
                { "Resultado", false },
                { "Historial", historial }
            };

            try
            {
                using (Data.RazielVazquezPruebaSicossEntities context = new Data.RazielVazquezPruebaSicossEntities())
                {
                    var registros = context.HistorialGetByUsuario(idUsuario);

                    if ( registros != null)
                    {
                        historial.Historiales = new List<ModelL.Historial>();

                        foreach(var registro in registros)
                        {
                            ModelL.Historial hist = new ModelL.Historial();

                            hist.IdHistorial = registro.IdHistorial;
                            hist.Numero = registro.Numero;
                            hist.Resultado = registro.Resultado;
                            hist.Fecha = registro.Fecha.Value;

                            historial.Historiales.Add(hist);
                        }

                        diccionario["Resultado"] = true;
                        diccionario["Historial"] = historial;
                    }
                }
            }
            catch (Exception e)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = e.Message;
            }
            return diccionario;
        }

        public static Dictionary<string, object> GetNumero(ModelL.Historial historial)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>
            {
                { "Resultado", false },
                { "Excepcion", "" },
                { "Historial", historial }
            };

            try
            {
                using (Data.RazielVazquezPruebaSicossEntities context = new Data.RazielVazquezPruebaSicossEntities())
                {
                    var registro = context.Historials
                        .Where(h => h.IdUsuario == historial.Usuario.IdUsuario && h.Numero == historial.Numero)
                        .FirstOrDefault();

                    if (registro != null)
                    {
                        registro.Fecha = DateTime.Now;

                        context.SaveChanges();

                        diccionario["Historial"] = registro;
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }

            return diccionario;
        }

        public static Dictionary<string, object> Delete(int idHistorial)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>
            {
                { "Excepcion", "" },
                { "Resultado", false }
            };

            try
            {
                using (Data.RazielVazquezPruebaSicossEntities context = new Data.RazielVazquezPruebaSicossEntities())
                {
                    int historialRegistrado = context.HistorialDelete(idHistorial);

                    if (historialRegistrado > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                }
            }
            catch (Exception e)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = e.Message;
            }
            return diccionario;
        }

    }
}
