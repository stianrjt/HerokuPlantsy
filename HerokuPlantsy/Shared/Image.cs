using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerokuPlantsy.Shared
{
	public class Image
	{
		public Guid ID { get; set; }
		public Guid PlantID { get; set; }

		//is rlly imgSource base64
		public string ImagePath { get; set; }
		public string ImageName { get; set; }
	}
}
