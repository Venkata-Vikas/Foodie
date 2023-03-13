using System;
using SQLite;

namespace FoodiesApp
{
    public class Items
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string item { get; set; }
        public string description { get; set; }
        public int cost { get; set; }
    }
}
