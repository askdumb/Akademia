﻿using System.Collections;
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

        public static void StateOfKey(Key key, bool state)
        {
            keys[key] = state;
        }

    }
}
