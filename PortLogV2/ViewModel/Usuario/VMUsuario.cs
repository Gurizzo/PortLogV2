using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortLogV2.ViewModel.Usuario
{
    public class VMUsuario
    {
            [Display(Name = "Cedula"), Required]
            public string CI { get; set; }

            [Display(Name = "Contraseña"), Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
    }
    
}