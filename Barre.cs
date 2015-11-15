using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Barre
    {
        public List<int> emplacement = new List<int>() ;
        public bool bloquer;

        public Barre()
        {
            emplacement.Add(4);
            emplacement.Add(5);
            emplacement.Add(6);
            bloquer = false;
        }


    }
}
