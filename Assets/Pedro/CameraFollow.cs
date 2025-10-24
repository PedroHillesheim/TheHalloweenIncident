using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    public Transform target;      
    public float smoothSpeed = 5f; 
    public Vector3 offset;        

    void LateUpdate()
    {
        if (target == null)
        {
            // Tenta achar o jogador automaticamente
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
            else
                return;
        }

        // Posição desejada com o offset
        Vector3 desiredPosition = target.position + offset;

        // Suaviza o movimento da câmera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Atualiza a posição da câmera
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
