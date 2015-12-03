using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitmapObjectFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             0 1 0 0 1 1
             1 0 0 0 0 1
             0 1 1 0 0 1
             0 0 0 0 1 1
             */
            bool[,] picture1 = new bool[4, 6] { { false, true, false, false, true, true }, 
                                               { true, false, false, false, false, true }, 
                                               { false, true, true, false, false, true }, 
                                               { false, false, false, false, true, true } };

            /*
             0 1 0 0 1 1
             1 1 0 0 0 1
             0 1 1 0 0 1
             0 0 1 1 1 1
             */
            bool[,] picture2 = new bool[4, 6] { { false, true, false, false, true, true }, 
                                               { true, true, false, false, false, true }, 
                                               { false, true, true, false, false, true }, 
                                               { false, false, true, true, true, true } };

            /*
             0 1 0 0 1 1
             1 1 0 0 0 1
             0 1 1 0 0 1
             0 0 0 0 1 1
             */
            bool[,] picture3 = new bool[4, 6] { { false, true, false, false, true, true }, 
                                               { true, true, false, false, false, true }, 
                                               { false, true, true, false, false, true }, 
                                               { false, false, false, false, true, true } };

            int result1 = FindNumberOfObjectsIterative(picture1);
            int result2 = FindNumberOfObjectsIterative(picture2);
            int result3 = FindNumberOfObjectsIterative(picture3);

            Console.WriteLine("Iterative Result for picture 1: " + result1);
            Console.WriteLine("Iterative Result for picture 2: " + result2);
            Console.WriteLine("Iterative Result for picture 3: " + result3);

            int rresult1 = FindNumberOfObjectsRecursive(picture1);
            int rresult2 = FindNumberOfObjectsRecursive(picture2);
            int rresult3 = FindNumberOfObjectsRecursive(picture3);

            Console.WriteLine("Recursive Result for picture 1: "+ rresult1);
            Console.WriteLine("Recursive Result for picture 2: " + rresult2);
            Console.WriteLine("Recursive Result for picture 3: " + rresult3);

            Console.ReadLine();
        }

        public static int FindNumberOfObjectsIterative(bool[,] picture)
        {
            List<BitmapObject> bitmaps = new List<BitmapObject>();

            for (int i = 0; i < picture.GetLength(0); i++)
            {
                for (int j = 0; j < picture.GetLength(1); j++)
                {
                    //skip false values 
                    if (!picture[i, j]) continue;

                    var currentCoordinate = new Tuple<int, int>(i, j);

                    BitmapObject existingObject = bitmaps.FirstOrDefault(x => x.IsAdjacent(currentCoordinate));
                    if (existingObject != null)
                    {
                        existingObject.AddCoordinate(currentCoordinate);
                    }
                    else
                    {
                        BitmapObject bitmapObject = new BitmapObject(i, j);
                        bitmaps.Add(bitmapObject);
                    }
                }
            }

            var bitmapsToRemove = NumberOfAdjacentBitmaps(bitmaps);

            return bitmaps.Count - bitmapsToRemove;
        }

        public static int NumberOfAdjacentBitmaps(List<BitmapObject> objects)
        {
            int count = 0;

            for (int i = 0; i < objects.Count; i++)
            {
                for (int j = i + 1; j < objects.Count; j++)
                {
                    if (objects[i].IsAdjacent(objects[j]))
                    {
                        count++;
                    }
                }
            }

            return count;
        }


        public static int FindNumberOfObjectsRecursive(bool[,] picture)
        {
            List<BitmapObject> bitmaps = new List<BitmapObject>();
            bool[,] visited = new bool[picture.GetLength(0), picture.GetLength(1)];

            for (int i = 0; i < picture.GetLength(0); i++)
            {
                for (int j = 0; j < picture.GetLength(1); j++)
                {
                    //skip false and visited values 
                    if (!picture[i, j] || visited[i, j]) continue;

                    var currentCoordinate = new Tuple<int, int>(i, j);
                    BitmapObject bitmapObject = new BitmapObject();
                    FindAdjacentNodes(currentCoordinate, bitmapObject, picture, visited);
                    bitmaps.Add(bitmapObject);
                }
            }


            return bitmaps.Count;
        }

        public static void FindAdjacentNodes(Tuple<int,int> currentNode, BitmapObject obj, bool[,] picture, bool[,] visited)
        {
            visited[currentNode.Item1, currentNode.Item2] = true;
            if (!picture[currentNode.Item1, currentNode.Item2]) return;

            if (obj.ContainsCoordinate(currentNode)) return;

            obj.AddCoordinate(currentNode);

            var top = currentNode;
            var bottom = currentNode;
            var left = currentNode;
            var right = currentNode;

            if (currentNode.Item1 - 1 >= 0)
            {
                top = new Tuple<int, int>(currentNode.Item1 - 1, currentNode.Item2);
                FindAdjacentNodes(top, obj, picture, visited);
            }
            if (currentNode.Item1 + 1 < picture.GetLength(0))
            {
                bottom = new Tuple<int, int>(currentNode.Item1 + 1, currentNode.Item2);
                FindAdjacentNodes(bottom, obj, picture, visited);
            }
            if (currentNode.Item2 - 1 >= 0)
            {
                left = new Tuple<int, int>(currentNode.Item1, currentNode.Item2 - 1);
                FindAdjacentNodes(left, obj, picture, visited);
            }
            if (currentNode.Item2 + 1 < picture.GetLength(1))
            {
                right = new Tuple<int, int>(currentNode.Item1, currentNode.Item2 + 1);
                FindAdjacentNodes(right, obj, picture, visited);
            }
        }

    }
}
