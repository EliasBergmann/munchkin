﻿using Microsoft.AspNetCore.Components;
using Munchkin.Data;
using Munchkin.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Components
{
    public partial class VueCarteEquipement : ComponentBase
    {
        [Inject] MunchkinService munchkinService { get; set; }

        [Parameter] public Joueur Joueur { get ; set;}
        [Parameter] public Carte Carte { get; set; }

        private bool _afficheCarte;
        private bool _envoiCarte;
        private double _opacite = 1;

        private void EnvoyeCarte(Carte carte, Joueur joueur)
        {
            Joueur.EnvoyeCarte(carte, joueur);
            _envoiCarte = false;
        }
    }
}
