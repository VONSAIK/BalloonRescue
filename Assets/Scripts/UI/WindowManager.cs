using System;
using System.Collections.Generic;
using UI.Windows;

//using UI.Windows;
using UnityEngine;

namespace UI
{
    public class WindowManager
    {
        private const string PrefabsFilePath = "Windows/";

        private static readonly Dictionary<Type, string> PrefabsDictionary = new Dictionary<Type, string>()
        {
            {typeof(LoadingWindow), "LoadingWindow"},
            {typeof(YouLoseWindow),"YouLoseWindow"},
            {typeof(YouWinWindow),"YouWinWindow"}
        };

        public static T ShowWindow<T>() where T : Window
        {
            var go = GetPrefabByType<T>();
            if (go == null)
            {
                Debug.LogError("Show window - object not found");
                return null;
            }

            return GameObject.Instantiate(go, GUIHolder);
        }

        private static T GetPrefabByType<T>() where T : Window
        {
            var prefabName = PrefabsDictionary[typeof(T)];
            if (string.IsNullOrEmpty(prefabName))
            {
                Debug.LogError("cant find prefab type of " + typeof(T) + "Do you added it in PrefabsDictionary?");
            }

            var path = PrefabsFilePath + PrefabsDictionary[typeof(T)];
            var window = Resources.Load<T>(path);
            if (window == null)
            {
                Debug.LogError("Cant find prefab at path " + path);
            }

            return window;
        }

        public static Transform GUIHolder
        {
            get { return ServiceLocator.Current.Get<GUIHolder>().transform; }
        }
    }
}