using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading;
using System.Windows.Input;



namespace Snake
{
    class Input
    {

        private static Hashtable keys = new Hashtable();

        public static bool Pressed(Key key)
        {
            if (keys[key] == null)
                return false;

            return (bool)keys[key];
        }

        public static bool StateOfKey(Key key, bool state)
        {
            keys[key] = state;
            return state;
        }

    }
}
