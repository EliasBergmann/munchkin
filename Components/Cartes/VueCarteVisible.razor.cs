using Microsoft.AspNetCore.Components;
using Munchkin.Data;

namespace Munchkin.Components
{
    public partial class VueCarteVisible : ComponentBase
    {
        [Parameter] public Joueur Joueur { get; set; }
        [Parameter] public Carte Carte { get; set; }

        private bool _afficheCarteEnGrand;
        private bool _envoiCarte;
        private Joueur _joueurSelectionne;
        private Partie _partie => Joueur.Partie;

        private void EnvoyeCarte(Carte carte, Joueur joueurSelectionne)
        {
            _partie.TransfertCarte(Carte, null, joueurSelectionne);
            _envoiCarte = false;
        }
    }
}
