using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TagGet
{
    public class TagUtility : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public static string getParentTagName(string name) {
            int pos = name.IndexOf("/");
    
            if (0 < pos) {
                return name.Substring(0, pos);
            } else {
                return name;
            }
        }
    
        public static string getParentTagName(GameObject gameObject) {
            string name = gameObject.tag;
            int pos = name.IndexOf("/");
    
            if (0 < pos) {
                return name.Substring(0, pos);
            } else {
                return name;
            }
        }
    
        public static string getChildTagName(string name) {
            int pos = name.IndexOf("/");
    
            if (0 < pos) {
                return name.Substring(pos + 1);
            } else {
                return name;
            }
        }
    
        public static string getChildTagName(GameObject gameObject) {
            string name = gameObject.tag;
            int pos = name.IndexOf("/");
    
            if (0 < pos) {
                return name.Substring(pos + 1);
            } else {
                return name;
            }
        }
                
        public static GameObject[] getParentTagObjects(string name) {
            List<GameObject> gameObjects = new List<GameObject>();
    
            foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>()) {
                if (getParentTagName(obj.tag) == name) {
                    gameObjects.Add(obj);
                }
            }
    
            return gameObjects.ToArray();
        }

        public static GameObject[] getChildTagObjects(string name) {
            List<GameObject> gameObjects = new List<GameObject>();
 
            foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>()) {
                if (getChildTagName(obj.tag) == name) {
                    gameObjects.Add(obj);
                }
            }
 
            return gameObjects.ToArray();
        }
    }
}