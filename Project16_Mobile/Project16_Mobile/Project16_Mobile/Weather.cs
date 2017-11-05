using System;
using System.Collections.Generic;
using System.Text;

namespace Project16_Mobile
{
    public class Clouds
    {
        public int all { get; set; }
    }

    public class Coord
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class Main
    {
        public int humidity { get; set; }
        public int pressure { get; set; }
        public int temp { get; set; }
        public int temp_max { get; set; }
        public int temp_min { get; set; }
    }

    public class Sys
    {
        public string country { get; set; }
        public double message { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class Weather
    {
        public string description { get; set; }
        public string icon { get; set; }
        public int id { get; set; }
        public string main { get; set; }
    }

    public class Wind
    {
        public double deg { get; set; }
        public double speed { get; set; }
    }

    public class WeatherObject
    {
        public string @base { get; set; }
        public Clouds clouds { get; set; }
        public int cod { get; set; }
        public Coord coord { get; set; }
        public int dt { get; set; }
        public int id { get; set; }
        public Main main { get; set; }
        public string name { get; set; }
        public Sys sys { get; set; }
        public List<Weather> weather { get; set; }
        public Wind wind { get; set; }
    }
}
