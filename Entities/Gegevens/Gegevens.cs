namespace Entities.Gegevens
{
    public class Gegevens : IValueObject
    {
        public string Naam { get; set; }

        public string Voornaam { get; set; }

        public string Adres { get; set; }

        public string Huisnummer { get; set; }

        public string Postcode { get; set; }

        public string Gemeente { get; set; }

        public string Land { get; set; }
    }
}
