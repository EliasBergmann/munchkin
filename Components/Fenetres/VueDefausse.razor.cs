using Microsoft.AspNetCore.Components;
using Munchkin.Data;
using System;

namespace Munchkin.Components
{
    public partial class VueDefausse : ComponentBase
    {
        [Parameter] public Joueur Joueur { get; set; }
        [Parameter] public Type DefausseType { get; set; }


        private Carte _carteAffichee = null;
        private bool _afficheCarteEnGrand = false;
        private Partie _partie => Joueur.Partie;

        private void PrendCarteDeDefausseTresor(CarteTresor carte, Joueur joueur)
        {
            _partie.PrendCarteDeDefausseTresor(carte, joueur);
            _afficheCarteEnGrand = false;
            StateHasChanged();
        }

        private void PrendCarteDefausseDonjon(CarteDonjon carte, Joueur joueur)
        {
            _partie.PrendCarteDefausseDonjon(carte, joueur);
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
