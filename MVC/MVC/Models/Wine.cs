﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class Wine
    {
        public string ID { get; private set; }

        public string Country { get; private set; }

        public string Description { get; private set; }

        public string Designation { get; private set; }

        public string Points { get; private set; }

        public string Price { get; private set; }

        public string Region_1 { get; private set; }

        public string Region_2 { get; private set; }

        public string Variety { get; private set; }

        public string Winery { get; private set; }

        public static List<Wine> GetWineList()
        {
            List<Wine> myWine = new List<Wine>();
            string path = Environment.CurrentDirectory;
            //For testing, make sure the path is pointing to the root.
            string newPath = Path.GetFullPath(Path.Combine(path, @"wwwroot\wine.csv"));
            using (StreamReader reader = new StreamReader(newPath))
            {
                int counter = 0;
                string line;

                //only grab the top 1000 wines. 
                while (((line = reader.ReadLine()) != null) && counter < 1001)
                {
                    Regex parser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

                    //Separating columns to array
                    string[] wineList = parser.Split(line);

                    //Add new wine object to the list. 
                    myWine.Add(new Wine
                    {
                        ID = wineList[0],
                        Country = wineList[1],
                        Description = wineList[2],
                        Designation = wineList[3],
                        Points = wineList[4],
                        Price = wineList[5],
                        Region_1 = wineList[6],
                        Region_2 = wineList[7],
                        Variety = wineList[8],
                        Winery = wineList[9]

                    });

                    counter++;

                }
            }

            return myWine;
        }

        public static List<Wine> FilterWineList(string price, string pointRating)
        {
            List<Wine> allWines = GetWineList();
            IEnumerable<Wine> filteredByPrice = allWines.Where(w => w.Price == price);
            return filteredByPrice.Where(w => w.Points == pointRating).ToList();
        }

    }
}
