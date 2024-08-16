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



        public static List<List<int>> SuperDigito(ModelL.Historial historial)
        {
            int entrada = historial.Numero;

            //int superDigito = CalcularSuperDigito(entrada);

            List<List<int>> list = new List<List<int>>();

            while (entrada >= 10)
            {

                List<int> list1 = SumaDigitos(entrada);

                entrada = list1[list1.Count - 1];

                for (int i = list1.Count - 2; i >= 0; i--)
                {
                    if (i > 0)
                        Console.Write(list1[i] + " + ");
                    else
                        Console.WriteLine(list1[i] + " = " + entrada);
                }
                list.Add(list1);
            }
            return list;
        }

        static List<int> SumaDigitos(int number)
        {
            List<int> list = new List<int>();

            int suma = 0;
            while (number > 0)
            {
                list.Add(number % 10);

                suma += number % 10;
                number /= 10;
            }

            list.Add(suma);

            return list;
        }
    }
}
