using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin
{
    public class CarteTresor : Carte
    {
        private readonly int _numeroDeCarte;
        public CarteTresor(int numeroDeCarte)
        {
            _numeroDeCarte = numeroDeCarte;
        }

        public override string ImageURL => "/Cartes/Tresor/" + "T" + _numeroDeCarte + ".png";
    }
}
