using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PracticaExamen2Febrero.Data;
using PracticaExamen2Febrero.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaExamen2Febrero.Repositories
{
    #region PROCEDIMIENTOS
    //    ALTER PROCEDURE MAXIDCOCHE
    //    (@MAXID INT OUT)
    //AS
    //SELECT @MAXID=MAX(idcoche) FROM coches
    //GO

    //CREATE PROCEDURE CREARCOCHE
    //(@ID INT, @MARCA NVARCHAR(30), @MODELO NVARCHAR(30), @CONDUCTOR NVARCHAR(30), @IMAGEN NVARCHAR(MAX))
    //AS
    //INSERT INTO coches(idcoche, marca, modelo, conductor, imagen)
    //VALUES(@ID, @MARCA, @MODELO, @CONDUCTOR, @IMAGEN)
    //GO
    #endregion

    public class RepositoryCochesSQL : IRepositoryCoches
    {
        CocheContext context;

        public RepositoryCochesSQL(CocheContext context)
        {
            this.context = context;
        }

        public Coche BuscarCoche(string modelo)
        {
            return this.context.Coches.Where(x=>x.Modelo == modelo).FirstOrDefault();
        }

        public Coche BuscarCoche(int idcoche)
        {
            return this.context.Coches.Where(x => x.IdCoche == idcoche).FirstOrDefault();
        }

        public void EliminarCoche(int idcoche)
        {
            Coche coche = this.BuscarCoche(idcoche);
            this.context.Coches.Remove(coche);
            this.context.SaveChanges();
        }

        public List<Coche> GetCoches()
        {
            var consulta = from datos in this.context.Coches
                           select datos;
            return consulta.ToList();
        }

        private int MaxId()
        {
            //String sql = "MAXIDCOCHE @MAXID out";
            //SqlParameter pamid = new SqlParameter("@MAXID", -1);
            //pamid.Direction = System.Data.ParameterDirection.Output;
            //this.context.Coches.FromSqlRaw(sql, pamid);
            //int id = Convert.ToInt32(pamid.Value); 
            //return id;

            var consulta = (from datos in this.context.Coches select datos.IdCoche).Max();
            int id = Convert.ToInt32(consulta) + 1;
            return id;

        }

        public void InsertarCoche(string marca, string modelo, string conductor, string imagen)
        {
            int id = MaxId();
            String sql = "CREARCOCHE @ID, @MARCA, @MODELO, @CONDUCTOR, @IMAGEN";
            SqlParameter pamid = new SqlParameter("@ID",id);
            SqlParameter pammodelo = new SqlParameter("@MARCA",marca);
            SqlParameter pammarcar = new SqlParameter("@MODELO",modelo);
            SqlParameter pamconductor = new SqlParameter("@CONDUCTOR", conductor);
            SqlParameter pamimg = new SqlParameter("@IMAGEN",imagen);
            this.context.Database.ExecuteSqlRaw(sql,pamid,pammodelo,pammarcar,pamconductor,pamimg);

        }

        public void ModificarCoche(int idCoche, string marca, string modelo, string conductor, string imagen)
        {
            Coche coche = this.BuscarCoche(idCoche);
            coche.Marca = marca;
            coche.Modelo = modelo;
            coche.Conductor = conductor;
            coche.Imagen = imagen;
            this.context.SaveChanges();
        }
    }
}
