using System;

namespace AccessDatabaseAPI.Models
{
	public class Element
	{
		public int atomicNumber { get; set; }
		public string symbol { get; set; }
		public string name { get; set; }
		public string atomicMass { get; set; }
		public DateTime yearDiscovered { get; set; }
		public string cpkHexColor { get; set; }
		public int period { get; set; }
		public int groupfamily { get; set; }
		public GroupBlock groupBlock { get; set; }
		public StandardState standardState { get; set; }

		public Element()
		{
		}

        public Element(int atomicNumber, string symbol, string name, string atomicMass, DateTime yearDiscovered, string cpkHexColor, int period, int groupfamily, GroupBlock groupBlock, StandardState standardState)
        {
            this.atomicNumber = atomicNumber;
            this.symbol = symbol;
            this.name = name;
            this.atomicMass = atomicMass;
            this.yearDiscovered = yearDiscovered;
            this.cpkHexColor = cpkHexColor;
            this.period = period;
            this.groupfamily = groupfamily;
            this.groupBlock = groupBlock;
            this.standardState = standardState;
        }
    }
}