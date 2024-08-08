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
            ModelL.Historial historial = new ModelL.Historial();
            Dictionary<string, object> diccionario = new Dictionary<string, object>
            {
                { "Resultado", false },
                { "Excepcion", "" },
                { "Usuario", historial }
            };

            try
            {
                using (Data.RazielVazquezPruebaSicossEntities context = new Data.RazielVazquezPruebaSicossEntities())
                {
                    var user = context.Login(userName, password).SingleOrDefault();

                    if (user != null)
                    {
                        ModelL.Usuario usuario = new ModelL.Usuario();

                        historial.Usuario = new ModelL.Usuario();
                        historial.Usuario.IdUsuario = user.IdUsuario;
                        historial.Usuario.UserName = user.UserName;

                        //usuario.IdUsuario = user.IdUsuario;
                        //usuario.UserName = user.UserName;

                        diccionario["Resultado"] = true;
                        diccionario["Usuario"] = historial;
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
