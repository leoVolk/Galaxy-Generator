using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiral : MonoBehaviour
{
    public static readonly int iterations = 450;
    public static readonly float addedRotation = 1f;
    public static readonly float devidedPosition = 2;

    //TODO: OPTIMIZE JESUS CHRIST
    public static List<Vector3> GenerateSpiral(){

        List<Vector3> positions = new List<Vector3>();

        //right
        for (int i = 0; i < iterations; i++)
        {
            GameObject g = new GameObject();
            g.transform.rotation = Quaternion.Euler(new Vector3(0, addedRotation * i,0));
            g.transform.position = ((g.transform.forward + g.transform.right) * i)/devidedPosition;

            positions.Add(g.transform.position);

            Destroy(g.gameObject);
        }

        //left
        for (int i = 0; i < iterations; i++)
        {
            GameObject g = new GameObject();
            g.transform.rotation = Quaternion.Euler(new Vector3(0, addedRotation * i,0));
            g.transform.position = ((-g.transform.forward + -g.transform.right) * i) /devidedPosition;

            positions.Add(g.transform.position);

            Destroy(g.gameObject);
        }

        return positions;
    }
}
