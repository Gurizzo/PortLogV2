﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    [Table("Producto")]
    public class Producto
    {


        [Key]
        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public decimal Peso { get; set; }

        public Cliente Cliente { get; set; }



        public List<Importacion> Importaciones { get; set; }


        public Producto()
        {

        }


    }
}
