using System;
namespace QLib
{
    public class Singleton<T> where T : Singleton<T>
    {
        public Singleton()
        {
            if ( Instance != null )
                throw new Exception("Singleton class instantiated again!");
            Instance = this as T;
        }
        public static T Instance { get; private set; }
    }
}
