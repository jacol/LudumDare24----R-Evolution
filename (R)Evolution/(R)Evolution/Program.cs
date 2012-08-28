using System;

namespace _R_Evolution
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Revolution game = new Revolution())
            {
                game.Run();
            }
        }
    }
#endif
}

