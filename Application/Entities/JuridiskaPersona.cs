namespace AutoServiss.Application.Entities
{
    public class JuridiskaPersona
    {
        public int Id { get; set; }
        public string Nosaukums { get; set; }
        public string RegNumurs { get; set; }
        public string PvnNumurs { get; set; }
        public string Kontaktpersona { get; set; }
        public string Talrunis { get; set; }
        public string Epasts { get; set; }
        public string JuridiskaAdrese { get; set; }
        public string FiziskaAdrese { get; set; }
        public string Piezimes { get; set; }
        public Transportlidzeklis[] Transportlidzekli { get; set; }
    }
}