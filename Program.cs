/*
 * Name:            Briana Lindquist
 * College Class:   DotNet
 * Assignment:      A4 Movie Library Assignment
 * Due Date:        2023-02-20
 */

using A4Movie.Modules;
using System.Security.Cryptography.X509Certificates;

namespace A4Movie
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            // config is loaded using xml (NLog.config saved in  debug folder)
            //logger.Info("Program started");

            // path to movie data file
            string file = $"{Environment.CurrentDirectory}/data/movies.csv";

            // make sure movie file exists
            if (!File.Exists(file))
            {
                Console.WriteLine($"File does not exist: {file}");
            }
            else
            {
                //Displays the main menu
                int menuInput = Movie.MainMenu();

                //Creates list object to track movies
                List<Movie> movies = new List<Movie>();

                // to populate the object with data, read from the data file
                try
                {
                    StreamReader sr = new StreamReader(file);
                    // first line contains column headers
                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        ulong id = 0;
                        string title;
                        string genre;
                        string line = sr.ReadLine();
                        // first look for quote(") in string
                        // this indicates a comma(,) in movie title
                        int idx = line.IndexOf('"');
                        if (idx == -1)
                        {
                            // no quote = no comma in movie title
                            // movie details are separated with comma(,)
                            string[] movieDetails = line.Split(',');
                            // 1st array element contains movie id
                            id = UInt64.Parse(movieDetails[0]);
                            // 2nd array element contains movie title
                            title = movieDetails[1];
                            // 3rd array element contains movie genre(s)
                            // replace "|" with ", "
                            genre = movieDetails[2].Replace("|", ", ");
                        }
                        else
                        {
                            // quote = comma in movie title
                            // extract the movieId
                            id = UInt64.Parse(line.Substring(0, idx - 1));
                            // remove movieId and first quote from string
                            line = line.Substring(idx + 1);
                            // find the next quote
                            idx = line.IndexOf('"');
                            // extract the movieTitle
                            title=line.Substring(0, idx);
                            // remove title and last comma from the string
                            line = line.Substring(idx + 2);
                            // replace the "|" with ", "
                            genre = line.Replace("|", ", ");
                        }
                        movies.Add(new Movie(id,title,genre));
                    }
                    // close file when done
                    sr.Close();
                }
                catch (Exception ex)
                {
                    //logger.Error(ex.Message);
                    Console.Write("Error occured");
                }
                //logger.Info("Movies in file {Count}", MovieIds.Count);


                //Switch statement to run various methods depending on menu option chosen
                while (menuInput != 3)
                {
                    switch (menuInput)
                    {
                        //List movies
                        case 1:
                            {
                                Movie.MovieList(movies);
                                break;
                            }
                        //Add movie
                        case 2:
                            {
                                string movieTitle = Input.GetStringWithPrompt("Enter movie title: ", "Entry must be text: ");

                                // check for duplicate title
                                //List<string> LowerCaseMovieTitles = MovieTitles.ConvertAll(t => t.ToLower());
                                //List<Movie> LowerCaseMovieTitles = movies.ConvertAll(t => t.ToLower());

                                //if (LowerCaseMovieTitles.Contains(movieTitle.ToLower()))
                                //{
                                //    Console.WriteLine("That movie has already been entered");
                                //logger.Info("Duplicate movie title {Title}", movieTitle);
                                //}
                                //else
                                //{

                                    // generate movie id - use max Id value in movies list + 1
                                    UInt64 movieId = movies.Max(x => x.Id) + 1;
                                    // input genres
                                    List<string> genres = new List<string>();
                                    string genre;
                                    do
                                    {
                                        // ask user to enter genre
                                        genre = Input.GetStringWithPrompt("Enter genre (or done to quit): ", "Entry must be text: ");
                                        // if user enters "done"
                                        // or does not enter a genre do not add it to list
                                        if (genre != "done" && genre.Length > 0)
                                        {
                                            genres.Add(genre);
                                        }
                                    } while (genre != "done");
                                    // specify if no genres are entered
                                    if (genres.Count == 0)
                                    {
                                        genres.Add("(no genres listed)");
                                    }
                                    // use "|" as delimeter for genres
                                    string genresString = string.Join("|", genres);

                                    // if there is a comma(,) in the title, wrap it in quotes
                                    movieTitle = movieTitle.IndexOf(',') != -1 ? $"\"{movieTitle}\"" : movieTitle;

                                    // display movie id, title, genres
                                    Console.WriteLine($"{movieId},{movieTitle},{genresString}");

                                    // create file from data
                                    StreamWriter sw = new StreamWriter(file, true);
                                    sw.WriteLine($"{movieId},{movieTitle},{genresString}");
                                    sw.Close();

                                    // add movie details to List
                                    genresString = genresString.Replace("|", ", ");

                                    movies.Add(new Movie(movieId, movieTitle, genresString));
    
                                    Console.WriteLine();

                                // log transaction
                                //logger.Info("Movie id {Id} added", movieId);
                                //}
                                break;
                            }
                        //Exit
                        case 3:
                            {
                                break;
                            }
                    }
                    //Display the menu again
                    menuInput = Movie.MainMenu();
                }
                //logger.Info("Program ended");
            }
        }
    }
}