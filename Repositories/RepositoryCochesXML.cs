using PracticaExamen2Febrero.Helpers;
using PracticaExamen2Febrero.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PracticaExamen2Febrero.Repositories
{
    public class RepositoryCochesXML : IRepositoryCoches
    {
        private PathProvider pathProvider;
        private XDocument docxml;
        private String path;

        public RepositoryCochesXML(PathProvider pathProvider)
        {
            this.pathProvider = pathProvider;
            this.path = this.pathProvider.MapPath("coches.xml",Folders.Documents);
            this.docxml = XDocument.Load(this.path);
        }

        public Coche BuscarCoche(string modelo)
        {
            var conulta = from datos in this.docxml.Descendants("coche")
                          where datos.Element("modelo").Value == modelo.ToUpper()
                          select new Coche
                          {
                              IdCoche = int.Parse(datos.Element("idcoche").Value),
                              Marca = datos.Element("marca").Value,
                              Modelo = datos.Element("modelo").Value,
                              Conductor = datos.Element("conductor").Value,
                              Imagen = datos.Element("imagen").Value
                          };
            return conulta.FirstOrDefault();
        }

        public Coche BuscarCoche(int idcoche)
        {
            var consulta = from datos in this.docxml.Descendants("coche")
                           where datos.Element("idcoche").Value == idcoche.ToString()
                           select new Coche
                           {
                               IdCoche = int.Parse(datos.Element("idcoche").Value),
                               Marca = datos.Element("marca").Value,
                               Modelo = datos.Element("modelo").Value,
                               Conductor = datos.Element("conductor").Value,
                               Imagen = datos.Element("imagen").Value
                           };
            return consulta.FirstOrDefault();
        }

        public void EliminarCoche(int idcoche)
        {
            XElement xElement = this.GetXElement(idcoche);
            xElement.Remove();
            this.docxml.Save(this.path);
        }

        public List<Coche> GetCoches()
        {
            var consulta = from datos in this.docxml.Descendants("coche")
                           select new Coche
                           {
                               IdCoche = int.Parse(datos.Element("idcoche").Value),
                               Marca = datos.Element("marca").Value,
                               Modelo = datos.Element("modelo").Value,
                               Conductor = datos.Element("conductor").Value,
                               Imagen = datos.Element("imagen").Value
                           };
            return consulta.ToList();
        }

        private int GetMaxId()
        {
            var consulta = (from datos in this.docxml.Descendants("coche")
                            select int.Parse(datos.Element("idcoche").Value)).Max();
            int id = Convert.ToInt32(consulta) + 1;
            return id;
        }

        public void InsertarCoche(string marca, string modelo, string conductor, string imagen)
        {
            int id = GetMaxId();
            XElement xElement = new XElement("coche");
            xElement.Add(new XElement("idcoche", id));
            xElement.Add(new XElement("marca", marca));
            xElement.Add(new XElement("modelo", modelo));
            xElement.Add(new XElement("conductor", conductor));
            xElement.Add(new XElement("imagen", imagen));
            this.docxml.Element("coches").Add(xElement);
            this.docxml.Save(this.path);
        }

        private XElement GetXElement(int idcoche)
        {
            var consulta = from datos in this.docxml.Descendants("coche")
                           where datos.Element("idcoche").Value == idcoche.ToString()
                           select datos;
            return consulta.FirstOrDefault();
        }


        public void ModificarCoche(int idCoche, string marca, string modelo, string conductor, string imagen)
        {
            XElement xElement = this.GetXElement(idCoche);
            xElement.Element("marca").Value = marca;
            xElement.Element("modelo").Value = modelo;
            xElement.Element("conductor").Value = conductor;
            xElement.Element("imagen").Value = imagen;
            this.docxml.Save(this.path);
        }
    }
}
