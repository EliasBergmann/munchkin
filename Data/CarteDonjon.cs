namespace Munchkin
{
    public class CarteDonjon : Carte
    {
        private readonly int _numeroDeCarte;
        public CarteDonjon(int numeroDeCarte)
        {
            _numeroDeCarte = numeroDeCarte;
        }

        public override string ImageURL => "/Cartes/Donjon/" + "D" + _numeroDeCarte + ".png";
    }
}
