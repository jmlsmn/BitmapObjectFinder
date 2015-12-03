using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitmapObjectFinder
{
    public class BitmapObject
    {
        private List<Tuple<int, int>> coordinates = new List<Tuple<int, int>>();

        public BitmapObject(int x, int y)
        {
            this.AddCoordinate(new Tuple<int, int>(x, y));
        }

        public BitmapObject()
        {
        }

        public void AddCoordinate(Tuple<int, int> coordinate)
        {
            coordinates.Add(coordinate);
        }

        public bool ContainsCoordinate(Tuple<int, int> coordinate)
        {
            return coordinates.Contains(coordinate);
        }

        public List<Tuple<int, int>> GetCoordinates()
        {
            return coordinates;
        }

        public bool IsAdjacent(Tuple<int, int> xy)
        {
            foreach (var c in coordinates)
            {
                //check x-axis adjacency
                if (Math.Abs(c.Item2 - xy.Item2) == 1 && c.Item1 == xy.Item1)
                    return true;

                //check y-axis adjacency
                if (Math.Abs(c.Item1 - xy.Item1) == 1 && c.Item2 == xy.Item2)
                    return true;
            }
            return false;
        }

        public bool IsAdjacent(BitmapObject obj)
        {
            foreach (var c in obj.GetCoordinates())
            {
                if (this.IsAdjacent(new Tuple<int, int>(c.Item1, c.Item2)))
                    return true;
            }
            return false;
        }

    }
}
