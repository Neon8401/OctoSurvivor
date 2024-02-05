using UnityEngine;

public class RoundLight : SingletonMonoBehaviourInSceneBase<RoundLight>
{
    public Light Light { get; private set; }

    private void Start()
    {
        Light = GetComponent<Light>();
    }

    public void RotateTo(Vector3 to)
    {
        transform.eulerAngles = to;
    }
}