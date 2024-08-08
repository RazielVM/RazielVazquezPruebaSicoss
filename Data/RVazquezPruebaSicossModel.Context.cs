﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class RazielVazquezPruebaSicossEntities : DbContext
    {
        public RazielVazquezPruebaSicossEntities()
            : base("name=RazielVazquezPruebaSicossEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Historial> Historials { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
    
        public virtual int UsuarioAdd(string userName, string password, string nombre, string apellido)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var apellidoParameter = apellido != null ?
                new ObjectParameter("Apellido", apellido) :
                new ObjectParameter("Apellido", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UsuarioAdd", userNameParameter, passwordParameter, nombreParameter, apellidoParameter);
        }
    
        public virtual ObjectResult<Login_Result> Login(string userName, string password)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Login_Result>("Login", userNameParameter, passwordParameter);
        }
    
        public virtual int HistorialAdd(Nullable<int> numero, Nullable<int> resultado, Nullable<int> idUsuario)
        {
            var numeroParameter = numero.HasValue ?
                new ObjectParameter("Numero", numero) :
                new ObjectParameter("Numero", typeof(int));
    
            var resultadoParameter = resultado.HasValue ?
                new ObjectParameter("Resultado", resultado) :
                new ObjectParameter("Resultado", typeof(int));
    
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("HistorialAdd", numeroParameter, resultadoParameter, idUsuarioParameter);
        }
    
        public virtual int HistorialDeleteByUsuario(Nullable<int> idUsuario)
        {
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("HistorialDeleteByUsuario", idUsuarioParameter);
        }
    
        public virtual int HistorialDelete(Nullable<int> idHistorial)
        {
            var idHistorialParameter = idHistorial.HasValue ?
                new ObjectParameter("IdHistorial", idHistorial) :
                new ObjectParameter("IdHistorial", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("HistorialDelete", idHistorialParameter);
        }
    
        public virtual ObjectResult<HistorialGetByUsuario_Result> HistorialGetByUsuario(Nullable<int> idUsuario)
        {
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<HistorialGetByUsuario_Result>("HistorialGetByUsuario", idUsuarioParameter);
        }
    }
}
