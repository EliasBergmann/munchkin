using Microsoft.AspNetCore.Components;
using Munchkin.Data;
using Munchkin.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Components
{
    public partial class VueCarteVisible : ComponentBase
    {
        [Inject] MunchkinService munchkinService { get; set; }
        [Parameter] public Joueur Joueur { get ; set;}
        [Parameter] public Carte Carte { get; set; }

        private bool _afficheCarteEnGrand;
        private bool _envoiCarte;
        private Joueur _joueurSelectionne;

        private void EnvoyeCarte(Carte carte, Joueur joueurSelectionne)
        {
            munchkinService.TransfertCarte(Carte, null, joueurSelectionne);
            _envoiCarte = false;
        }
    }
}
