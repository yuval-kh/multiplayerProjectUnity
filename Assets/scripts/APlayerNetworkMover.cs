using Photon.Pun;
using UnityEngine;
using System.Collections;


public class APlayerNetworkMover : MonoBehaviourPunCallbacks, IPunObservable {

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject cameraObject;
    [SerializeField]
    private GameObject gunObject;
    [SerializeField]
    private GameObject playerObject;

    private Vector3 position;
    private Quaternion rotation;
    private bool jump;
    private float smoothing = 10.0f;

    /// <summary>
    /// Move game objects to another layer.
    /// </summary>
    void MoveToLayer(GameObject gameObject, int layer) {
        gameObject.layer = layer;
        foreach(Transform child in gameObject.transform) {
            MoveToLayer(child.gameObject, layer);
        }
    }


    // Awake is called when the script instance is being loaded.
    void Awake() {
        // FirstPersonController script require cameraObject to be active in its Start function.
        // sets the camera on only to my player: to all the other players the camera is not active.
        if (photonView.IsMine) {
            cameraObject.SetActive(true);
        }
    }


    void Start() {
        if (photonView.IsMine) {
            // hide for my player the 3rd person view gun and hide the model(I want only to see the gun)
            GetComponent<CharacterController>().enabled = true;
            MoveToLayer(gunObject, LayerMask.NameToLayer("Hidden"));
            MoveToLayer(playerObject, LayerMask.NameToLayer("Hidden"));
        } else {
            // for other players show the model and 3rd person gun.
            position = transform.position;
            rotation = transform.rotation;
        }
    }

    void Update() {
        if (!photonView.IsMine) {
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * smoothing);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * smoothing);
        }
    }

    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    void FixedUpdate() {
        if (photonView.IsMine)
        {
            animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
        }
    }

    /// <summary>
    /// Used to customize synchronization of variables in a script watched by a photon network view.
    /// </summary>
    /// <param name="stream">The network bit stream.</param>
    /// <param name="info">The network message information.</param>
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        } else {
            position = (Vector3)stream.ReceiveNext();
            rotation = (Quaternion)stream.ReceiveNext();
        }
    }

}
