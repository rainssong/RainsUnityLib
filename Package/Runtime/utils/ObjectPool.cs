using System.Collections.Generic;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;


/**
*   线程安全
new ObjectPool<ExampleObject>(() => new ExampleObject());
**/
namespace com.rainssong.utils
{
    public class ObjectPool<T>
    {
        private readonly ConcurrentBag<T> _objects;
        private readonly Func<T> _objectGenerator;

        public ObjectPool(Func<T> objectGenerator)
        {
            _objectGenerator = objectGenerator ?? throw new ArgumentNullException(nameof(objectGenerator));
            _objects = new ConcurrentBag<T>();
        }

        public T Get() => _objects.TryTake(out T item) ? item : _objectGenerator();

        public void Return(T item) => _objects.Add(item);
    }


    public class ObjectPoolManager<T>
    {
        private static readonly Dictionary<string,Object> poolDic=new Dictionary<string, Object>();

        public static ObjectPool<T> CreatePool(string poolName,Func<T> objectGenerator)
        {
            if(!poolDic.ContainsKey(poolName))
            {
                var p=new ObjectPool<T>(objectGenerator);
                poolDic.Add(poolName,p);
            }

            return (ObjectPool<T>)poolDic[poolName];
        }

        public static T Get(string poolName)
        {
            return (T) ((ObjectPool<T>)poolDic[poolName]).Get();
        }

        public static void Return(string poolName,T item)
        {
            ((ObjectPool<T>)poolDic[poolName]).Return(item);
        }
    }

   
}