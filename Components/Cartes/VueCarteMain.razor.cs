﻿using Microsoft.AspNetCore.Components;
using Munchkin.Data;

namespace Munchkin.Components
{
    public partial class VueCarteMain : ComponentBase
    {
        [Parameter] public Joueur Joueur { get; set; }
        [Parameter] public Carte Carte { get; set; }

        private Joueur _joueurSelectionne;
        private bool _afficheCarte;
        private bool _envoiCarte;
        private Partie _partie => Joueur.Partie;


        private void EnvoyeCarte(Carte carte, Joueur joueur)
        {
            Joueur.EnvoyeCarte(carte, joueur);
            _envoiCarte = false;
        }
    }
}
