using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class CameraRoll : MonoBehaviour {
    [AssetsOnly]
    public GameObject photoPrefab;
    public List<Photo> photos;
    public UnityEvent onPhotoAdded, onPhotoRemoved;
    [Range(20, 150)]
    public int maxCapacity;

    public void AddPhoto(RenderTexture rt) {
        if (photos.Count >= maxCapacity) return;

        GameObject newPhotoGameObject = Instantiate(photoPrefab, transform.position, transform.rotation);
        Photo newPhoto = newPhotoGameObject.GetComponent<Photo>();

        newPhoto.texture = rt;
        newPhoto.image.texture = newPhoto.texture;
        newPhoto.timestamp = System.DateTime.Now;
        newPhoto.sourceCameraRoll = this;

        photos.Add(newPhoto);
        onPhotoAdded.Invoke();
    }
    public void AddPhoto(Photo photo) {
        if (photos.Count >= maxCapacity) return;

        photos.Add(photo);
        photo.sourceCameraRoll = this;

        onPhotoAdded.Invoke();
    }

    public void RemoveFromRoll(Photo photo) {
        photos.Remove(photo);

        onPhotoRemoved.Invoke();
    }
}
