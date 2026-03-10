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

		static int currentHighScore=0;
		static int allTimeHighScore=0;

		public void updateHighScore()
		{
			if(File.Exists(".\\HighScore.thesixtext"))
			{
				string content = File.ReadAllText(".\\HighScore.thesixtext");
				if(!Int32.TryParse(content, out allTimeHighScore))
					File.Delete(".\\HighScore.thesixtext");
				File.Create(".\\highscore.thesixtext");
				File.WriteAllText(".\\HighScore.thesixtext", currentHighScore.ToString());
			}
			else
			{
				File.Create(".\\HighScore.thesixtext");
				File.WriteAllText(".\\HighScore.thesixtext", "0");
			}
			for(int x=0; x<4; ++x)
				for(int y=0; y<4; ++y)
					if(grid[x,y]>currentHighScore)
						currentHighScore=grid[x,y];
			if(allTimeHighScore<currentHighScore)
			{
				allTimeHighScore=currentHighScore;
			}
		}

		public static bool display()
		{
			Console.Clear();
			for(int y=0; y<4; ++y)
			{
				for(int x=0; x<4; ++x)
				{
					Console.Write(grid[x,y]);
					Console.Write(' ');
					Console.Write(' ');
					Console.Write(' ');
					Console.WriteLine("");
				}

				Console.WriteLine();
				Console.WriteLine();

				Console.WriteLine("HighScore: " + currentHighScore);
				Console.WriteLine("AllTime: " + allTimeHighScore);
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
				if(grid[x,y]==0)
					break;
			}

			if(rand.Next(1, 101) <= chanseFor4 && canGive4)
				grid[x,y]=4;
			else
				grid[x,y]=2; 
		}

		public static char getKey()
		{
			ConsoleKeyInfo buttonPress;
			buttonPress=Console.ReadKey();
			if(buttonPress.Key == ConsoleKey.W)
				return 'w';
			if(buttonPress.Key == ConsoleKey.A)
				return 'a';
			if(buttonPress.Key == ConsoleKey.S)
				return 's';
			if(buttonPress.Key == ConsoleKey.D)
				return 'd';

			return '0';
		}

		static void SortList(int[] listPar)
		{
			for(int i=0; i<4; ++i)
				for(int j=i; j<4; ++j)
					if(i!=j)
						if(listPar[i] == listPar[j])
						{
							listPar[i]=listPar[i]+listPar[j];
							listPar[j]=0;
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

		static bool MoveLogic()
		{
			char move=getKey();
			bool redo=true;
			int[] tempLine = new int[4];

			int[,] gridCopy = {
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
				redo=true;
			}
			for(int x=0; x<4; ++x)
				for(int y=0; y<4; ++y)
					if(gridCopy[x,y] != grid[x,y])
						return true;
			return false;
		}
		public static void Main(string[] args)

		{
			
			updateHighScore();
			addBlock(true, 10);
			while(true) // gameloop
			{
				display();
				if(MoveLogic())
					addBlock(true, 10);
				updateHighScore();
			}
		}
	}
}
