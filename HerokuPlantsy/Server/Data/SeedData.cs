using HerokuPlantsy.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plantsy.Server.Data
{
	public class SeedData
	{
		public static void Initialize(ApplicationDbContext context)
		{
			List<List<Change>> changesList = new List<List<Change>>();
			for (int i = 0; i < 5; i++)
			{

				var changes = (from j in Enumerable.Range(0, 5)
						 select new Change
						 {
							 ChangeDate = DateTimeOffset.Now.AddDays(j),
							 ChanfeInfo = "Flytta planten ut.",

						 }).ToList();

				changesList.Add(changes);

			}
			
			List<List<Water>> waterList = new List<List<Water>>();
			for (int i = 0; i < 5; i++)
			{

				var water = (from j in Enumerable.Range(0, 5)
							 select new Water
							 {
								 WaterDate = DateTimeOffset.Now.AddDays(j),
								 Note = "Den fikk masse vann",

							 }).ToList();
				waterList.Add(water);
			}


			

			var plants = (from i in Enumerable.Range(0, 5)
						  select new Plant
						  {
							  ChangeLog = changesList[i],
							  Info = "RANDOM INFO om planten her ja :)",
							  PlantName = $"Plante{i}",
							  PlantType = $"PlanteType{i}",
							  WaterLog = waterList[i],

						  }).ToList();


			foreach (var plant in plants)
			{
				context.Plants.Add(plant);
			}

			context.SaveChanges();
		}

	}
}
