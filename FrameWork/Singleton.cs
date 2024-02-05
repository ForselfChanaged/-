
    public class Singleton<T> where T : new()
    {
        protected Singleton()
        {
            Init();
        }
        private static T instance =new T();//饿汉模式
        public static T Instance
        {
            get
            {
                return instance;
            }
        }
        protected virtual  void Init()
        {

        }
    }



