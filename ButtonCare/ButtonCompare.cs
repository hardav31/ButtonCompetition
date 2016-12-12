﻿using System;
using System.Windows.Forms;

namespace ButtonCare
{
    class ButtonCompare:Button,IComparable
    {
        public int CompareTo(object obj)
        {
            ButtonCompare temp = (ButtonCompare)obj;
            if (this.Location.X > temp.Location.X)
                return -1;
            if (this.Location.X < temp.Location.X)
                return 1;
            else
                return 0;
            
        }
    }
}
