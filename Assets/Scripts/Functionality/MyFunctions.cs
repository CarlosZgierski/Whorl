
using UnityEngine;

static public class MyFunctions 
{
   public static void LookAtOnlyYAxis(Transform looker, Vector3 targetPosition)
    {
        Vector3 targetPostition = new Vector3(targetPosition.x, looker.position.y, targetPosition.z);
        looker.LookAt(targetPostition);

   }

    public static float Distance(Vector3 x, Vector3 y)
    {
        return (x - y).sqrMagnitude;
    }
}
