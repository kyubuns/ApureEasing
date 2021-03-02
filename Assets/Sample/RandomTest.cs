using UnityEngine;

public class RandomTest
{
    public static Vector3 OnUnitSphere
    {
        get
        {
            var a = Random.onUnitSphere;
            Debug.Log(a);
            return a;
        }
    }
}
