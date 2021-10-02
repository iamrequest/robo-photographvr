using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

/// <summary>
/// Paginate the whole camera roll, showing a few at a time
/// </summary>
public class PhotoBoard : MonoBehaviour {
    public CameraRoll cameraRoll;

    public Vector3 globalOffset;
    [Range(1, 10)]
    public int rowCount, colCount;
    [Range(0f, 5f)]
    public float width, height;

    public int currentPage;

    [Range(0f, 5f)]
    public float debugSphereRadius;
    [Range(0, 10)]
    public int renderLimit;



    private void Awake() {
        cameraRoll = GetComponent<CameraRoll>();
    }

    private void OnEnable() {
        cameraRoll.onPhotoAdded.AddListener(OnCameraRollUpdated);
        cameraRoll.onPhotoRemoved.AddListener(OnCameraRollUpdated);
    }
    private void OnDisable() {
        cameraRoll.onPhotoAdded.RemoveListener(OnCameraRollUpdated);
        cameraRoll.onPhotoRemoved.RemoveListener(OnCameraRollUpdated);
    }

    private void OnCameraRollUpdated() {
        RenderPage();
    }

    [Button]
    public void RenderPage() {
        int photoIndex = currentPage * rowCount * colCount;

        for (int r = 0; r < rowCount; r++) {
            for (int c = 0; c < colCount; c++) {
                if (cameraRoll.photos.Count <= photoIndex) {
                    return;
                }

                // TODO: Check if the photo is being grabbed first
                Photo currentPhoto = cameraRoll.photos[photoIndex];

                // TODO: Apply transform rotation
                currentPhoto.transform.position = transform.position + GetPositionOffset(r, c);

                photoIndex++;
            }
        }
    }

    // TODO: This isn't perfect, but it's close enough
    public Vector3 GetPositionOffset(int row, int col) {
        Vector3 offset = new Vector3(width / (colCount + 1) - width/2, 
            height / (rowCount + 1),
            0f);


        offset.x += (float) col * (width / colCount);
        offset.y -= (float) row * (height / rowCount);

        offset += globalOffset;
        return offset;
    }

    private void OnDrawGizmosSelected() {
        int iterations = rowCount * colCount;
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.DrawLine(-Vector3.right * width / 2, Vector3.right * width/2);
        Gizmos.DrawLine(-Vector3.up * height / 2, Vector3.up * height / 2);
        Gizmos.matrix = Matrix4x4.identity;


        int currentIteration = 0;
        for (int r = 0; r < rowCount; r++) {
            for (int c = 0; c < colCount ; c++) {
                currentIteration++;

                if (currentIteration <= renderLimit) {
                    Gizmos.color = Color.Lerp(Color.green,
                        Color.red,
                        (float) currentIteration / (float)iterations);
                    Gizmos.DrawWireSphere(transform.position + GetPositionOffset(r, c), debugSphereRadius);
                }
            }
        }
    }
}
