﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    internal class Program
    {
        static void Main()
        {
            while (GameManager.HandleGameFlow() == 1)
            {
                Ex02.ConsoleUtils.Screen.Clear();
            };
        }
    }
}
