namespace Munchkin
{
    public abstract class Carte
    {
        public string Titre { get; set; }
        public abstract string ImageURL { get; }
        public bool Activee { get; set; } = true;
        public bool EstVisible { get; set; } = false;
    }
}
