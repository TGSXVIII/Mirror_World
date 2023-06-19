using UnityEngine;

public class SaveProfile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract record SaveProfileData { }

    public record PlayerSaveData : SaveProfileData
    {
        public Vector2 position;
    }
}
