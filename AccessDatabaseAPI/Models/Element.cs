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
		public int group { get; set; }
		public bool favorited { get; set; }
		public GroupBlock groupBlock { get; set; }
		public StandardState standardState { get; set; }

		public Element()
		{
		}

		public Element(int atomicNumber_, string symbol_, string name_, string atomicMass_, DateTime yearDiscovered_, string cpkHexColor_, int period_, int groupfamily_, GroupBlock groupBlock_, StandardState standardState_)
		{
			this.atomicNumber = atomicNumber_;
			this.symbol = symbol_;
			this.name = name_;
			this.atomicMass = atomicMass_;
			this.yearDiscovered = yearDiscovered_;
			this.cpkHexColor = cpkHexColor_;
			this.period = period_;
			this.group = groupfamily_;
			this.groupBlock = groupBlock_;
			this.standardState = standardState_;
		}
	}
}