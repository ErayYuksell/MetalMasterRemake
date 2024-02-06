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
    private void Update()
    {

    }
    
    int RandomListNumber()
    {
        random = Random.Range(1, shatterObjects.Count);
        return random;
    }
    Vector3 RandomPos()
    {
        //Vector3 pos = Random.insideUnitCircle * 5f; // 0 ve 1 arasinda rastgele sayi donduruyor onu 5 le carpip posisyona ekliyorum 
        Vector3 pos = new Vector3(Random.Range(transform.position.x - 2, transform.position.x + 2), 0, Random.Range(transform.position.z - 3, transform.position.z - 5));
        //newPos = transform.position + pos;
        return pos;
    }

    void MoveShatterPart()
    {
        var obj = shatterObjects[RandomListNumber()];
        shatterObjects.Remove(obj);

        obj.transform.DOJump(RandomPos(), 3, 1, 0.5f).OnComplete(() =>
        {
            obj.transform.DOMove(new Vector3(-11, 0, transform.position.z + 3), 2).OnComplete(() =>
            {
                var shatterPart = obj.GetComponent<ShatterPart>();
                shatterPart.canMoveShatter = true;
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
