using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public string CI { get; set; }

        public string Password { get; set; }

        public string Rol { get; set; }




        public Usuario()
        {
            
        }




        public bool Validar()
        {
            

            Regex pass = new Regex(@"^(?=\w*\d)(?=\w*[A-Z])(?=\w*[a-z])\S{6,16}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);


            if (!string.IsNullOrEmpty(CI) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Rol))
            {
                if(this.CI.Length>=7 & this.CI.Length <= 8)
                {
                    if(!this.CI.Contains("#") && !this.Password.Contains("#"))
                    {
                         if (!Regex.Match(this.CI, @"^[0 - 9] *$").Success)
                            {
                            if (pass.IsMatch(this.Password))
                            {
                                
                                
                                    return true;
                            }
                            
                        }
                        
                    }
                   
                }
                
            }
            return false;
        }

    }
}
