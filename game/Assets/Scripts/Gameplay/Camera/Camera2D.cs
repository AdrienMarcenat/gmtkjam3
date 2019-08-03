using UnityEngine;

public class Camera2D : MonoBehaviour
{
    void Start()
    {
        TileCoordinates levelDimension = LevelManagerProxy.Get().GetLevelDimension();
        transform.position = new Vector3(levelDimension.x.ToWorldUnit() / 2, levelDimension.y.ToWorldUnit() / 2, transform.position.z);
    }
}
