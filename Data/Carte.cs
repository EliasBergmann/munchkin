using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin
{
    public abstract class Carte
    {
        public string Titre { get; set; }
        public abstract string ImageURL { get; }
        public bool Activee { get; set; } = true;
        public bool EstVisible { get; set; } = false;
    }
}
