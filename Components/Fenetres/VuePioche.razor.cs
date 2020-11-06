using Microsoft.AspNetCore.Components;
using Munchkin.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Components
{
    public partial class VuePioche : ComponentBase
    {
        [Parameter] public Joueur Joueur { get; set; }

        [Parameter]
        public Type DefausseType
        {
            get => _defausseType;
            set
            {
                if (value != _defausseType)
                {
                    _nbCartesTresorAffichees = 0;
                    _nbCartesDonjonAffichees = 0;
                    _defausseType = value;
                }
            }
        }

        private Carte _carteAffichee = null;
        private bool _afficheCarteEnGrand = false;
        private Partie _partie => Joueur.Partie;

        private IEnumerable<Carte> _cartesDonjonAffichees
        {
            get
            {
                for (int i = _nbCartesDonjonAffichees - 1; i >= 0; i--)
                    yield return _partie.PiocheCartesDonjon.ElementAt(i);
            }
        }

        private IEnumerable<Carte> _cartesTresorAffichees
        {
            get
            {
                for (int i = _nbCartesTresorAffichees - 1; i >= 0; i--)
                    yield return _partie.PiocheCartesTresor.ElementAt(i);
            }
        }

        private int _nbCartesDonjonAffichees = 0;
        private int _nbCartesTresorAffichees = 0;
        private Type _defausseType;

        private void PrendCarteDePiocheTresor(CarteTresor carte, Joueur joueur)
        {
            _partie.PrendCarteDePiocheTresor(carte, joueur);
            _nbCartesTresorAffichees--;
            _afficheCarteEnGrand = false;
            StateHasChanged();
        }

        private void PrendCarteDePiocheDonjon(CarteDonjon carte, Joueur joueur)
        {
            _partie.PrendCarteDePiocheDonjon(carte, joueur);
            _nbCartesDonjonAffichees--;
            _afficheCarteEnGrand = false;
            StateHasChanged();
        }

        public void BougeCarteDonjon(int oldIndex, int newIndex)
        {
            CarteDonjon oldCarte = _partie.PiocheCartesDonjon.ElementAt(oldIndex);

            _partie.PiocheCartesDonjon.Remove(oldCarte);
            _partie.PiocheCartesDonjon.Insert(newIndex, oldCarte);

            StateHasChanged();
        }

        public void BougeCarteTresor(int oldIndex, int newIndex)
        {
            CarteTresor oldCarte = _partie.PiocheCartesTresor.ElementAt(oldIndex);

            _partie.PiocheCartesTresor.Remove(oldCarte);
            _partie.PiocheCartesTresor.Insert(newIndex, oldCarte);

            StateHasChanged();
        }

        private void AfficheCarteEnGrand(Carte carte)
        {
            _carteAffichee = carte;
            _afficheCarteEnGrand = true;
        }
    }
}
