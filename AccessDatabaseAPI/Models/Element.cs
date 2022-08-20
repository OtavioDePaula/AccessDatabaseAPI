namespace AccessDatabaseAPI.Models
{
    public class Element
    {

        public int atomicNumber { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string atomicMass { get; set; }
        public string electronicConfiguration { get; set; }
        public double electronegativity { get; set; }
        public int atomicRadius { get; set; }
        public string ionRadius { get; set; }
        public int vanDerWaalsRadius { get; set; }
        public int ionizationEnergy { get; set; }
        public int electronAffinity { get; set; }
        public string oxidationStates { get; set; }
        public string standardState { get; set; }
        public string bondingType { get; set; }
        public int meltingPoint { get; set; }
        public int boilingPoint { get; set; }
        public double density { get; set; }
        public string groupBlock { get; set; }
        public string yearDiscovered { get; set; }
        public string block { get; set; }
        public string cpkHexColor { get; set; }
        public int period { get; set; }
        public int group { get; set; }
        public bool favorited { get; set; }

        //CONSTRUCTORS
        public Element()
        {
        }

    }
}