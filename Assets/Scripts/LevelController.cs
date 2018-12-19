using UnityEngine;

public class LevelController : MonoBehaviour {
    // serialize for debugging purposes
    [SerializeField] int breakableBlocks;

    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void AddBlockToGlobalBlockCount()
    {
        breakableBlocks++;
    }

    public void DecrementBlockCount()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
