using System.Collections.Generic;
using UnityEngine;

namespace DevourDev.Unity.Utils
{
    public static class RandomHelpers
    {
        private static readonly System.Random _rndInst = new();


        public static Vector2 PointOnCircle(System.Random rndInst)
        {
            double angle = rndInst.NextDouble() * System.Math.PI * 2;
            Vector2 p;
            p.x = (float)System.Math.Cos(angle);
            p.y = (float)System.Math.Sin(angle);
            return p;
        }


        public static T RandomElement<T>(IReadOnlyList<T> collection)
        {
            return RandomElement(collection, _rndInst);
        }

        public static T RandomElement<T>(IReadOnlyList<T> collection, System.Random rng)
        {
            return collection[RandomIndex(collection, rng)];
        }

        public static int RandomIndex<T>(IReadOnlyCollection<T> collection)
        {
            return RandomIndex(collection, _rndInst);
        }

        public static int RandomIndex<T>(IReadOnlyCollection<T> collection, System.Random rng)
        {
            return rng.Next(collection.Count);
        }
        public static float NextFloat(float min, float max, System.Random rnd)
        {
            return NextFloat(rnd) * (max - min) + min;
        }
        public static float NextFloat(float min, float max, System.Random rnd, UnityEngine.AnimationCurve curve)
        {
            float mantissa = NextFloat(rnd, curve);
            return (max - min) * mantissa + min;
        }


        public static float NextFloat(System.Random rnd, UnityEngine.AnimationCurve curve)
        {
            return curve.Evaluate(NextFloat(rnd));
        }

        public static float NextFloat(System.Random rnd)
        {
            return (float)rnd.NextDouble();
        }

        public static float NextFloat()
        {
            return NextFloat(_rndInst);
        }

        public static float NextFloat(UnityEngine.AnimationCurve curve)
        {
            return NextFloat(_rndInst, curve);
        }
    }
}
