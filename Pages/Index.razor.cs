using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Munchkin.Data;
using Munchkin.Service;
using Munchkin.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject] MunchkinService MunchkinService { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }


        private bool _selectionneJoueur = false;
        private bool _selectionnePartie = false;

        private bool _creeNouveauJoueur = false;
        private bool _creeNouvellePartie = false;

        private bool _vueJoueur = false;

        private Partie _partieSelectionne;
        private Joueur _joueurSelectionne;
        private Partie _partieASupprimer;

        private Dictionary<string, object> _nomJoueurInputAttributes { get; set; } = new Dictionary<string, object>()
        {
            { "id", "nomJoueurTextInput" },
            { "autocomplete", "off"}
        };

        private Dictionary<string, object> _nomPartieInputAttributes { get; set; } = new Dictionary<string, object>()
        {
            { "id", "nomPartieTextInput" },
            { "autocomplete", "off"}
        };


        private void SelectionneJoueur(Joueur joueur)
        {
            _joueurSelectionne = joueur;
            _selectionneJoueur = false;

            _vueJoueur = true;
        }

        private void SupprimeJoueur(Joueur joueur)
        {
            _partieSelectionne.SupprimeJoueur(joueur);
        }

        private void SelectionnePartie(Partie partie)
        {
            _partieSelectionne = partie;
            _selectionnePartie = false;
            _selectionneJoueur = true;
        }

        private void DemandeSupprimePartie(Partie partie)
        {
            _partieASupprimer = partie;
        }

        private async Task CreeNouveauJoueur()
        {
            _joueurSelectionne = new Joueur(_partieSelectionne);
            _creeNouveauJoueur = true;
            _selectionneJoueur = false;

            StateHasChanged();
            await Task.Delay(100);
            await JavaScriptLibrary.FocusAsync("nomJoueurTextInput", JSRuntime);
        }

        private async Task CreeNouvellePartie()
        {
            _partieSelectionne = new Partie();
            _selectionnePartie = false;
            _creeNouvellePartie = true;

            StateHasChanged();

            await Task.Delay(100);
            await JavaScriptLibrary.FocusAsync("nomPartieTextInput", JSRuntime);
        }

        private void CreationNouveauJoueur()
        {
            if (!_partieSelectionne.Joueurs.Any(item => item.Nom == _joueurSelectionne.Nom))
            {
                _partieSelectionne.AjouteJoueur(_joueurSelectionne);
                _creeNouveauJoueur = false;

                _vueJoueur = true;
            }
        }

        private async Task CreationNouvellePartie()
        {
            if (!MunchkinService.Parties.Any(item => item.Nom == _partieSelectionne.Nom))
            {
                MunchkinService.AjouteNouvellePartie(_partieSelectionne);
                _creeNouvellePartie = false;
                await CreeNouveauJoueur();
            }
        }

        private void VueJoueur_ExitClicked()
        {

            _vueJoueur = false;

            _selectionneJoueur = false;
            _joueurSelectionne = null;
            _partieSelectionne = null;
        }

        private void SupprimerPartie()
        {
            MunchkinService.SupprimePartie(_partieASupprimer);

            if (!MunchkinService.Parties.Any())
            {
                _selectionnePartie = false;
            }

            _partieASupprimer = null;
        }
    }
}
