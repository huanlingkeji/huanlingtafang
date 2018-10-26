using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Assets.Script.Game
{

    public class Map
    {
        private static int WIDTH_POINTS = 20;           //20个
        private static int HEIGHT_POINTD = 48;          //48个；

        private Random random;

        public int[][] map = new int[WIDTH_POINTS][];
        public static bool[][] isMapBuild = new bool[WIDTH_POINTS][];

        public Map()
        {
            init_data();
        }

        void init_data()
        {
            for (int i = 0; i < WIDTH_POINTS; i++)
            {
                map[i] = new int[HEIGHT_POINTD];
                isMapBuild[i] = new bool[HEIGHT_POINTD];
            }
            create_map();
        }
        void create_map()
        {
            for (int i = 0; i < WIDTH_POINTS; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < HEIGHT_POINTD; j++)
                    {
                        map[i][j] = 1;
                    }
                }
                else
                {
                    if (i % 4 == 1)
                    {
                        map[i][0] = 1;
                    }
                    else if (i % 4 == 3)
                    {
                        map[i][HEIGHT_POINTD - 1] = 1;
                    }
                }
            }
            for (int i = 0; i < WIDTH_POINTS; i++)
            {
                map[i][40] = 2;
            }
        }
    }
}
