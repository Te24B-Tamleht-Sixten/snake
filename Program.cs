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
			return true;
		}

		public static void addBlock(bool canGive4, int chanseFor4 /*1 - 100, under or equal the variable it becomes a 4*/ ) 
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

		public static void Main(string[] args)
		{

			while(true) // gameloop
			{

			}
		}
	}
}
