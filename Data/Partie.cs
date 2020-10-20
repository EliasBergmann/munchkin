using Munchkin.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Data
{
    public class Partie
    {
        #region Constructor
        public Partie()
        {
            InitialiseCartes();
            _joueurs.CollectionChanged += Joueurs_CollectionChanged;
            _cartesVisibles.CollectionChanged += CartesVisibles_CollectionChanged;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Nom de la partie
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Pioche des cartes Tresor
        /// </summary>
        public List<CarteTresor> PiocheCartesTresor = new List<CarteTresor>();

        /// <summary>
        /// Pioche des cartes Donjon
        /// </summary>
        public List<CarteDonjon> PiocheCartesDonjon = new List<CarteDonjon>();

        /// <summary>
        /// Defausse des cartes Tresor
        /// </summary>
        public List<CarteTresor> DefausseCartesTresor = new List<CarteTresor>();

        /// <summary>
        /// Defausse des cartes Donjon
        /// </summary>
        public List<CarteDonjon> DefaussesCartesDonjon = new List<CarteDonjon>();

        /// <summary>
        /// Joueurs de la partie actuelle
        /// </summary>
        public IEnumerable<Joueur> Joueurs => _joueurs;

        /// <summary>
        /// Liste de cartes actuellement visible par tous les joueurs 
        /// </summary>
        public IEnumerable<Carte> CartesVisibles => _cartesVisibles;
        #endregion

        #region Private Properties
        private ObservableCollection<Joueur> _joueurs = new ObservableCollection<Joueur>();
        private ObservableCollection<Carte> _cartesVisibles = new ObservableCollection<Carte>();
        #endregion

        #region Public Methods

        /// <summary>
        /// Ajoute un nouveau joueur à la partie
        /// </summary>
        /// <param name="joueur">Joueur à ajouer</param>
        public void AjouteUnNouveauJoueur(Joueur joueur)
        {
            _joueurs.Add(joueur);
            DistribueCartes(joueur);
        }

        /// <summary>
        /// Place une cartre dans la défausse correspondante 
        /// </summary>
        /// <param name="carte">Carte à défausser</param>
        public void DefausseCarte(Carte carte)
        {
            if (_cartesVisibles.Contains(carte))
                _cartesVisibles.Remove(carte);

            if (carte is CarteDonjon)
                DefaussesCartesDonjon.Add(carte as CarteDonjon);
            else if (carte is CarteTresor)
                DefausseCartesTresor.Add(carte as CarteTresor);
        }

        public void PrendCarteDePiocheDonjon(CarteDonjon carte, Joueur joueur)
        {
            PiocheCartesDonjon.Remove(carte);
            joueur.Main.Add(carte);
        }

        public void PrendCarteDePiocheTresor(CarteTresor carte, Joueur joueur)
        {
            PiocheCartesTresor.Remove(carte);
            joueur.Main.Add(carte);
        }

        public void PrendCarteDefausseDonjon(CarteDonjon carte, Joueur joueur)
        {
            DefaussesCartesDonjon.Remove(carte);
            joueur.Main.Add(carte);
        }

        public void PrendCarteDeDefausseTresor(CarteTresor carte, Joueur joueur)
        {
            DefausseCartesTresor.Remove(carte);
            joueur.Main.Add(carte);
        }

        public void TransfertCarte(Carte carte, Joueur envoyeur, Joueur receveur)
        {
            if (envoyeur is null)
                _cartesVisibles.Remove(carte);
            else
            {
                if (envoyeur.Main.Contains(carte))
                    envoyeur.Main.Remove(carte);
                else if (envoyeur.Equipement.Contains(carte))
                    envoyeur.Equipement.Remove(carte);
            }

            if (receveur is null)
                _cartesVisibles.Add(carte);
            else
                receveur.Main.Add(carte);
        }

        public void NouvelleCarteTresor(Joueur joueur = null)
        {
            Carte carte = PiocheCartesTresor.PopAt(0);

            if (joueur is null)
                _cartesVisibles.Add(carte);
            else
                joueur.NouvelleCarte(carte);
        }

        public void NouvelleCarteDonjon(Joueur joueur = null)
        {
            Carte carte = PiocheCartesDonjon.PopAt(0);

            if (joueur is null)
                _cartesVisibles.Add(carte);
            else
                joueur.NouvelleCarte(carte);
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Initialises et mélange toutes les carte des pioches
        /// </summary>
        private void InitialiseCartes()
        {
            for (int i = 1; i <= 191; i++)
            {
                if ((i != 117) && (i != 116) && (i != 118) && (i != 3))
                    PiocheCartesTresor.Add(new CarteTresor(i) { Titre = "Carte " + i });
            }

            for (int i = 1; i <= 259; i++)
            {
                if ((i != 23) && (i != 20) && (i != 24) && (i != 25) && (i != 114) && (i != 225) && (i != 226) && (i != 227) && (i != 55) &&(i != 132))
                    PiocheCartesDonjon.Add(new CarteDonjon(i) { Titre = "Carte " + i });
            }

            PiocheCartesTresor.Shuffle();
            PiocheCartesDonjon.Shuffle();
        }

        /// <summary>
        /// Distribue 4 cartes de chaque pioche à un joueur
        /// </summary>
        /// <param name="joueur">JJoueur receveur</param>
        private void DistribueCartes(Joueur joueur)
        {
            for (int i = 0; i < 4; i++)
            {
                NouvelleCarteDonjon(joueur);
                NouvelleCarteTresor(joueur);
            }
        }

        private void CartesVisibles_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CartesVisiblesOntChanges?.Invoke(this, null);
        }

        private void Joueurs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            JoueursOntChanges?.Invoke(this, null);
        }
        #endregion

        #region Events
        public event EventHandler CartesVisiblesOntChanges;
        public event EventHandler JoueursOntChanges;
        #endregion
    }
}
