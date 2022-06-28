using UnityEngine;

namespace SF.Common.Helpers
{
    public static class MathHelper
    {
        public static Vector3 GetAvg(Vector3[] sample)
        {
            var avg = Vector3.zero;

            for (var i = 0; i < sample.Length; i++)
            {
                avg += sample[i];
            }

            return avg / sample.Length;
        }
    }
}