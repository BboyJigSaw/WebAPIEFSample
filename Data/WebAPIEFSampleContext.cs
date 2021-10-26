using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPIEFSample.Models;

/*
 * Actividad 8: Creando mi propio API (Actividad grupal)
Grupo 1
Aneurys Jose Baez Hernandez
Jose Gabriel Polanco Ramos
Miguel Angel Romero Ureña
*/


namespace WebAPIEFSample.Data
{
    public class WebAPIEFSampleContext : DbContext
    {
        public WebAPIEFSampleContext (DbContextOptions<WebAPIEFSampleContext> options)
            : base(options)
        {
        }

        public DbSet<WebAPIEFSample.Models.Book> Book { get; set; }
    }
}
