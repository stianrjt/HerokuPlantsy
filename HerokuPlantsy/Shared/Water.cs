using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerokuPlantsy.Shared
{
	public class Water
	{
		public Guid ID { get; set; }
		public Guid PlantID { get; set; }
		public DateTimeOffset WaterDate { get; set; }
		public string Note { get; set; }
	}
}
