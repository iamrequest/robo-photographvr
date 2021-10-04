using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using TMPro;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core;

/// <summary>
/// Paginate the whole camera roll, showing a few at a time
/// </summary>
public class PhotoBoard : MonoBehaviour {
    public CameraRoll cameraRoll;
    public TextMeshProUGUI pageNumText;

    [AssetsOnly]
    public GameObject photoSocketPrefab;
    public List<HVRSocket> photoSockets;

    public Vector3 globalOffset;
    [Range(1, 10)]
    public int rowCount, colCount;
    [Range(0f, 5f)]
    public float width, height;
    public int currentPage, maxPages;

    [Range(0f, 5f)]
    public float debugSphereRadius;



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

    private void Start() {
        UpdatePageText();
    }

    private void OnCameraRollUpdated() {
        RenderPage();
    }


    // ================================================================================
    // Edit mode stuff
    // ================================================================================
    [HideInPlayMode]
    [Button]
    private void InitializeSockets() {
        foreach (HVRSocket socket in photoSockets) {
            DestroyImmediate(socket.gameObject);
        }
        photoSockets.Clear();


        GameObject newPhotoSocket;
        for (int r = 0; r < rowCount; r++) {
            for (int c = 0; c < colCount; c++) {
                newPhotoSocket = Instantiate(photoSocketPrefab, transform);
                newPhotoSocket.transform.localPosition = GetPositionOffset(r, c);

                if(newPhotoSocket.TryGetComponent(out HVRSocket socket)) {
                    photoSockets.Add(socket);
                }
            }
        }
    }

    // TODO: This isn't perfect, but it's close enough
    private Vector3 GetPositionOffset(int row, int col) {
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

                Gizmos.color = Color.Lerp(Color.green,
                    Color.red,
                    (float) currentIteration / (float)iterations);
                Gizmos.DrawWireSphere(transform.position + transform.rotation * GetPositionOffset(r, c), debugSphereRadius);
            }
        }
    }


    // ================================================================================
    // Rendering pages
    // ================================================================================
    [Button]
    public void RenderPage() {
        int photoIndex = currentPage * rowCount * colCount;
        int socketIndex = 0;

        // Release all sockets, disable all photos
        foreach (HVRSocket socket in photoSockets) {
            HVRGrabbable grabbable = socket.GrabbedTarget;
            if (grabbable != null) {
                grabbable.ForceRelease();
                grabbable.gameObject.SetActive(false);
            }
        }


        for (int r = 0; r < rowCount; r++) {
            for (int c = 0; c < colCount; c++) {
                if (cameraRoll.photos.Count <= photoIndex) {
                    return;
                }

                // TODO: Check if the photo is being grabbed first
                Photo currentPhoto = cameraRoll.photos[photoIndex];
                currentPhoto.gameObject.SetActive(true);
                photoSockets[socketIndex].TryGrab(currentPhoto.hvrGrabbable, true, false);

                photoIndex++;
                socketIndex++;
            }
        }
    }

    [Button]
    public void NextPage() {
        currentPage = Mathf.Clamp(currentPage + 1, 0, maxPages);
        RenderPage();

        UpdatePageText();
    }

    [Button]
    public void PrevPage() {
        currentPage = Mathf.Clamp(currentPage - 1, 0, maxPages);
        RenderPage();

        UpdatePageText();
    }

    public void UpdatePageText() {
        pageNumText.text = $"Page {currentPage + 1}";
    }
}
