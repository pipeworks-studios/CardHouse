using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardHouse
{
    public static class Utils
    {
        public static float CorrectAngle(float angle) // yields an angle beween -180 and 180
        {
            while (angle < 0)
            {
                angle += 360;
            }
            while (angle > 360)
            {
                angle -= 360;
            }
            return angle;
        }

        public static T CopyComponent<T>(T original, GameObject destination) where T : Component
        {
            System.Type type = original.GetType();
            Component copy = destination.AddComponent(type);
            System.Reflection.FieldInfo[] fields = type.GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                field.SetValue(copy, field.GetValue(original));
            }
            return copy as T;
        }

        public static T GetComponentForName<T>(this IEnumerable<T> list, string name) where T : Component
        {
            return list.FirstOrDefault(x => x.gameObject.name == name);
        }

        public static T GetComponentForName<T>(this IEnumerable<T> list, string name, Type searchType) where T : Component
        {
            return list.FirstOrDefault(x => x.gameObject.name == name && x.GetType() == searchType);
        }
    }
}