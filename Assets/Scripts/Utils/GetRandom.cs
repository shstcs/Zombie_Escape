using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRandom : MonoBehaviour
{
    public static Vector3 GetRandomPos()
    {
        float x = Random.Range(-50.0f, 50.0f);
        float z = Random.Range(-50.0f, 50.0f);
        Vector3 pos = new Vector3(x, 0.5f, z);

        return pos;
    }
}
