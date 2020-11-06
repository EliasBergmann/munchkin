using Microsoft.AspNetCore.Components;
using Munchkin.Data;

namespace Munchkin.Components
{
    public partial class VueCarteEquipement : ComponentBase
    {
        [Parameter] public Joueur Joueur { get; set; }
        [Parameter] public Carte Carte { get; set; }


        private Partie _partie => Joueur.Partie;
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
