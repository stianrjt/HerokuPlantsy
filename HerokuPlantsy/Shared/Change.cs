using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerokuPlantsy.Shared
{
	public class Change
	{
		public Guid ID { get; set; }
		public Guid PlantID { get; set; }
		public string ChanfeInfo { get; set; }
		public DateTimeOffset ChangeDate { get; set; }
	}
}
