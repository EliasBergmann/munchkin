using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Munchkin.Data;
using System;

namespace Munchkin.Pages
{
    public partial class VueJoueur : ComponentBase
    {

        [Inject] IJSRuntime JSRuntime { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

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

        private bool _afficheMain = true;
        private bool _afficheAutreJoueur = false;
        private bool _affichePioche = false;
        private bool _afficheDefausse = false;

        private Type _selectedCarteType = null;
        private bool _selectionneTypeDefausse = false;

        private bool _demandePioche = false;
        private bool _demandeDefausse = false;

        private Partie _partie => Joueur.Partie;
        private string _nomJoueur => Joueur.Nom;

        protected override void OnInitialized()
        {
            foreach (Joueur joueur in _partie.Joueurs)
                joueur.JoueurAChange += Joueur_JoueurAChange;

            _partie.CartesVisiblesOntChanges += MunchkinService_CartesVisiblesOntChanges;
            _partie.JoueursOntChanges += MunchkinService_JoueurOntChanges;
            _partie.CartesOntChanges += MunchkinService_CartesVisiblesOntChanges;

            RafraichisInterface();

            base.OnInitialized();
        }


        private void Return()
        {
            ExitClicked.InvokeAsync(null);
        }

        private void MunchkinService_JoueurOntChanges(object sender, EventArgs e)
        {
            foreach (Joueur joueur in _partie.Joueurs)
                joueur.JoueurAChange -= Joueur_JoueurAChange;

            foreach (Joueur joueur in _partie.Joueurs)
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
