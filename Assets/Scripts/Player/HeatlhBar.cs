
using UnityEngine;

public class HeatlhBar : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform.position);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

}
