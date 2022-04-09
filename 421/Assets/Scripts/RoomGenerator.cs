using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class RoomGenerator : MonoBehaviour
{
    public enum Direction{up, down, left, right};
    public Direction direction;



    [Header("房间信息")]
    public GameObject roomPrefab;
    public int roomNumber;
    public Color startColor, endColor;
    private Room endRoom;

    [Header("位置控制")]
    public Transform generatorPoint;
    public float xOffset;
    public float yOffset;
    public LayerMask roomLayer;
    public int maxStep;
    bool minimap;
    public GameObject mapui;
    public GameObject minimapui;

    public List<Room> rooms = new List<Room>();

    List<Room> farRooms = new List<Room>();
    List<Room> lessFarRooms = new List<Room>();
    List<Room> oneWayRooms = new List<Room>();

    public WallType wallType;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < roomNumber; i++)
        {
            rooms.Add(Instantiate(roomPrefab,generatorPoint.position,Quaternion.identity).GetComponent<Room>());
            rooms[i].name += i;

            //改变point位置
            ChangePointPos();
        }

        foreach (var room in rooms)
        {
            // if(room.transform.position.sqrMagnitude > endRoom.transform.position.sqrMagnitude)
            //     endRoom = room.gameObject;
            SetupRoom(room, room.transform.position);
        }

        rooms[0].sq.GetComponent<SpriteRenderer>().color = startColor;

        FindEndRoom();
        endRoom.sq.GetComponent<SpriteRenderer>().color = endColor;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("`"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(Input.GetKeyDown("m"))
        {
            if(minimap)
            {
                minimap = !minimap;
                minimapui.SetActive(minimap);
                mapui.SetActive(!minimap);
            }
            else
            {
                minimap = !minimap;
                minimapui.SetActive(minimap);
                mapui.SetActive(!minimap);
            }
        }
    }

    void ChangePointPos()
    {
        do
        {
            direction = (Direction)Random.Range(0, 4);

            switch (direction)
            {
                case Direction.up:
                    generatorPoint.position += new Vector3(0, yOffset, 0);
                    break;
                case Direction.down:
                    generatorPoint.position += new Vector3(0, -yOffset, 0);
                    break;
                case Direction.left:
                    generatorPoint.position += new Vector3(-xOffset, 0, 0);
                    break;
                case Direction.right:
                    generatorPoint.position += new Vector3(xOffset, 0, 0);
                    break;
            }
        }
        while (Physics2D.OverlapCircle(generatorPoint.position,0.2f,roomLayer));
    }
    

    public void SetupRoom(Room newRoom,Vector3 roomPosition)
    {
        newRoom.roomUp = Physics2D.OverlapCircle(roomPosition + new Vector3(0, yOffset, 0), 0.2f, roomLayer);
        newRoom.roomDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0, -yOffset, 0), 0.2f, roomLayer);
        newRoom.roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0, 0), 0.2f, roomLayer);
        newRoom.roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0, 0), 0.2f, roomLayer);
        
        newRoom.UpdateRoom(xOffset ,yOffset);
        switch(newRoom.doorNumber)
        {
            case 1:
                if(newRoom.roomUp)
                    Instantiate(wallType.singleU, roomPosition, Quaternion.identity);
                if (newRoom.roomDown)
                    Instantiate(wallType.singleD, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft)
                    Instantiate(wallType.singleL, roomPosition, Quaternion.identity);
                if (newRoom.roomRight)
                    Instantiate(wallType.singleR, roomPosition, Quaternion.identity);
                break;
            case 2:
                if(newRoom.roomLeft && newRoom.roomUp)
                    Instantiate(wallType.doubleUL, roomPosition, Quaternion.identity);
                if (newRoom.roomUp && newRoom.roomRight)
                    Instantiate(wallType.doubleUR, roomPosition, Quaternion.identity);
                if (newRoom.roomDown && newRoom.roomLeft)
                    Instantiate(wallType.doubleDL, roomPosition, Quaternion.identity);
                if (newRoom.roomDown && newRoom.roomRight)
                    Instantiate(wallType.doubleDR, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomRight)
                    Instantiate(wallType.doubleLR, roomPosition, Quaternion.identity);
                if (newRoom.roomDown && newRoom.roomUp)
                    Instantiate(wallType.doubleUD, roomPosition, Quaternion.identity);
                break;
            case 3:
                if(newRoom.roomLeft && newRoom.roomRight && newRoom.roomDown)
                    Instantiate(wallType.tripleDLR, roomPosition, Quaternion.identity);
                if (newRoom.roomUp && newRoom.roomLeft && newRoom.roomRight)
                    Instantiate(wallType.tirpleULR, roomPosition, Quaternion.identity);
                if (newRoom.roomUp && newRoom.roomRight && newRoom.roomDown)
                    Instantiate(wallType.tripleUDR, roomPosition, Quaternion.identity);
                if (newRoom.roomUp && newRoom.roomDown && newRoom.roomLeft)
                    Instantiate(wallType.tripleUDL, roomPosition, Quaternion.identity);
                break;
            case 4:
                Instantiate(wallType.fourDoors, roomPosition, Quaternion.identity);
                break;
        }
    }

    public void FindEndRoom()
    {
        //获得最大数值
        for (int i = 0; i < rooms.Count; i++)
        {
            if(rooms[i].stepToStart > maxStep)
            {
                maxStep = rooms[i].stepToStart;
            }
        }
        //获得最大值和次大值房间
        foreach (var room in rooms)
        {
            if(room.stepToStart == maxStep)
                farRooms.Add(room);
            if(room.stepToStart == maxStep - 1)
                lessFarRooms.Add(room);
        }
        
        for (int i = 0; i < farRooms.Count; i++)
        {
            if(farRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add(farRooms[i]);
        }

        for (int i = 0; i < lessFarRooms.Count; i++)
        {
            if (lessFarRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add(lessFarRooms[i]);
        }

        if(oneWayRooms.Count != 0)
        {
            endRoom = oneWayRooms[Random.Range(0, oneWayRooms.Count)];
        }
        else
        {
            endRoom = farRooms[Random.Range(0, farRooms.Count)];
        }
    }
}

[System.Serializable]
public class WallType
{
    public GameObject singleL, singleR, singleU, singleD,
                      doubleUL, doubleUR, doubleUD, doubleDL, doubleDR, doubleLR,
                      tripleUDL, tripleUDR, tirpleULR, tripleDLR,
                      fourDoors;
}