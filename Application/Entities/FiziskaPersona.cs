namespace AutoServiss.Application.Entities
{
    public class FiziskaPersona
    {
        public int Id { get; set; }
        public string PilnsVards { get; set; }
        public string Talrunis { get; set; }
        public string Epasts { get; set; }
        public string Adrese { get; set; }
        public string Piezimes { get; set; }
        public Transportlidzeklis[] Transportlidzekli { get; set; }
    }
}