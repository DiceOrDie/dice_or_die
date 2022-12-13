using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    public int level_ = 1;
    [SerializeField] List<GameObject> level_mask;
    void Awake() {
        foreach(var mask in level_mask) {
            mask.SetActive(true);
        }
    }
    public void NextLevel() {
        level_mask[level_-1].SetActive(false);
        level_++;
    }
}
