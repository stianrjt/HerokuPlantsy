using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerokuPlantsy.Shared
{
	public class Plant
	{
		public Guid ID { get; set; }
		public string PlantName { get; set; }
		public string PlantType { get; set; }
		public Image Image { get; set; }
		public Guid ImageID { get; set; }
		public DateTimeOffset LastWatered { get; set; }
		public List<Water> WaterLog { get; set; }
		public string Info { get; set; }
		public List<Change> ChangeLog { get; set; }
	}
}
