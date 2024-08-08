using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Usuario
    {
        public static Dictionary<string, object> Add(ModelL.Usuario usuario)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>
            {
                { "Resultado", false },
                { "Excepcion", "" }
            };

            try
            {
                using(Data.RazielVazquezPruebaSicossEntities context = new Data.RazielVazquezPruebaSicossEntities())
                {
                    int usuarioRegistrado = context.UsuarioAdd(
                        usuario.UserName,
                        usuario.Password,
                        usuario.Nombre,
                        usuario.Apellido);

                    if(usuarioRegistrado > 0 )
                    {
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }
            }
            catch(Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }
            return diccionario;
        }

        public static Dictionary<string, object> Login(string userName, string password)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>
            {
                { "Resultado", false },
                { "Excepcion", "" },
                { "Usuario", new ModelL.Usuario() }
            };

            try
            {
                using (Data.RazielVazquezPruebaSicossEntities context = new Data.RazielVazquezPruebaSicossEntities())
                {
                    var user = context.Login(userName, password).SingleOrDefault();

                    if (user != null)
                    {
                        ModelL.Usuario usuario = new ModelL.Usuario();

                        usuario.IdUsuario = user.IdUsuario;
                        usuario.UserName = user.UserName;

                        diccionario["Resultado"] = true;
                        diccionario["Usuario"] = usuario;
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

        public static Dictionary<string, object> GetUserName(string userName)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>
            {
                { "Resultado", false },
                { "Excepcion", "" }
            };

            try
            {
                using (Data.RazielVazquezPruebaSicossEntities context = new Data.RazielVazquezPruebaSicossEntities())
                {
                    var user = context.UsuarioUserName(userName).FirstOrDefault();

                    if (user != null)
                    {
                        diccionario["Resultado"] = true;
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



        public static int SuperDigito(ModelL.Historial historial)
        {
            int entrada = historial.Numero;

            int superDigito = CalcularSuperDigito(entrada);

            return superDigito;
        }
        static int CalcularSuperDigito(long number)
        {
            while (number >= 10)
            {
                number = SumaDigitos(number);
            }
            return (int)number;
        }

        static long SumaDigitos(long number)
        {
            long suma = 0;
            while (number > 0)
            {
                suma += number % 10;
                number /= 10;
            }
            return suma;
        }
    }
}
