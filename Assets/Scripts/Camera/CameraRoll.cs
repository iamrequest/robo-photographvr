using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CameraRoll : MonoBehaviour {
    [AssetsOnly]
    public GameObject photoPrefab;
    public List<Photo> photos;

    public void AddPhoto(RenderTexture rt) {
        GameObject newPhotoGameObject = Instantiate(photoPrefab, transform.position, transform.rotation);
        Photo newPhoto = newPhotoGameObject.GetComponent<Photo>();

        newPhoto.texture = rt;
        newPhoto.image.texture = newPhoto.texture;
        newPhoto.timestamp = System.DateTime.Now;
    }
}
