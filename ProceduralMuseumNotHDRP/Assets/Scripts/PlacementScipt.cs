using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlacementScipt : MonoBehaviour
{
    #region Public Variables
    public GameObject[] objectsToPlace;
    public GameObject wall;
    [System.Serializable]
    public struct GameObjectsAttributes
    {
        public int id;
        public Vector3 area;
        public GameObject go;
    }
    public GameObjectsAttributes[] gosAttributes;
    public GameObject door;
    #endregion

    #region Private Variables
    private Vector3 localTransfromOfPlane;
    private Vector2 area;
    private GameObject[] walls;
    private GameObject copyOfThis; //For walls
    private float widthOfWalls;
    private float lengthOfWalls;
    private List<GameObject> doorList;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        localTransfromOfPlane = transform.localPosition;
        Renderer render = GetComponent<Renderer>();
        area = new Vector2(render.bounds.size.x, render.bounds.size.z);
        gosAttributes = new GameObjectsAttributes[objectsToPlace.Length];
        SetUpGameObjectsStruct(objectsToPlace);
        PlaceGameObjects(gosAttributes);
        PlaceWalls();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetUpGameObjectsStruct(GameObject[] go)
    {
        for (int i = 0; i < objectsToPlace.Length; i++)
        {
            Renderer renderGo = objectsToPlace[i].GetComponent<Renderer>();
            gosAttributes[i].go = objectsToPlace[i];
            gosAttributes[i].area = new Vector3(renderGo.bounds.size.x, renderGo.bounds.size.y, renderGo.bounds.size.z);
        }
    }

    void PlaceGameObjects(GameObjectsAttributes[] gosAtts)
    {
        //First we need to calculate the area of the plane that we are working on
        //How I approached the problem of placing go's is to divide the area by the number of go's that we have, later on, I will introduce some types of go's here depending of it is for a wall or the importance of it.
        float offsetForY;
        Quaternion rotation = new Quaternion(0, 0, 0, 0);
        int numberOfAreas = objectsToPlace.Length;
        Vector2 areaForGos = new Vector2(area.x / numberOfAreas, area.y / numberOfAreas);
        /*if (objectsToPlace.Length == 1)
        {
            //I have to add code for important room adding some more assets to make it look cooler...
            offsetForY = gosAttributes[0].go.GetComponent<Renderer>().bounds.size.y / 2;
            Vector3 coordsToPlace = new Vector3(localTransfromOfPlane.x - (area.x / 2) + (areaForGos.x / 2), localTransfromOfPlane.y + offsetForY, localTransfromOfPlane.z - (area.y / 2) + (areaForGos.y / 2));
            gosAttributes[0].go = Instantiate(objectsToPlace[0], coordsToPlace, rotation) as GameObject;
            return 0;
        }
        else
        {*/
            int randRows = Random.Range(1, objectsToPlace.Length);
            Debug.Log("Rows " + randRows);
            float areaNeededForZ = area.y / randRows;
            int id = 0;
            int prevRandomXInstances = 0;

            for (int j = 0; j < randRows; j++)
            {
                int remainingRows = randRows - j;
                int maxRand = objectsToPlace.Length - prevRandomXInstances - remainingRows;
                int randomXInstances = Random.Range(1, maxRand);
                Debug.Log(randomXInstances);
                
                prevRandomXInstances += randomXInstances;
                if (remainingRows == 1)
                {
                    randomXInstances = objectsToPlace.Length - prevRandomXInstances + 1;
                }
                float areaNeededForX = area.x / randomXInstances;
                for (int i = 0; i < randomXInstances; i++)
                    {
                        Debug.Log("Id "+id);
                        float x = localTransfromOfPlane.x - (area.x / 2) + (areaNeededForX / 2) + (areaNeededForX * i);
                        float z = localTransfromOfPlane.z - (area.y / 2) + (areaNeededForZ / 2) + (areaNeededForZ * j);
                        offsetForY = gosAttributes[i].go.GetComponent<Renderer>().bounds.size.y / 2; //Since some GOs has its pivot on the center we have to change it
                        float y = localTransfromOfPlane.y + offsetForY;
                        Vector3 coordsToPlace = new Vector3(x, y, z);
                        gosAttributes[id].go = Instantiate(objectsToPlace[id], coordsToPlace, rotation) as GameObject;
                        gosAttributes[id].go.name = "Cubo_" + id;
                        id++;
                    }
            }
        //}
    }  

    void PlaceWalls()
    {
        walls = new GameObject[4];
        Quaternion rotation = Quaternion.Euler(90, 0, 0);
        Vector3 position = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - area.y / 2);
        _PlaceWalls(0, position, rotation, this.transform.localScale.x, this.transform.localScale.z);
        rotation = Quaternion.Euler(270, 0, 0);
        position = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + area.y / 2);
        _PlaceWalls(1, position, rotation, this.transform.localScale.x, this.transform.localScale.z);
        rotation = Quaternion.Euler(90, 90, 0);
        position = new Vector3(transform.localPosition.x - area.x / 2, transform.localPosition.y, transform.localPosition.z);
        _PlaceWalls(2, position, rotation, this.transform.localScale.z, this.transform.localScale.x);
        rotation = Quaternion.Euler(90, 270, 0);
        position = new Vector3(transform.localPosition.x + area.x / 2, transform.localPosition.y, transform.localPosition.z);
        _PlaceWalls(3, position, rotation, this.transform.localScale.z, this.transform.localScale.x);
        //PlaceDoors();
    }

    void _PlaceWalls(int x, Vector3 originPos, Quaternion rot, float xScale, float yScale)
    {

        /* This is the generation of walls without holes, going to keep it safe if it booleans does not work correctly
        walls[x] = Instantiate(wall, originPos, rot);
        walls[x].transform.localScale = new Vector3(xScale, yScale, transform.localScale.z);
        walls[x].transform.position = new Vector3(walls[x].transform.position.x, walls[x].transform.position.y + walls[x].GetComponent<Renderer>().bounds.size.y / 2, walls[x].transform.position.z);
        Vector3 doorPosition = new Vector3(walls[x].transform.position.x, walls[x].transform.position.y - walls[x].GetComponent<Renderer>().bounds.size.y / 2 + door.GetComponent<Renderer>().bounds.size.y / 2, walls[x].transform.position.z);*/

        GameObject originalWall = new GameObject();
        originalWall = Instantiate(wall, originPos, rot);
        originalWall.transform.localScale = new Vector3(xScale, yScale, transform.localScale.z);
        originalWall.transform.position = new Vector3(originalWall.transform.position.x, originalWall.transform.position.y + originalWall.GetComponent<Renderer>().bounds.size.y / 2, originalWall.transform.position.z);
        Vector3 doorPosition = new Vector3(originalWall.transform.position.x, originalWall.transform.position.y - originalWall.GetComponent<Renderer>().bounds.size.y / 2 + door.GetComponent<Renderer>().bounds.size.y / 2, originalWall.transform.position.z);
        Instantiate(door, doorPosition, originalWall.transform.rotation);
        GameObject wallHoled = new GameObject("HoledWall"+x);
        



    }

    void PlaceDoors()
    {
        foreach(GameObject go in walls)
        {
            Renderer _render = go.GetComponent<Renderer>();
            float xCoord = _render.bounds.size.x / 2 + transform.localPosition.x;
            Vector3 position = new Vector3(xCoord, transform.localPosition.y + door.GetComponent<Renderer>().bounds.size.y / 2, go.transform.localPosition.z + _render.bounds.size.z / 2 );
            Instantiate(door, position, go.transform.rotation);
        }
    }
}
