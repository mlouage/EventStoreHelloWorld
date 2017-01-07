namespace Entities.Rekening
{
    public class Rekening : IValueObject
    {
        public string RekeningNummer { get; set; }

        public string Bic { get; set; }

        public decimal Saldo { get; set; }

        public string Valuta { get; set; }
    }
}