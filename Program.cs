namespace TwoThousandFourtyEight
{
	class Shaft
	{
		/*
		 * needed for non reapeating nummbers*/
		static Random rand = new Random(Guid.NewGuid().GetHashCode()); // random number with a "random" seed

		/*
		 * used to store data for most of computation and display*/
		static int[,] grid = {
			{0, 0, 0, 0},
			{0, 0, 0, 0},
			{0, 0, 0, 0},
			{0, 0, 0, 0}
		};

		/*
		 * used to store if a moved failed and if all fail there are no possible moves left. 0 w, 1 a, 2 s, 3 d*/
		public static bool[] failedMove=[false, false, false, false];

		static int currentHighScore=0;
		static int allTimeHighScore=0;


		/*
		 * Read in a file and updated highscore, if the file doesent exist it makes one */
		public static void updateHighScore()
		{
			if(File.Exists(".\\HighScore"))
			{
				string content = File.ReadAllText(".\\HighScore");
				if(!Int32.TryParse(content, out allTimeHighScore))
				{
					File.Delete(".\\HighScore");
					File.Create(".\\HighScore").Close();
					File.WriteAllText(".\\HighScore", currentHighScore.ToString()+Environment.NewLine);
				}
				else if(allTimeHighScore<currentHighScore)
					File.WriteAllText(".\\HighScore", currentHighScore.ToString()+Environment.NewLine);

			}
			else
			{
				File.Create(".\\HighScore").Close();
				File.WriteAllText(".\\HighScore", "0"+Environment.NewLine);
			}
			for(int x=0; x<4; ++x)
				for(int y=0; y<4; ++y)
					if(grid[x,y]>currentHighScore)
						currentHighScore=grid[x,y];
			if(allTimeHighScore<=currentHighScore)
			{
				allTimeHighScore=currentHighScore;
			}
		}

		/*
		 * displays through changing terminal color and then typing the nummber to the screen one at a time, very simple print algorithm 👍 */
		public static bool display()
		{
			Console.Clear();

				Console.ForegroundColor=ConsoleColor.Black;
				
				for(int i = 0; i<13; ++i)
					Console.Write(' ');
				Console.BackgroundColor=ConsoleColor.White;
				Console.WriteLine("HighScore: " + currentHighScore + "  AllTime: " + allTimeHighScore);
				Console.BackgroundColor=ConsoleColor.Black;

			Console.ForegroundColor=ConsoleColor.DarkCyan;
			for(int i = 0; i<17; ++i)
				Console.Write(' ');
			for(int i=0; i<9; ++i)
			{
				Console.Write('-');
				Console.Write(' ');
				
			}
			Console.WriteLine("");
			for(int y=0; y<4; ++y)
			{
				for(int i = 0; i<17; ++i)
					Console.Write(' ');
				Console.Write('|');
				for(int x=0; x<4; ++x)
				{	
					if(grid[x,y]<10 && x==3)	
						Console.Write(' ');
					if(grid[x,y]<1000 && x!=3)
						Console.Write(' ');
					switch(grid[x,y])
					{
						case 2:
							Console.ForegroundColor=ConsoleColor.White;
							break;
						case 4:
							Console.ForegroundColor=ConsoleColor.Yellow;
							break;
						case 8:
							Console.ForegroundColor=ConsoleColor.Green;
							break;
						case 16:
							Console.ForegroundColor=ConsoleColor.Red;
							break;
						case 32:
							Console.ForegroundColor=ConsoleColor.DarkBlue;
							break;
						case 64:
							Console.ForegroundColor=ConsoleColor.Magenta;
							break;
						case 128:
							Console.ForegroundColor=ConsoleColor.DarkRed;
							break;
						case 256:
							Console.ForegroundColor=ConsoleColor.Gray;
							break;
						case 512:
							Console.ForegroundColor=ConsoleColor.DarkMagenta;
							break;
						case 1024:
							Console.ForegroundColor=ConsoleColor.DarkYellow;
							break;
						default:
							Console.ForegroundColor=ConsoleColor.Blue;
							break;
					}
					if(grid[x,y]!=0)
						Console.Write(grid[x,y]);
					else
						Console.Write(' ');
					if(grid[x,y]<100 && x==3)	
						Console.Write(' ');
					if(grid[x,y]>10 && grid[x,y] <1000&& x==3)	
						Console.Write(' ');
					if(x!=3)
					{
						if(grid[x,y]<10)
							Console.Write(' ');

					if(grid[x,y]<100)
						Console.Write(' ');
					}
					else
						if(grid[x,y]<=10)
							Console.Write(' ');
					

				}
				Console.ForegroundColor=ConsoleColor.DarkCyan;
				Console.Write('|');
				if(y!=3)
				{
					Console.WriteLine("");
					Console.WriteLine("");
					Console.WriteLine("");
				}
			}
			Console.WriteLine("");
			for(int i = 0; i<17; ++i)
				Console.Write(' ');
			for(int i=0; i<9; ++i) {
				Console.Write('-');
				Console.Write(' ');
			}


			return true;
		}

		public static void addBlock(bool canGive4/*orkade inte göra en overide*/, int chanseFor4 /*1 - 100, under or equal the variable it becomes a 4*/ ) 
		{
			int x;
			int y;
			while(true)//written like this because the checking is to be done after the first coputation, could use for loop but this is eaasier
			{
				x = rand.Next(4);
				y = rand.Next(4);
				if(grid[x,y]==0) break;
			}

			if(rand.Next(1, 101) <= chanseFor4 && canGive4)
				grid[x,y]=4;
			else
				grid[x,y]=2; 
		}

		/*
		 * gets key and returns the wasd equivalent for future processing*/
		public static char getKey()
		{
			ConsoleKeyInfo buttonPress;
			while(true)
			{
			buttonPress=Console.ReadKey();
			if(buttonPress.Key == ConsoleKey.W)
				return 'w';
			if(buttonPress.Key == ConsoleKey.A)
				return 'a';
			if(buttonPress.Key == ConsoleKey.S)
				return 's';
			if(buttonPress.Key == ConsoleKey.D)
				return 'd';
			}
		}

		/*
		 * takes a list that is sorted by paris them removes spaces. This is for the stacking effekt of the game*/
		static void SortList(int[] listPar)
		{
			for(int i=0; i<4; ++i)
				for(int j=i+1; j<4; ++j)
				{
					if(listPar[i]!=listPar[j]&&listPar[j]!=0)
						break;
					else if(listPar[i] == listPar[j])
					{
						listPar[i]=listPar[i]+listPar[j];
						listPar[j]=0;
					}
				}


			for(int i=0; i<4; ++i)
				for(int j=0; j<i; ++j)
					if(listPar[j]==0 && i!=j)
					{
						listPar[j]=listPar[i];
						if(i!=j)
						listPar[i]=0;
					}
		}

		/*
		 * resets the fail counter so the game doesent end prematurly*/
		static void resetFail()
		{
			failedMove[0]=false;
			failedMove[1]=false;
			failedMove[2]=false;
			failedMove[3]=false;
		}

		/*
		 * Returns 1 if the game can continiue. Takes care of all movement through sortList() function. Also takes care of if the failscreen and its trigger.*/
		static bool moveLogic()
		{
			char move=getKey();
			bool redo=true;
			int[] tempLine = new int[4];

			int[,] gridCopy = 
			{
				{0, 0, 0, 0},
				{0, 0, 0, 0},
				{0, 0, 0, 0},
				{0, 0, 0, 0}
			};

			for(int x=0; x<4; ++x)
				for(int y=0; y<4; ++y)
					gridCopy[x,y]=grid[x,y];

			while(redo)
			{
				redo=false;
			if(move=='w')
			{
				for(int x=0; x<4; ++x)
				{
					for(int i=0; i<4; ++i)
						tempLine[i]=grid[x,i];
					SortList(tempLine);
					for(int i=0; i<4; ++i)
						grid[x,i]=tempLine[i];
				}
			}
			else if(move=='s')
			{
				for(int x=0; x<4; ++x)
				{
					for(int i=3; i>=0; --i)
						tempLine[3-i]=grid[x,i];
					SortList(tempLine);
					for(int i=3; i>=0; --i)
						grid[x,i]=tempLine[3-i];
					display();
				}
			}
			else if(move=='a')
			{
				for(int y=0; y<4; ++y)
				{
					for(int i=0; i<4; ++i)
						tempLine[i]=grid[i,y];
					SortList(tempLine);
					for(int i=0; i<4; ++i)
						grid[i,y]=tempLine[i];
				}
			}
			else if(move=='d')

			{
				for(int y=0; y<4; ++y)
				{
					for(int i=3; i>=0; --i)
						tempLine[3-i]=grid[i,y];
					SortList(tempLine);
					for(int i=3; i>=0; --i)
						grid[i,y]=tempLine[3-i];
				}
			}
			
			else
			{
				redo=true;
			}
			}
			for(int x=0; x<4; ++x)
				for(int y=0; y<4; ++y)
					if(gridCopy[x,y] != grid[x,y])
					{
						resetFail();
						return true;
					}

			if(move=='w')
				failedMove[0]=true;
			if(move=='a')
				failedMove[1]=true;
			if(move=='s')
				failedMove[2]=true;
			if(move=='d')
				failedMove[3]=true;

			if(failedMove[0] && failedMove[1] && failedMove[2] && failedMove[3])
			{
				Console.BackgroundColor=ConsoleColor.Black;
				Console.ForegroundColor=ConsoleColor.Cyan;
				Console.WriteLine("\n\ndu fick: " + currentHighScore + " som mest");
				System.Environment.Exit(1);
			}
			return false;
		}
		public static void Main(string[] args)

		{
			
			updateHighScore(); // makes you see you all time highscore in the start
			addBlock(true, 10); // starter so the game can start
			while(true) // gameloop
			{
				display(); // shows the first block and continues to display after
				if(moveLogic()) // unless moveLogic returns 1 it wont allow the program to add another block.
					addBlock(true, 10);
				updateHighScore();
			}
		}
	}
}
