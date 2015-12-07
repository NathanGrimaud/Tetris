using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{

    public class Comparator : IComparer // cette classe hérite de IComparer.
                                         // c'est cette classe qui est appelée par 
                                         // les méthodes de tri pour savoir comment 
                                         // comparer les éléments
    {
        public int Compare(object x, object y) // la méthode de comparaison : 
                                               // elle est forcément déclarée comme cela
        {
            if ((int)x > (int)y) // bien penser à caster les objet dans la structure qui va être comparée
                return -1;
            if ((int)x < (int)y)
                return 1;
            return 0; // eh oui, si les éléments sont égaux on ne va pas s'amuser à les trier, 
                      // donc on retournera 0 :-D
        }
    }
}
