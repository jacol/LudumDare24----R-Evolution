using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _R_Evolution.GameObjects.Wall;

namespace _R_Evolution.GameMechs
{
    static class LabRevolutionMaker
    {
        private static readonly Random RandomGenerator;

        static LabRevolutionMaker()
        {
            RandomGenerator = new Random((int)DateTime.Now.Ticks);
        }

        internal static void Rebuild(List<Wall> lab)
        {
            for(int i =0 ;i<lab.Count;i++)
            {
                if(RandomGenerator.Next() % 5 == 0)
                {
                    lab[i].ChangeOrientation();
                    lab[i].RecalculateArea();
                }
            }
        }
    }
}
