// obs detta är 2048 inte snake!!!

namespace Snake
{
	class Shaft
	{
		static Random rand = new Random(Guid.NewGuid().GetHashCode()); // random number with a "random" seed

		static int[,] grid = {
			{0, 0, 0, 0},
			{0, 0, 0, 0},
			{0, 0, 0, 0},
			{0, 0, 0, 0}
		};
		public static bool[] failedMove=[false, false, false, false];

		static int currentHighScore=0;
		static int allTimeHighScore=0;

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

		static void resetFail()
		{
			failedMove[0]=false;
			failedMove[1]=false;
			failedMove[2]=false;
			failedMove[3]=false;
		}

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
			
			updateHighScore();
			addBlock(true, 10);
			while(true) // gameloop
			{
				display();
				if(moveLogic())
					addBlock(true, 10);
				updateHighScore();
			}
		}
	}
}
