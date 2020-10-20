using Munchkin.Data;
using Munchkin.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Munchkin.Service
{
    public class MunchkinService
    {
        #region Constructor
        public MunchkinService()
        {
            _partieActuelle.JoueursOntChanges += _partieActuelle_JoueursOntChanges;
            _partieActuelle.CartesVisiblesOntChanges += _partieActuelle_CartesVisiblesOntChanges;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Pioche des cartes Tresor
        /// </summary>
        public List<CarteTresor> PiocheCartesTresor => _partieActuelle.PiocheCartesTresor;

        /// <summary>
        /// Pioche des cartes Donjon
        /// </summary>
        public List<CarteDonjon> PiocheCartesDonjon => _partieActuelle.PiocheCartesDonjon;

        /// <summary>
        /// Defausse des cartes Tresor
        /// </summary>
        public List<CarteTresor> DefausseCartesTresor => _partieActuelle.DefausseCartesTresor;

        /// <summary>
        /// Defausse des cartes Donjon
        /// </summary>
        public List<CarteDonjon> DefaussesCartesDonjon => _partieActuelle.DefaussesCartesDonjon;

        /// <summary>
        /// Joueurs de la partie actuelle
        /// </summary>
        public IEnumerable<Joueur> Joueurs => _partieActuelle.Joueurs;

        /// <summary>
        /// Liste de cartes actuellement visible par tous les joueurs 
        /// </summary>
        public IEnumerable<Carte> CartesVisibles => _partieActuelle.CartesVisibles;

        /// <summary>
        /// Parties enregistrées
        /// </summary>
        public List<Partie> Parties = new List<Partie>();
        #endregion

        #region Private Properties

        /// <summary>
        /// Partie Actuelle
        /// </summary>
        private Partie _partieActuelle = new Partie();
        #endregion


        #region Public Methods
        public void DefausseCarte(Carte carte) => _partieActuelle.DefausseCarte(carte);
        public void PrendCarteDePiocheDonjon(CarteDonjon carte, Joueur joueur) => _partieActuelle.PrendCarteDePiocheDonjon(carte, joueur);
        public void PrendCarteDePiocheTresor(CarteTresor carte, Joueur joueur) => _partieActuelle.PrendCarteDePiocheTresor(carte, joueur);        
        public void PrendCarteDefausseDonjon(CarteDonjon carte, Joueur joueur) => _partieActuelle.PrendCarteDefausseDonjon(carte, joueur);        
        public void PrendCarteDeDefausseTresor(CarteTresor carte, Joueur joueur) => _partieActuelle.PrendCarteDeDefausseTresor(carte, joueur);        
        public void TransfertCarte(Carte carte, Joueur envoyeur, Joueur receveur) => _partieActuelle.TransfertCarte(carte, envoyeur, receveur);       
        public void NouvelleCarteTresor(Joueur joueur = null) =>_partieActuelle.NouvelleCarteTresor(joueur);     
        public void NouvelleCarteDonjon(Joueur joueur = null) => _partieActuelle.NouvelleCarteDonjon(joueur);    
        public void AjouteUnNouveauJoueur(Joueur joueur) => _partieActuelle.AjouteUnNouveauJoueur(joueur);

        public void Recommence()
        {
            _partieActuelle.JoueursOntChanges -= _partieActuelle_JoueursOntChanges;
            _partieActuelle.CartesVisiblesOntChanges -= _partieActuelle_CartesVisiblesOntChanges;
            _partieActuelle = new Partie();
            _partieActuelle.JoueursOntChanges += _partieActuelle_JoueursOntChanges;
            _partieActuelle.CartesVisiblesOntChanges += _partieActuelle_CartesVisiblesOntChanges;
        }

        public void AfficheDePourTous()
        {
            AfficheDe?.Invoke(this, null);
        }

        public void AfficheResultatPourTous(string result)
        {
            AfficheResultat?.Invoke(this, result);
        }

        public void NotifieCartesOntChanges()
        {
            CartesOntChanges?.Invoke(null, null);
        }

        #endregion

        #region Private Methods
        private void _partieActuelle_CartesVisiblesOntChanges(object sender, EventArgs e)
        {
            CartesVisiblesOntChanges?.Invoke(this, null);
        }

        private void _partieActuelle_JoueursOntChanges(object sender, EventArgs e)
        {
            JoueursOntChanges?.Invoke(this, null);
        }
        #endregion

        public event EventHandler CartesVisiblesOntChanges;
        public event EventHandler JoueursOntChanges;
        public event EventHandler CartesOntChanges;
        public event EventHandler AfficheDe;

        public event EventHandler<string> AfficheResultat;
    }
}
