using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject bulletObject;
    Queue<GameObject> queue = new Queue<GameObject>();
    [SerializeField] int objectSize = 15;
    private void Start()
    {
        CreateObjectPool();
    }
    public void CreateObjectPool()
    {
        for (int i = 0; i < objectSize; i++)
        {
            var obj = Instantiate(bulletObject, transform);
            obj.SetActive(false);

            queue.Enqueue(obj);
        }
    }
    public GameObject GetObjectPool()
    {
        foreach (var item in queue) // siranin ilk elamani false ise ac ve siranin sonuna ekle range i gecince false olucak zaten siraya geri ekledigim icin yine ayni sekilde donguye girerek ilerleyecek
        {
            if (!item.activeSelf)
            {
                item.SetActive(true);
                queue.Enqueue(item);
                return item;
            }
        }
        var obj = Instantiate(bulletObject, transform);
        obj.SetActive(false);

        queue.Enqueue(obj);

        return obj;

    }
}
