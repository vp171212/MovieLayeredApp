using MovieStoreApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApp.Model
{
    internal class MovieManager
    {
        private List<Movies> moviesList=new List<Movies>();
        public List<Movies> GetMovies()
        { if (moviesList.Count > 0)
                return moviesList;
            throw new ListIsEmptyException("First Add some movies." +
                "List is Empty!");
        }

       public void AddMovie(Movies movies)
        {  if(moviesList.Count < 5)
            moviesList.Add(movies);
            throw new ListIsFullException("Maximum 5 movie details is added." +
                "List is full!");
        }

        public string FindMoviesByYear(int year)
        {
            Movies movie = moviesList.Find(x => x.Year == year);
            if (movie == null)
            {
                return "Not Found";
            }   
                return movie.MovieName;   
        }

       public void RemoveMovie(string movieName)
        {
            Movies movie=moviesList.Find(x=>x.MovieName == movieName);
            moviesList.Remove(movie);
        }

        public void DeleteAll()
        {
           moviesList.Clear();
        }
    }
}
