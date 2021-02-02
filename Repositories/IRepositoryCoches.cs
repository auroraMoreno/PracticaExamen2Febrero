using PracticaExamen2Febrero.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaExamen2Febrero.Repositories
{
    public interface IRepositoryCoches
    {
        List<Coche> GetCoches();
        Coche BuscarCoche(String modelo);
        Coche BuscarCoche(int idcoche);
        void InsertarCoche(String marca, String modelo, String conductor, String imagen);
        void ModificarCoche(int idCoche,String marca, String modelo, String conductor, String imagen);
        void EliminarCoche(int idcoche);
       
    }
}
