using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateInstance : MonoBehaviour
{
    //public SceneState[] scenes;
    public List<SceneState> scenes;
    public bool isOnboarding;

    public List<Photo> Photos; 

    [Serializable]
    public class Photo
    {
        public string ImagePath;
        public string ImageIndicatorPath;
        public string MetadataPath;
        public string AreaName;
        public string SceneName;
        public List<string> Tags;
    }

    [Serializable]
    public class SceneState
    {
        //public AreaState[] areas;
        public string sceneName;
        public List<AreaState> areas;
        public SceneState(string sceneName)
        {
            this.sceneName = sceneName;
            areas = new List<AreaState>();
        }
        public AreaState makeAreaState(string areaName)
        {
            var area = new AreaState(areaName);
            areas.Add(area);
            return area;
        }
        public AreaState getAreaState(string areaName)
        {
            foreach (var area in areas)
            {
                if (area.areaName == areaName)
                {
                    return area;
                }
            }
            return makeAreaState(areaName);
        }
        public int CapturedAreaCount
        {
            get
            {
                int count = 0;
                foreach(var area in areas)
                {
                    if (area.isCaptured)
                    {
                        count++;
                    }
                }
                return count;
            }
        }
    }

    [Serializable]
    public class AreaState
    {
        public string areaName;
        public bool isCaptured;
        public AreaState(string areaName)
        {
            this.areaName = areaName;
            isCaptured = false;
        }
    }

    public SceneState makeSceneState(string sceneName)
    {
        var scene = new SceneState(sceneName);
        scenes.Add(scene);
        return scene;
    }
    public SceneState getSceneState()
    {
        return getSceneState(SceneName);
    }
    public SceneState getSceneState(string sceneName)
    {
        foreach (var scene in scenes)
        {
            if (scene.sceneName == sceneName)
            {
                return scene;
            }
        }
        return makeSceneState(sceneName);
    }

    public string SceneName
    {
        get
        {
            return SceneManager.GetActiveScene().name;
        }
    }

    public bool GetIsCaptured(string areaName)
    {
        return GetIsCaptured(SceneName, areaName);
    }
    public bool GetIsCaptured(string sceneName, string areaName)
    {
        var scene = getSceneState(sceneName);
        var area = scene.getAreaState(areaName);
        return area.isCaptured;
    }

    public void SetIsCaptured(string areaName, bool isCaptured)
    {
        SetIsCaptured(SceneName, areaName, isCaptured);
    }
    public void SetIsCaptured(string sceneName, string areaName, bool isCaptured)
    {
        var scene = getSceneState(sceneName);
        var area = scene.getAreaState(areaName);
        area.isCaptured = isCaptured;
    }

    public int getTagCount(string tag)
    {
        int count = 0;
        foreach(var photo in Photos)
        {
            if (photo.Tags.Contains(tag))
            {
                count++;
            }
        }
        return count;
    }
}
