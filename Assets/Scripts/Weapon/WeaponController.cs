using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Transform _player;

    void Update()
    {

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;

        Vector2 direction = (mouseWorldPosition - _player.position);
     
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90.0f);

        //Debug.Log($"Mouse: {mouseWorldPosition}, Player: {player.position}, Direction: {direction}, Angle: {angle}");
    }

    public void SetPlayer(Transform player)
    {
        _player = player;
    }
}
