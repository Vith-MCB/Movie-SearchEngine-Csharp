using Movie_Search_Engine;

class Program
{
    public static List<Movies> dramaMovies = new List<Movies>()
    {
        new Movies("The Shawshank Redemption", 9.2f),
        new Movies("The Godfather", 9.2f),
        new Movies("The Dark Knight", 9.0f),
        new Movies("12 Angry Men", 8.9f),
        new Movies("Schindler's List", 8.9f),
        new Movies("The Lord of the Rings: The Return of the King", 9.0f)
    };

    public static List<Movies> actionMovies = new List<Movies>
    {
        new Movies("John Wick", 8.0f),
        new Movies("The Matrix", 8.7f),
        new Movies("Fast and Furious", 7.2f),
        new Movies("Lethal Weapon", 7.6f),
        new Movies("Die Hard", 8.2f),
        new Movies("Terminator 2: Judgment Day", 8.5f)
    };

    public static Dictionary<string, List<Movies>> moviesDictionary = new Dictionary<string, List<Movies>>()
    {
        {"action", actionMovies },
        {"drama", dramaMovies }
    };

    public static List<string> keyList = new List<string>(moviesDictionary.Keys);

    public static void Main(string[] args)
    {
        /* Predefined Movie List */
        //User can input movies here aswell

        while (true)
        {
            Console.WriteLine("=================================");
            Console.WriteLine("------ Movie Search Engine ------");
            Console.WriteLine("=================================");

            Console.WriteLine("[1] Search Movie by genre");
            Console.WriteLine("[2] Insert Movie");
            Console.WriteLine("[3] Leave");
            Console.WriteLine("[4] Clean console");

            string interfaceAns = Console.ReadLine();

            if (interfaceAns.Equals("1"))
            {
                keyList = new List<string>(moviesDictionary.Keys);
                Console.Clear();

                SearchAndPrintMovies(moviesDictionary);
            }
            else if (interfaceAns.Equals("2"))
            {
                Console.Clear();
                while (true)
                {
                    Tuple<Movies, string> movieToAddTuple = AddMovie();

                    Movies movieToAdd = new Movies(movieToAddTuple.Item1.movieTitle, movieToAddTuple.Item1.movieRate);
                    string movieGenre = movieToAddTuple.Item2;

                    Console.WriteLine("\nMovie Name: {0}", movieToAdd.movieTitle);
                    Console.WriteLine("Movie Rate: {0}", movieToAdd.movieRate);
                    Console.WriteLine("Movie Genre: {0}\n", movieGenre);

                    while (true) //Handle confirmation of input
                    {
                        Console.WriteLine("Do you want to add this movie? [y/n]");
                        string userInput = Console.ReadLine().ToLower();

                        if (!string.IsNullOrEmpty(userInput))
                        {
                            if (userInput.Equals("y"))
                            {
                                break;
                            }
                            else if (userInput.Equals("n"))
                            {
                                return;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Input.");
                                continue; //Invalid input
                            }
                        }
                    }

                    Add(movieGenre, movieToAdd);

                    Console.WriteLine("Do you want to add another movie? [y/n]");
                    string userInputAddOtherMovie = Console.ReadLine().ToLower();
                    if (userInputAddOtherMovie.Equals("y"))
                    {
                        continue;
                    }
                    else if (userInputAddOtherMovie.Equals("n"))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input.");
                        continue; //Invalid input
                    }
                }
            }
            else if (interfaceAns.Equals("3"))
            {
                break;
            }
            else if (interfaceAns.Equals("4")) { Console.Clear(); }

            else
            {
                Console.WriteLine("Invalid Input!");
                continue;
            }
        }

    }

    public static Tuple<string, float> SplitInput()
    {
        float result;
        string input;

        while (true)
        {
            Console.WriteLine("Input a Movie (Name/ Rate):");
            input = Console.ReadLine().ToLower();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Inset a valid movie (Name / Rate)!");
                continue;
            }
            else
            {
                if (input.Contains("/"))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Input must contain '/'");
                    continue;
                }

            }

        }

        //Spliting the input from the user
        string[] parts = input.Split('/');
        string stringPart = parts[0].Trim();

        //Trying to parse the value
        if (float.TryParse(parts[1], out result))
        {
            float floatPart = result;
            return new Tuple<string, float>(stringPart, floatPart);
        }
        else
        {
            Console.WriteLine("Invalid float entered.");
            return new Tuple<string, float>(stringPart, 0f);
        }
    }
    public static Tuple<Movies, string> AddMovie()
    {
        Tuple<string, float> Movie;

        while (true)
        {
            Movie = SplitInput();


            if (Movie != null) //While loop to check if 
            {
                string movieGenre = "";

                while (true) //While loop Used to read genre input
                {
                    Console.WriteLine("Movie Genre: ");
                    movieGenre = Console.ReadLine().ToLower();

                    if (string.IsNullOrEmpty(movieGenre))
                    {
                        Console.WriteLine("Movie genre cant be null!");
                        continue;
                    }
                    else
                    {
                        break; //If genre input was valid
                    }
                }

                Movies addedMovie = new Movies(Movie.Item1, Movie.Item2);
                return new Tuple<Movies, string>(addedMovie, movieGenre);
            }
            else
            {
                Console.WriteLine("Invalid Movie! Do you want try again? [y/n]");
                string userResponse = Console.ReadLine().ToLower();

                if (userResponse.Equals("y")) { continue; }

            }
        }

    }
    public static void Add(String key, Movies movie)
    {
        if (moviesDictionary.ContainsKey(key))
        {

            moviesDictionary[key].Add(movie);
        }
        else
        {
            moviesDictionary.Add(key, new List<Movies> { movie });
        }


    }

    public static void SearchAndPrintMovies(Dictionary<string, List<Movies>> moviesDictionary)
    {
        while (true)
        {
            Console.WriteLine("Do you want to print all the genres? [y/n]");
            string allGenInput = Console.ReadLine().ToLower();

            if (allGenInput.Equals("y"))
            {
                Console.Clear();
                Console.WriteLine("Genre list:");
                foreach (string key in keyList)
                {
                    Console.WriteLine(key);
                }
                break;
            }
            else if (allGenInput.Equals("n"))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid Input.");
                continue; //Invalid input
            }
        }

        Console.WriteLine("Enter a genre to search for:");
        string genre = Console.ReadLine().ToLower();

        if (moviesDictionary.ContainsKey(genre))
        {
            Console.WriteLine("Do you want to print the movie list for the genre '{0}'? (y/n)", genre);
            string userInput = Console.ReadLine().ToLower();
            if (userInput.Equals("y"))
            {
                Console.WriteLine("Movies in the '{0}' genre:", genre);
                foreach (var movie in moviesDictionary[genre])
                {
                    Console.WriteLine("- {0} ({1})", movie.movieTitle, movie.movieRate);
                }
            }
            else
            {
                Console.WriteLine("Movie list not printed.");
            }
        }
        else
        {
            Console.WriteLine("Genre '{0}' not found.", genre);
        }

    }
}