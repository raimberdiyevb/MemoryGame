using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project3_MemoryGame
{
    internal class Player
    {
        static string nm;
        static int tm;
        static int nol;
        static int not;
        static int bz;

        public static string name
        {
            get { return nm; }
            set { nm = value; }
        }

        public static int time
        {
            get { return tm; }
            set { tm = value; }
        }

        public static int BoardSize
        {
            get { return bz; }
            set { bz = value; }
        }

        public static int NumberofLives
        {
            get { return nol; }
            set { nol = value; }
        }

        public static int NumberofTiles
        {
            get { return not; }
            set { not = value; }
        }

    }
}
