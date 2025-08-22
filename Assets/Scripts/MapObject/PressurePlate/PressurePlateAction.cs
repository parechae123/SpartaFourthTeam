using UnityEngine;

public class PressurePlateAction : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private PlayerController controller;
    [SerializeField] private PressurePlate press;
    [SerializeField] private Animator targetAnimator;
    [SerializeField] private string pressedBoolName = "Pressed";
    [SerializeField] private Transform cubeSpawnPoint;
    [SerializeField] private GameObject cubePrefab;

    private GameObject currentCube;

    void Awake()
    {
        if (press == null)
            press = GetComponent<PressurePlate>();

        if (press != null)
            press.OnValueChanged += HandlePlateChanged;
        else
            Debug.LogWarning("PressurePlateAction: press가 연결되지 않았습니다.");
    }

    void OnDestroy()
    {
        if (press != null)
            press.OnValueChanged -= HandlePlateChanged;
    }

    private void HandlePlateChanged(bool pressed)
    {
        if (targetAnimator)
            targetAnimator.SetBool(pressedBoolName, pressed);

        if (pressed)
            SpawnOrRespawnCube();
    }

    private void SpawnOrRespawnCube()
    {
        if (cubePrefab == null || !cubeSpawnPoint)
        {
            Debug.LogWarning("PressurePlateAction: cubePrefab 또는 cubeSpawnPoint가 비어있습니다.");
            return;
        }

        if (currentCube != null)
        {
            controller.DropGrabable(currentCube.GetComponent<IGrabable>());
            Destroy(currentCube);
        }



        currentCube = Instantiate(cubePrefab, cubeSpawnPoint.position, cubeSpawnPoint.rotation);
        currentCube.transform.SetParent(cubeSpawnPoint, true);
    }
}
