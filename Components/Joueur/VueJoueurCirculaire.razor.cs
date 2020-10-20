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
    public partial class VueJoueurCirculaire : ComponentBase
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

        private bool _afficheDe = false;
        private bool _showHand = true;
        private bool _afficheAutreJoueur = false;
        private bool _affichePioche = false;
        private bool _afficheDefausse = false;

        private Type _selectedCarteType = null;
        private bool _selectionneTypeDefausse = false;

        private bool _demandePioche = false;
        private bool _demandeDefausse = false;

        private string _resultatDe = null;

        protected override void OnInitialized()
        {
            foreach(Joueur joueur in munchkinService.Joueurs)
                joueur.JoueurAChange += Joueur_JoueurAChange;

            munchkinService.CartesVisiblesOntChanges += MunchkinService_CartesVisiblesOntChanges;
            munchkinService.JoueursOntChanges += MunchkinService_JoueurOntChanges;
            munchkinService.CartesOntChanges += MunchkinService_CartesVisiblesOntChanges;
            munchkinService.AfficheDe += MunchkinService_AfficheDe;
            munchkinService.AfficheResultat += MunchkinService_AfficheResultat;

            RafraichisInterface();

            base.OnInitialized();
        }

        private void AfficheDe()
        {
            munchkinService.AfficheDePourTous();
        }

        private void JoueurALanceLeDe(string result)
        {
            munchkinService.AfficheResultatPourTous(result);
        }

        private void MunchkinService_AfficheResultat(object sender, string e)
        {
            _resultatDe = e;
            RafraichisInterface();
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

        private void MunchkinService_JoueurOntChanges(object sender, EventArgs e)
        {
            foreach (Joueur joueur in munchkinService.Joueurs)
                joueur.JoueurAChange -= Joueur_JoueurAChange;

            foreach (Joueur joueur in munchkinService.Joueurs)
                joueur.JoueurAChange += Joueur_JoueurAChange;

            RafraichisInterface();
        }

        private void MunchkinService_CartesVisiblesOntChanges(object sender, EventArgs e)
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
            _selectedCarteType = type;
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
            _selectedCarteType = null;
            _selectionneTypeDefausse = true;
            _demandePioche = true;
        }

        private void SelectionneTypeDefausse()
        {
            _demandePioche = false;
            _selectedCarteType = null;
            _selectionneTypeDefausse = true;
            _demandeDefausse = true;
        }

        private void Joueur_JeuAChange(object sender, EventArgs e)
        {
            RafraichisInterface();
        }

    }
}
