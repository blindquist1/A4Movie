using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4Movie.Modules
{
    internal class Movie
    {
        //Properties
        public ulong Id { get; set; }
        public string Title { get; set; }
        public string Genres { get; set; }

        //Entry of a new movie
        public Movie(ulong id, string title, string genres)
        {
            Id = id;
            Title = title;
            Genres = genres;
        }

        //Display of the main menu
        public static int MainMenu()
        {
            Console.WriteLine("Movie Console");
            Console.WriteLine("1. List Movies");
            Console.WriteLine("2. Add Movie");
            Console.WriteLine("3. Exit");
            Console.WriteLine();

            bool validEntry = false;
            int menuSelection = 0;

            //Keep looping through until user chooses a valid entry, an integer and between 1 and 3.
            while (!validEntry)
            {
                menuSelection = Input.GetIntWithPrompt("Select an option: ", "Entry must be an integer");
                if (menuSelection < 1 || menuSelection > 3)
                {
                    Console.WriteLine("Entry must be between 1 and 3");
                }
                else
                {
                    validEntry = true;
                }
            }
            //logger.Info($"User choice: {menuSelection}");
            return menuSelection;
        }
        //Displays the list of movies
        public static void MovieList(List<Movie> movies)
        {
            if (movies.Count == 0)
            {
                Console.WriteLine("There are no movies in the file");
            }
            else
            { 
                //ask user how many movies to display
                int display = Input.GetIntWithPrompt("Enter number of movies to display: ", "Entry must be an integer: ");
                
                if (display > movies.Count)
                {
                    display = movies.Count;
                }

                for (int i = 0; i < display; i++)
                {
                    Console.WriteLine($"ID: {movies[i].Id}");
                    Console.WriteLine($"Title: {movies[i].Title}");
                    Console.WriteLine($"Genres: {movies[i].Genres}");
                    Console.WriteLine();
                }
            }
        }

    }
}
