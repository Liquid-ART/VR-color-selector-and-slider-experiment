using UnityEngine;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour
{

    [SerializeField]
    private BoxCollider uiCollider;
    [SerializeField]
    private float defaultLenghth = 2f;
    [SerializeField]
    private GameObject m_Dot;
    [SerializeField]
    private VRInputModule m_InputModule;
    [SerializeField]
    private Transform joystick;

    private LineRenderer m_LineRenderer = null;
    private MeshRenderer m_DotRenderer;

    GameObject lastHittedObject;

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_DotRenderer = m_Dot.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        m_InputModule.Process();
        PointerEventData data = m_InputModule.GetData();

        transform.position = joystick.position;
        transform.rotation = joystick.rotation;
        // Use default or distance
        float targetLenghth = data.pointerCurrentRaycast.distance == 0 ? defaultLenghth : data.pointerCurrentRaycast.distance;

        //  Raycast
        RaycastHit hit = CreateRaycast();

        //  Default or based on hit

        Vector3 endPosition = joystick.position + (transform.forward * targetLenghth);

        //  Default or based on hit

        if (hit.collider == uiCollider)
        {

            bool isHitUI = data.pointerCurrentRaycast.gameObject != null;
            if (isHitUI)
                endPosition = data.pointerCurrentRaycast.worldPosition;

            m_LineRenderer.enabled = true;
            m_DotRenderer.enabled = true;
        }

        else
        {
            m_DotRenderer.enabled = false;
            m_LineRenderer.enabled = false;

        }

        if(hit.collider != null)
            lastHittedObject = hit.collider.gameObject;


        //  Set position of the dot

        m_Dot.transform.position = endPosition;

        //  Set linerenderer

        m_LineRenderer.SetPosition(0, this.gameObject.transform.position);
        m_LineRenderer.SetPosition(1, endPosition);

    }


    private RaycastHit CreateRaycast()
    {
        RaycastHit hit;
        Ray ray = new Ray(this.gameObject.transform.position, transform.forward);
        Physics.Raycast(ray, out hit);
        return hit;
    }


}
