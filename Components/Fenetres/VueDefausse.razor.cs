using Microsoft.AspNetCore.Components;
using Munchkin.Data;
using Munchkin.Service;
using System;
using System.Collections.Generic;

namespace Munchkin.Components
{
    public partial class VueDefausse : ComponentBase
    {
        [Inject] MunchkinService munchkinService { get; set; }

        [Parameter] public Joueur Joueur { get; set; }
        [Parameter] public Type DefausseType { get; set; }


        private Carte _carteAffichee = null;
        private bool _afficheCarteEnGrand = false;

        private void PrendCarteDeDefausseTresor(CarteTresor carte, Joueur joueur)
        {
            munchkinService.PrendCarteDeDefausseTresor(carte, joueur);
            _afficheCarteEnGrand = false;
            StateHasChanged();
        }

        private void PrendCarteDefausseDonjon(CarteDonjon carte, Joueur joueur)
        {
            munchkinService.PrendCarteDefausseDonjon(carte, joueur);
            _afficheCarteEnGrand = false;
            StateHasChanged();
        }

        private void AfficheCarteEnGrand(Carte carte)
        {
            _carteAffichee = carte;
            _afficheCarteEnGrand = true;
        }
    }
}
