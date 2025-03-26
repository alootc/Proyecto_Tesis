using UnityEngine;

public class AcidDecalSpawner : MonoBehaviour
{
    public GameObject decalPrefab; // Prefab del decal con Decal Projector
    public float spawnInterval = 0.1f; // Intervalo de tiempo entre cada spawn de decal

    private float lastSpawnTime = 0f;

    public void SpawnAcidDecal(Vector3 contactPosition)
    {
        // Verifica si ha pasado el tiempo suficiente desde el �ltimo spawn
        if (Time.time - lastSpawnTime >= spawnInterval)
        {
            // Instancia el decal en la posici�n de contacto
            GameObject decal = Instantiate(decalPrefab, contactPosition, Quaternion.identity);

            // Aseg�rate de que el decal est� correctamente orientado hacia la superficie
            decal.transform.forward = -decal.transform.up; // Ajusta seg�n la orientaci�n de tu prefab

            lastSpawnTime = Time.time; // Actualiza el tiempo del �ltimo spawn
        }
    }
}