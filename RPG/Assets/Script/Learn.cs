using UnityEngine;
using System.Collections;


public class Learn : MonoBehaviour
{
    public int i;
    public Transform cube;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 pos = new Vector3(i,i,i);
            Instantiate(cube, pos, Quaternion.identity);
        }
        StartCoroutine(Test());
    }
    public IEnumerator Test()
    {
        print("123");
        yield return new WaitForSeconds(2);
        print("321");
        yield return new WaitForSeconds(2);
        for (int i = 0; i < 10; i++)
        {
            Vector3 pos = new Vector3(i,0,1);
            Instantiate(cube, pos, Quaternion.identity);
            
        }
    }

    
}
