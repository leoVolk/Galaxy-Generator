using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiral : MonoBehaviour
{
    public static int iterations = 256;

    //TODO: OPTIMIZE JESUS CHRIST
    public static List<Vector3> GenerateSpiral(){

        List<Vector3> positions = new List<Vector3>();

        //right
        for (int i = 0; i < iterations; i++)
        {
            GameObject g = new GameObject();
            g.transform.rotation = Quaternion.Euler(new Vector3(0, (360/iterations)  * i,0));
            g.transform.position = (g.transform.forward + g.transform.right) * i;

            positions.Add(g.transform.position);

            Destroy(g.gameObject);
        }
        //left
        for (int i = 0; i < iterations; i++)
        {
            GameObject g = new GameObject();
            g.transform.rotation = Quaternion.Euler(new Vector3(0, (360/iterations)  * i,0));
            g.transform.position = (-g.transform.forward + -g.transform.right) * i;

            positions.Add(g.transform.position);

            Destroy(g.gameObject);
        }

        return positions;
    }
}
