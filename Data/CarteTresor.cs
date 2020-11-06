namespace Munchkin
{
    public class CarteTresor : Carte
    {
        private readonly int _numeroDeCarte;
        public CarteTresor(int numeroDeCarte)
        {
            _numeroDeCarte = numeroDeCarte;
        }

        public override string ImageURL => "/Cartes/Tresor/" + "T" + _numeroDeCarte + ".png";
    }
}
