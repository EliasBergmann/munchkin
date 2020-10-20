using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Munchkin.Data;
using Munchkin.Service;
using Munchkin.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Components
{
    public partial class VueJoueur : ComponentBase
    {

        [Inject] MunchkinService munchkinService { get; set; }
        [Inject] IJSRuntime jSRuntime { get; set; }

        [Parameter]
        public Joueur Joueur
        {
            get => _joueur;
            set
            {
                if (_joueur != null)
                    _joueur.JeuAChange -= Joueur_JeuAChange;

                _joueur = value;
                _joueur.JeuAChange += Joueur_JeuAChange;
            }
        }

        [Parameter] public EventCallback ExitClicked { get; set; }

        private Joueur _joueur;
        private Joueur _joueurRegarde = null;

        private static bool _afficheDe = false;
        private bool _showHand = true;
        private bool _afficheAutreJoueur = false;
        private bool _affichePioche = false;
        private bool _afficheDefausse = false;

        private Type _selectedDefausseType = null;
        private bool _selectionneTypeDefausse = false;

        private bool _demandePioche = false;
        private bool _demandeDefausse = false;

        protected override void OnInitialized()
        {
            foreach (Joueur joueur in munchkinService.Joueurs)
                joueur.JoueurAChange += Joueur_JoueurAChange;

            munchkinService.CartesVisiblesOntChanges += MunchkinService_CartesVisiblesOnChanges;
            munchkinService.JoueursOntChanges += MunchkinService_JoueurAjoute;
            munchkinService.AfficheDe += MunchkinService_AfficheDe;

            base.OnInitialized();
        }

        private void AfficheDe()
        {
            _afficheDe = true;
        }

        private void JoueurALanceLeDe(bool alance)
        {
            munchkinService.JoueurLanceUnDe(Joueur);
        }

        private void MunchkinService_AfficheDe(object sender, EventArgs e)
        {
            _afficheDe = true;
            RafraichisInterface();
        }

        private async Task Return()
        {
            await ExitClicked.InvokeAsync(null);
        }

        private async void MunchkinService_JoueurAjoute(object sender, Joueur e)
        {
            e.JoueurAChange += Joueur_JoueurAChange;

            RafraichisInterface();
            await Task.Delay(100);

            await JavaScriptLibrary.UpdateLayoutAsync("list-item", jSRuntime);
        }

        private void MunchkinService_CartesVisiblesOnChanges(object sender, EventArgs e)
        {
            RafraichisInterface();
        }

        private void Joueur_JoueurAChange(object sender, EventArgs e)
        {
            RafraichisInterface();
        }

        private void RafraichisInterface()
        {
            System.Threading.Timer timer = new System.Threading.Timer(x =>
            {
                InvokeAsync(() =>
                {
                    StateHasChanged();
                });
            }, null, 100, 100);
        }

        private void SelectedCarteTypeChanged(Type type)
        {
            _selectedDefausseType = type;
            _selectionneTypeDefausse = false;

            if (_demandePioche)
                _affichePioche = true;
            else
                _afficheDefausse = true;
        }

        private void RegarderCartesJoueur(Joueur joueur)
        {
            _joueurRegarde = joueur;
            _afficheAutreJoueur = true;
        }

        private void SelectionneTypePioche()
        {
            _demandeDefausse = false;
            _selectedDefausseType = null;
            _selectionneTypeDefausse = true;
            _demandePioche = true;
        }

        private void SelectionneTypeDefausse()
        {
            _demandePioche = false;
            _selectedDefausseType = null;
            _selectionneTypeDefausse = true;
            _demandeDefausse = true;
        }

        private void Joueur_JeuAChange(object sender, EventArgs e)
        {
            RafraichisInterface();
        }

    }
}
