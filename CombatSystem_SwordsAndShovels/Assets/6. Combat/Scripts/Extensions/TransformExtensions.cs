using UnityEngine;

public static class TransformExtensions 
{
    private const float DOT_THREASHOLD = 0.5f;

    public static bool IsFacingTarget(this Transform transform, Transform targetTransform)
    {
        /*
         *  Dot Product returns values between 1 and -1
         *  If value = 1, both are facing the same direction
         *  -1, facing completely opposite direction
         *  0, if two vectors are diagonal. If they form a right-angle (90 degrees) to each other.         
         */
        var vectorToTarget = targetTransform.position - transform.position;

        //ensure the vector is normalized
        vectorToTarget.Normalize();

        //perform dot product operation
        var dot = Vector3.Dot(transform.forward, vectorToTarget);

        return dot > DOT_THREASHOLD;
    }
}
