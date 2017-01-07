namespace Entities.BesluitInformatie
{
    public class BesluitInformatie : IValueObject
    {
        public int Besluit { get; set; }

        public decimal Aanspraak { get; set; }
    }
}