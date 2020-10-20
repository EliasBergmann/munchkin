using Microsoft.AspNetCore.Components;
using Munchkin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Components
{
    public partial class VueAutreJoueur : ComponentBase
    {
        [Parameter] public Joueur JoueurCible { get; set; }
        [Parameter] public Joueur JoueurRegardant { get; set; }

        private Carte _carteAffichee = null;

        private bool _afficheCarteEnGrand = false;

        private void AfficheCarteEnGrand(Carte carte)
        {
            _carteAffichee = carte;
            _afficheCarteEnGrand = true;
        }
    }
}
