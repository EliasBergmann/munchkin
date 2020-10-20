using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Munchkin.Data;
using Munchkin.Service;
using Munchkin.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Munchkin.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject] MunchkinService munchkinService { get; set; }
        [Inject] IJSRuntime jSRuntime { get; set; }

        private Joueur _joueurSelectionne;
        private bool _selectionneJoueur = false;
        private bool _creeNouveauJoueur = false;
        private Joueur _joueur;

        private Dictionary<string, object> InputAttributes { get; set; } = new Dictionary<string, object>()
        {
            { "id", "textinput" },
            { "autocomplete", "off"}
        };


        private void JoueurSelectionne(Joueur joueur)
        {
            _joueurSelectionne = joueur;
            _selectionneJoueur = false;
        }

        private async Task NouvellePartie()
        {
            munchkinService.Recommence();
            await CreeNouveauJoueur();
        }

        private async Task CreeNouveauJoueur()
        {
            _joueur = new Joueur(munchkinService);
            _selectionneJoueur = false;
            _creeNouveauJoueur = true;
            StateHasChanged();

            await Task.Delay(100);
            await JavaScriptLibrary.FocusAsync("textinput", jSRuntime);
        }

        private void ExitClicked()
        {
            _selectionneJoueur = false;
            _joueurSelectionne = null;
        }

        private void CreationNouveauJoueur()
        {
            if(munchkinService.Joueurs.FirstOrDefault(item => item.Nom == _joueur.Nom) == null)
            {
                munchkinService.AjouteUnNouveauJoueur(_joueur);

                _creeNouveauJoueur = false;
                _joueurSelectionne = _joueur;
            }
        }
    }
}
