using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Search_Engine
{
    internal class Movies
    {
        public string movieTitle { get; set; }

        public float movieRate { get; set; }

        public Movies(string movieTitle, float movieRate)
        {
            this.movieTitle = movieTitle;
            this.movieRate = movieRate;
        }

    }
}
