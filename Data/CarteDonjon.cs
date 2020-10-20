using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin
{
    public class CarteDonjon : Carte
    {
        private readonly int _numeroDeCarte;
        public CarteDonjon(int numeroDeCarte)
        {
            _numeroDeCarte = numeroDeCarte;
        }

        public override string ImageURL => "/Cartes/Donjon/" + "D" + _numeroDeCarte + ".png";
    }
}
