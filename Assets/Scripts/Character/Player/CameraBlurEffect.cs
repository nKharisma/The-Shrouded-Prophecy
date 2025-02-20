using UnityEngine;
using System.Collections.Generic;

public class CameraBlurEffect : MonoBehaviour
{
    public Transform player;
    public LayerMask buildingLayer;
    public Shader blurShader;
    float blurDistance = 5f;

    private List<Renderer> blurredBuildings = new List<Renderer>();
    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();

    void Update()
    {
        HandleBuildingBlur();
    }

    void HandleBuildingBlur()
    {
        // reset blurred buildings
        foreach(var building in blurredBuildings)
        {
            if(building != null && originalMaterials.ContainsKey(building))
            {
                building.materials = originalMaterials[building];
            }
        }
        blurredBuildings.Clear();

        
        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distance + blurDistance, buildingLayer);

        foreach(var hit in hits)
        {
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            if(renderer != null && !blurredBuildings.Contains(renderer))
            {
                // Save original materials if not already saved
                if (!originalMaterials.ContainsKey(renderer))
                {
                    originalMaterials[renderer] = renderer.materials;
                }

                // Apply blur shader
                Material[] newMaterials = new Material[renderer.materials.Length];
                for(int i = 0; i < renderer.materials.Length; i++)
                {
                    newMaterials[i] = new Material(blurShader);
                    newMaterials[i].SetTexture("_MainTex", renderer.materials[i].mainTexture);
                }
                renderer.materials = newMaterials;
                blurredBuildings.Add(renderer);
            }
        }
    }
}
