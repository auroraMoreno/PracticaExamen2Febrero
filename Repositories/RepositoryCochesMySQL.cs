using PracticaExamen2Febrero.Data;
using PracticaExamen2Febrero.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaExamen2Febrero.Repositories
{
    public class RepositoryCochesMySQL : IRepositoryCoches
    {
        CocheContext context;

        public RepositoryCochesMySQL(CocheContext context)
        {
            this.context = context;
        }

        public Coche BuscarCoche(string modelo)
        {
            throw new NotImplementedException();
        }

        public Coche BuscarCoche(int idcoche)
        {
            throw new NotImplementedException();
        }

        public void EliminarCoche(int idcoche)
        {
            throw new NotImplementedException();
        }

        public List<Coche> GetCoches()
        {
            var consulta = from datos in this.context.Coches
                           select datos;
            return consulta.ToList();
        }


        public void InsertarCoche(string marca, string modelo, string conductor, string imagen)
        {
            throw new NotImplementedException();
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
