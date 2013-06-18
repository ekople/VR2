using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace PiimaToostusService
{
    public class Utils
    {
        //krüptograafiline juhuarvu genereerimine (genereerib 40 arvu pikkuse stringi)
        public static string GenCryptoRndStr()
        {
            string tulem = string.Empty;
            using (var rng = new RNGCryptoServiceProvider())
            {
                //Int64 suurune buffer random numbri jaoks
                var randomNumber = new byte[8];
                var tmpKeyStrB = new StringBuilder();
                while (tmpKeyStrB.Length < 40)
                {
                    //täida buffer
                    rng.GetBytes(randomNumber);
                    //väärtusta string töödeldavate arvudega
                    tmpKeyStrB.Append(Math.Abs(BitConverter.ToInt64(randomNumber, 0)).ToString());
                }

                //võta arvu jadast esimesed 40 arvu ning määra need tulemisse
                for (int i = 0; i < 40; i++)
                {
                    tulem += tmpKeyStrB[i];
                }
            }
            return tulem;
        }

        //wrapper meetod objektide kopeerimise meetodile
        public static T CopyTo<T>(object o, T toCopy) where T : class
        {
            return ObjectCopyTo(o, toCopy) as T;
        }

        //kopeerimaks andmeid ühest andmetüübist teise
        public static object ObjectCopyTo(object o, object toCopy)
        {
            //nulltingimuste kontrollid
            if (o == null) return null;
            if (toCopy == null) return null;
            //kopeerime propertid
            foreach (PropertyInfo property in o.GetType().GetProperties())
            {
                FieldInfo resultField = toCopy.GetType().GetField(property.Name);
                if (resultField == null)
                { continue; }
                //täidame rekursiivselt klassid
                if (property.PropertyType.IsClass && !property.PropertyType.Equals(typeof(string)))
                {
                    var objectToCopy =resultField.FieldType.GetConstructor(new Type[] { }).Invoke(null);
                    objectToCopy = CopyTo(property.GetValue(o,null), objectToCopy);
                    resultField.SetValue(toCopy,objectToCopy);
                }
                // täidame ülejäänud propertid
                else
                { resultField.SetValue(toCopy, property.GetValue(o,null)); }
            }
            //kopeerime fieldid
            foreach (FieldInfo fieldInfo in o.GetType().GetFields())
            {
                PropertyInfo resultProperty = toCopy.GetType().GetProperty(fieldInfo.Name);
                if (resultProperty == null)
                { continue; }
                //täidame rekursiivselt klassid
                if (fieldInfo.FieldType.IsClass && !fieldInfo.FieldType.Equals(typeof(string)))
                {
                    var objectToCopy = resultProperty.PropertyType.GetConstructor(new Type[] { }).Invoke(null);
                    objectToCopy = CopyTo(fieldInfo.GetValue(o), objectToCopy);
                    resultProperty.SetValue(toCopy, objectToCopy,null);
                }
                // täidame ülejäänud fieldid
                else
                { resultProperty.SetValue(toCopy, fieldInfo.GetValue(o),null); }
            }

            //tagastame koopia
            return toCopy;
        }

        public static IList<T> ConvertToType<T>(IList sisendList) where T : class, new()
        {
            if (ArrIsNullOrEmpty(sisendList))
            {
                return null;
            }
            List<T> tulem = new List<T>(sisendList.Count);

            for (int i = 0; i < sisendList.Count; i++)
            {
                if (!(sisendList[i] is T))
                {
                    var ElemToAdd = new T();
                    ElemToAdd = CopyTo(sisendList[i], ElemToAdd) as T;
                    tulem.Add(ElemToAdd as T);
                }
                else
                { tulem.Add(sisendList[i] as T); }
            }
            return tulem;
        }

        public static bool ArrIsNullOrEmpty(IList list)
        {
            return (list == null || list.Count == 0);
        }

        public static bool IsNullOrEmptyWhitespace(string sisendStr)
        {
            if (string.IsNullOrEmpty(sisendStr) || string.IsNullOrWhiteSpace(sisendStr))
            {
                return true;
            }
            return false;
        }

        public static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }
            return true;
        }
    }
}