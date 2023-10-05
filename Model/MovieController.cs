using MovieStoreApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApp.Model
{
    internal class MovieController
    {
        private MovieManager manager=new MovieManager();

        string filePath = ConfigurationManager.AppSettings["movieFile"];
      
            
        public void Start()
        {
            File.WriteAllText(filePath, string.Empty);
            displayMenuMethod();
        }

        public void displayMenuMethod()
        { 
            while (true) {
                Console.WriteLine("Please select an action");
                Console.WriteLine("To Display the movies list: 1 ");
                Console.WriteLine("To add movie : 2");
                Console.WriteLine("To find movie by year: 3");
                Console.WriteLine("To Remove movie by name: 4");
                Console.WriteLine("To clear list: 5");
                Console.WriteLine("To exit: 6");
                char command = (char)Console.Read();
                string name1 = Console.ReadLine();
                switch (command)
                {
                    case '1':
                        try
                        {
                            List<Movies> movies = manager.GetMovies();
                            Console.WriteLine("Movie List:");
                            using (StreamReader reader = new StreamReader(filePath))
                            {
                                Console.WriteLine("List of movies are: ");
                                Console.WriteLine(reader.ReadToEnd());
                            }
                        }
                        catch(ListIsEmptyException lie)
                        {
                            Console.WriteLine(lie.Message);
                        }
                        Console.WriteLine("------------------------------------------------");
                        break;
                    case '2':
                        Console.WriteLine("Enter movie name: ");
                        string name = Console.ReadLine();

                        Console.WriteLine("Enter movie Id: ");
                        int id = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Enter Movie Year: ");
                        int year = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Enter movie Director");
                        string director = Console.ReadLine();

                        Movies movie = new Movies(id, name, year, director);
                        try
                        {
                            manager.AddMovie(movie);
                            AddMoviesToFile();
                        }
                        catch (ListIsFullException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case '3':
                        Console.WriteLine("Enter the year of movie");
                        int MovieYear = Convert.ToInt32(Console.ReadLine());
                        string nameOfMovie=manager.FindMoviesByYear(MovieYear);
                        Console.WriteLine($"Name: {nameOfMovie}");
                        Console.WriteLine("------------------------------------------------");
                        break;
                    case '4':
                        Console.WriteLine("Enter the name of movie: ");
                        string name2 = Console.ReadLine();
                        manager.RemoveMovie(name2);
                        Console.WriteLine("Removed Successfuly");
                        Console.WriteLine("------------------------------------------------");
                        break;
                    case '5':
                        manager.DeleteAll();
                        File.WriteAllText(filePath, string.Empty);
                        Console.WriteLine("File is cleared");
                        Console.WriteLine("------------------------------------------------");
                        break;
                    case '6':
                        Console.WriteLine("Thank You");
                        return;
                    default:
                        Console.WriteLine("Invalid Command");
                        break;
                } 
            }
        }

        private void AddMoviesToFile()
        {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    foreach (Movies movie in manager.GetMovies())
                    {
                        writer.WriteLine("Movie Details:");
                        writer.WriteLine("Movie Id: " + movie.MovieId);
                        writer.WriteLine("Movie Name: " + movie.MovieName);
                        writer.WriteLine("Release Year: " + movie.Year);
                        writer.WriteLine("Movie Director: " + movie.Director);
                    }
                    Console.WriteLine("Movie added successfully");
                    Console.WriteLine("------------------------------------------------");
                }
        }
    }
}
