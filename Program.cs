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

		public static bool display()
		{
			Console.Clear();
			for(int y=0; y<4; ++y)
			{
				for(int x=0; x<4; ++x)
				{
					Console.Write(grid[x,y]);
					Console.Write(' ');
				}

				Console.WriteLine();
				Console.WriteLine();
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

		static void MoveLogic()
		{
			int x=0, y=0;
			int xDir=0, yDir=0;
			char move=getKey();
			if(move=='w')
			{
				yDir=-1;
				y=3;
			}
			if(move=='s')
			{
				yDir=1;
				y=0;
			}
			if(move=='a')
			{
				xDir=-1;
				x=3;
			}
			if(move=='d')
			{
				xDir=1;
				x=0;
			}

			int[] newLine = {0, 0, 0, 0};
			int addedBlocks=0;
			if(xDir!=0) 
				for(; y<4; ++y)
				{
					for(int iX=0; iX<4; ++iX)
						for(int i=iX-xDir; i+iX!=-1 && i+x!=4; i-=xDir)
							if(grid[x+i,y]==grid[x,y] && grid[x,y]!=0)
							{
								newLine[addedBlocks++]=grid[x,y]*2;
								grid[x+i,y]=0;
								grid[x,y]=0;
								break;
							}
					// lägg till så att newLine stoppar in i grid >:(
				}
			if(yDir!=0) ;
		}
		public static void Main(string[] args)

		{
			
			addBlock(true, 10);
			while(true) // gameloop
			{
				addBlock(true, 10);
				display();
				MoveLogic();
			}
		}
	}
}

/*
 

				while(hasMoved==false)
				{
					hasMoved=false;
					for(int x=0; x<4; ++x)
						for(int y=0; y<4; ++y)
						{
							if(move=='w')
							{
								if(y==0)
									++y;
								if(grid[x,y]==grid[x,y-1])
								{
									grid[x,y-1]*=2;
									grid[x,y]=0;
									hasMoved=true;
								}
								if(grid[x,y-1]==0 && grid[x,y]!=0)
								{
									grid[x,y-1]=grid[x,y];
									grid[x,y]=0;
									hasMoved=true;
								}
							}
							else if(move=='a')
							{
								if(x==0)
									++x;
								if(grid[x,y]==grid[x-1,y])
								{
									grid[x-1,y]*=2;
									grid[x,y]=0;
									hasMoved=true;
								}
								if(grid[x-1,y]==0 && grid[x,y]!=0)
								{
									grid[x-1,y]=grid[x,y];
									grid[x,y]=0;
									hasMoved=true;
								}
							}
							else if(move=='s' && y!=3)
							{
								if(grid[x,y]==grid[x,y+1])
								{
									grid[x,y+1]*=2;
									grid[x,y]=0;
									hasMoved=true;
								}
								if(grid[x,y+1]==0 && grid[x,y]!=0)
								{
									grid[x,y+1]=grid[x,y];
									grid[x,y]=0;
									hasMoved=true;
								}
							}
							else if(move=='d' && x!=3)
							{
								if(grid[x,y]==grid[x+1,y])
								{
									grid[x+1,y]*=2;
									grid[x,y]=0;
									hasMoved=true;
								}
								if(grid[x+1,y]==0 && grid[x,y]!=0)
								{
									grid[x+1,y]=grid[x,y];
									grid[x,y]=0;
									hasMoved=true;
								}
							}
						}
					}

*/
