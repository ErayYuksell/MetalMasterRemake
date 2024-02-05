using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEditor.PlayerSettings;
using TMPro;

public class ShatterControl : MonoBehaviour
{
    //mermi bu nesnelere carpinca listeden rastgele eleman dusurup daha sonra playerin collidera deyince conveyora gondermen gerek 
    [SerializeField] List<GameObject> shatterObjects = new List<GameObject>();
    int random;
    Vector3 newPos;
    [SerializeField] TextMeshProUGUI wallText;
    [SerializeField] int destroyValue;
    int destroyCount = 0;
    [SerializeField] Transform test;
    private void Start()
    {
        wallText.text = destroyValue.ToString();
    }
    int RandomListNumber()
    {
        random = Random.Range(1, shatterObjects.Count);
        return random;
    }
    Vector3 RandomPos()
    {
        //Vector3 pos = Random.insideUnitCircle * 5f; // 0 ve 1 arasinda rastgele sayi donduruyor onu 5 le carpip posisyona ekliyorum 
        Vector3 pos = new Vector3(Random.Range(-3, 3), Random.Range(0, 3), Random.Range(-2, 2));
        newPos = transform.position + pos;
        return newPos;

    }

    void MoveShatterPart()
    {
        var obj = shatterObjects[RandomListNumber()];
        shatterObjects.Remove(obj);
       
        obj.transform.DOMove(RandomPos(), 0.5f).OnComplete(() =>
        {
            //obj.GetComponent<Rigidbody>().useGravity = true;
            //DestroyWall();
            obj.transform.DOMove(new Vector3(transform.position.x - 5, 0, transform.position.z), 2).OnComplete(() =>
            {
                //obj.GetComponent<MeshCollider>().enabled = false;
                //obj.GetComponent<Rigidbody>().useGravity = false;
                //obj.GetComponent<Rigidbody>().velocity = new Vector3(obj.transform.position.x, obj.transform.position.y, 3); // conveyor ustune oturduklarinda parcalarin birbiriyle carpismayi birakmasi lazim
            });
        });
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LazerBullet"))
        {
            DestroyWall();
            var lazerBullet = other.GetComponent<BulletControl>();
            lazerBullet.gameObject.SetActive(false);
            MoveShatterPart();
        }
    }
    void DestroyWall()
    {
        destroyCount++;
        Debug.Log(destroyCount);
        DecreaseTextValue();
        if (destroyCount >= destroyValue)
        {
            foreach (var item in shatterObjects)
            {
                item.SetActive(false);
            }
            wallText.enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
    void DecreaseTextValue()
    {
        int value = destroyValue;
        value--;
        wallText.text = value.ToString();
    }

}
