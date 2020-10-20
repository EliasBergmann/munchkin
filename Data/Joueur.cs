using Munchkin.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Data
{
    public class Joueur
    {
        private readonly MunchkinService _munchkinService;
        private int _niveau = 1;
        private string _nom = string.Empty;
        private int _forceObjets = 0;

        public ObservableCollection<Carte> Equipement { get; set; } = new ObservableCollection<Carte>();
        public ObservableCollection<Carte> Main { get; set; } = new ObservableCollection<Carte>();

        [Required]
        [StringLength(12, ErrorMessage = "Le Nom choisi est trop long.")]
        public string Nom 
        {
            get => _nom; 
            set
            {
                if (value != _nom)
                {
                    _nom = value;
                    OnJoueurAChange();
                }
            }
        }
        public int Niveau 
        {
            get => _niveau;
            set
            {
                if(value != _niveau)
                {
                    _niveau = value;
                    OnJoueurAChange();
                }
            }
        }

        public int ForceObjets
        {
            get => _forceObjets; 
            set
            {
                if (value != _forceObjets)
                {
                    _forceObjets = value;
                    OnJoueurAChange();
                }
            }
        } 

        public int PuissanceTotale => Niveau + ForceObjets;

        public Joueur(MunchkinService  munchkinService)
        {
            _munchkinService = munchkinService;
            Equipement.CollectionChanged += Jeu_CollectionChanged;
            Main.CollectionChanged += Jeu_CollectionChanged;
        }    

        private void OnJoueurAChange()
        {
            JoueurAChange?.Invoke(this, null);
        }

        private void Jeu_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            JeuAChange?.Invoke(this, null);
            _munchkinService.NotifieCartesOntChanges();
        }

        public void NouvelleCarte(Carte carte)
        {
            _munchkinService.TransfertCarte(carte,null,this);
        }

        public void PoserCarte(Carte carte)
        {
            Main.Remove(carte);
            Equipement.Add(carte);
        }

        public void EnvoyeCarte(Carte carte, Joueur receveur)
        {
            _munchkinService.TransfertCarte(carte, this, receveur);
        }

        public void BougeCarteEquipement(int oldIndex, int newIndex)
        {
            Carte carte = Equipement.ElementAt(oldIndex);

            Equipement.Remove(carte);
            Equipement.Insert(newIndex, carte);

            JeuAChange?.Invoke(this, null);
        }


        public void BougeCarteMain(int oldIndex, int newIndex)
        {
            Carte carte = Main.ElementAt(oldIndex);

            Main.Remove(carte);
            Main.Insert(newIndex,carte);

            JeuAChange?.Invoke(this, null);
        }

        public void DefausseCarte(Carte carte)
        {
            if (Main.Contains(carte))
                Main.Remove(carte);
            else if (Equipement.Contains(carte))
                Equipement.Remove(carte);

            _munchkinService.DefausseCarte(carte);
        }

        public event EventHandler JeuAChange;
        public event EventHandler JoueurAChange;
    }
}
