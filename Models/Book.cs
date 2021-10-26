﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIEFSample.Models.Interfaces;

/*
 * Actividad 8: Creando mi propio API (Actividad grupal)
Grupo 1
Aneurys Jose Baez Hernandez
Jose Gabriel Polanco Ramos
Miguel Angel Romero Ureña
*/


namespace WebAPIEFSample.Models
{
    public class Book : IBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }

       
    }
}
