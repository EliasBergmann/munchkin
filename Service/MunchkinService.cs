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
        #region Public Properties

        /// <summary>
        /// Parties enregistrées
        /// </summary>
        public IEnumerable<Partie> Parties => _parties;

        #endregion

        #region Private Properties
        private List<Partie> _parties = new List<Partie>();
        #endregion


        public void AjouteNouvellePartie(Partie partie)
        {
            _parties.Add(partie);
        }

        public void SupprimePartie(Partie partie)
        {
            _parties.Remove(partie);
        }

    }
}
