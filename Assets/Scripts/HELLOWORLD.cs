using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.IO;

public class HELLOWORLD : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SpawnableFurniture;

    public XROrigin sessionOrigin;
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;


    public Text textElement;
    private List<GameObject> extraspawnobject = new List<GameObject>();
    private List<ARRaycastHit> hitList = new List<ARRaycastHit>();
    private List<Vector3> BoundaryPoints;
    private string object_static_name = "";
    private int obj_num = 0;
    public float collisionOffset = 0.4f; // Amount to move the existing object to prevent collision
    public float collisionRadius = 1f;
    // Update is called once per frame
    private void Update()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            //shoot Raycast
            //place the object randomly
            //disable plane manager
            textElement.text = "";


            int rand_plane_selection = Random.Range(0, 3);
            bool planeinfinitytype_collision = false, planewithinpolygon_collision = false, planewithinboundary_collision = false;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            planewithinpolygon_collision = raycastManager.Raycast(ray, hitList);

            







            if (planewithinpolygon_collision && isButtonPress() == false)
            {

                GameObject _object;




                if (IsObjectAtPosition(hitList[0].pose.position))
                {
                    // Instantiate the GameObject
                    textElement.text += "Clicked on a spawned object!";
                    SaveMetamorphData("\nClicked on a spawned object! " + obj_num, new Vector3(0, 0, 0), new Vector3(0, 0, 0));
                }
                else
                {

                    _object = Instantiate(SpawnableFurniture);

                    int rand_val = Random.Range(0, 2);
                    if (rand_val == 0)
                    {
                        _object.transform.position = hitList[0].pose.position + new Vector3(0.0f, 1.0f, 0.5f); //fail cases simulation
                        _object.tag = "EnemyAnimal";
                        _object.transform.localScale = new Vector3((float)0.3, (float)0.3, (float)0.3);
                    }
                    

                    //textElement.text += $"\n{hitList[0].hitType}, {hitList.Count}";
                    bool is_inside = false;

                    foreach (var plane in planeManager.trackables)
                    {
                        if (IsPointInsidePlane(_object.transform.position, plane.transform) == true)
                        {
                            SaveMetamorphData("\nWithin boundary of the plane.(Passed) " + obj_num, _object.transform.position, hitList[0].pose.position);
                            is_inside = true;
                            break;
                        }
                    }









                    

                    // interaction metamorphic test
                    //textElement.text += _object.transform.position == hitList[0].pose.position ?  "\nraycast and placement is in same position.(Passed)" : "\nNot in same position(Fail)";
                    if (_object.transform.position == hitList[0].pose.position)
                    {
                        SaveMetamorphData("\nraycast and placement is in same position.(Passed) " + obj_num, _object.transform.position, hitList[0].pose.position);
                    }
                    else
                    {
                        SaveMetamorphData("\nNot in same position(Fail) " + obj_num, _object.transform.position, hitList[0].pose.position);
                    }

                    //SaveMetamorphData("brinto", _object.transform.position);
                    if (object_static_name != "")
                    {
                        //textElement.text += $"\n{ _object.name}, {object_static_name}";
                        //textElement.text += _object.name == object_static_name+"(Clone)" ? "\nCorrect object intantiated.(Passed)" : "\nWrong Object Instantiate(Fail)";
                        if (_object.name == object_static_name + "(Clone)")
                        {
                            SaveMetamorphDataName("\nCorrect object intantiated.(Passed) " + obj_num, _object.name, object_static_name);
                        }
                        else
                        {
                            SaveMetamorphDataName("\nWrong Object Instantiate(Fail) " + obj_num, _object.name, object_static_name);
                        }
                    }


                }

                
            }

            
            obj_num++;
        }

    }


    private bool IsObjectAtPosition(Vector3 position)
    {
        
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(position);

        // Perform a raycast from the screen space point
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        RaycastHit hit;

        
        int layerMask = 1 << LayerMask.NameToLayer("ARPlane");
        layerMask = ~layerMask; // This inverts the layer mask to ignore the ARPlane layer


        // Arbitrary distance for the raycast, adjust as necessary
        float maxDistance = 100f;

        // Check for a collider hit within the maxDistance
        if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            // We've hit something
            return true;
        }

        // Nothing was hit
        return false;
    }


    bool IsPointInsidePlane(Vector3 point, Transform plane)
    {
        // Calculate the distance from the point to the plane
        float distance = Vector3.Dot(plane.up, point - plane.position);

        // Check if the distance is within a certain tolerance (adjust this value as needed)
        float tolerance = 0.1f;

        if (Mathf.Abs(distance) < tolerance)
        {
            // The point is inside the plane
            return true;
        }
        else
        {
            // The point is outside the plane
            return false;
        }
    }

    public void SaveMetamorphData(string s, Vector3 v, Vector3 u)
    {
        //textElement.text += "\nbefore it";

        string path = Application.persistentDataPath + "/PlayerData_collision.txt";
        // This text is added only once to the file.
        if (!File.Exists(path))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("\n" + s + " position is " + v.x + " " + v.y + " " + v.z + " position is" + u.x + " " + u.y + " " + u.z);
            }
        }
        else
        {
            // This text is always added, making the file longer over time
            // if it is not deleted.
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine("\n" + s + " position is " + v.x + " " + v.y + " " + v.z + " position is" + u.x + " " + u.y + " " + u.z);
            }
        }


        // File.WriteAllText(path, "\n"+s+" position is "+v.x+" "+v.y+" "+v.z+" position is" + u.x + " " + u.y + " " + u.z);
        //textElement.text += "\nSave file created at: ";
    }

    public void SaveMetamorphDataName(string s, string v, string u)
    {
        string path = Application.persistentDataPath + "/PlayerData_collision.txt";

        if (!File.Exists(path))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("\n" + s + " " + v + " " + u);
            }
        }
        else
        {
            // This text is always added, making the file longer over time
            // if it is not deleted.
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine("\n" + s + " " + v + " " + u);
            }
        }



        // File.WriteAllText(Application.persistentDataPath + "/PlayerData.txt", "\n" + s+" "+v+" "+u);
        //textElement.text += "\nSave file created at: ";
    }



    public bool isButtonPress()
    {
        if (EventSystem.current.currentSelectedGameObject?.GetComponents<Button>() == null)
        {
            //textElement.text += "\ninside isbuttonpress=false";
            return false;
        }
        else
        {
            // textElement.text += "\ninside isbuttonpress=true";
            return true;
        }
    }
    public void SwitchFurniture(GameObject furniture)
    {
        SpawnableFurniture = furniture;
        extraspawnobject.Add(SpawnableFurniture);
        //set real spawnableobject variable
        object_static_name = SpawnableFurniture.name;
        int second_rand = Random.Range(0, 2);
        if (second_rand == 0) { SpawnableFurniture = extraspawnobject[Random.Range(0, extraspawnobject.Count)]; }


        //textElement.text += $"\nwill spawn {furniture.name} {extraspawnobject.Count}";
    }
}
