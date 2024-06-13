using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MissionSpace
{
    public class mousedrag_MS : MonoBehaviour
    {
        // Start is called before the first frame update

        private bool IS_drag;


        public int orbit;


        public LineRenderer line;
        public Text LightDistance;
        private Vector2 startpos;
        public Transform cube;
        public Vector2 currentpos;
        // public Transform currentOrbit;

        bool ismove;
        private void Start()
        {
            startpos = transform.localPosition;
        }
        public void OnPointerdown()
        {
            if (IS_drag) return;
            IS_drag = true;
            Level2Manager_MS.instance.SetRaycasting(transform.GetComponent<Image>());
        }
        public void OnPointerUp()
        {
            print("aaya");
            IS_drag = false;
            LightDistance.text = "";
            Level2Manager_MS.instance.ReSetRaycasting();
            print("Letter_" + (orbit + 1) + "------------" + (planetCollisionOrbit_MS.instance.name));
            if (Level2Manager_MS.instance.IsRightMove == true)
            {
             
                if ("Letter_" + (orbit + 1) == (planetCollisionOrbit_MS.instance.name))
                {
                    print("aaya");
                    line.SetPosition(1, new Vector3(-9.9f, 4.4f, 6.6f));
                    currentpos = Level2Manager_MS.instance.orbit_point[orbit].position;
                    ismove = true;
                    return;
                }
                else
                {
                    transform.localPosition = startpos;
                    line.SetPosition(1, new Vector3(-9.9f, 4.4f, 6.6f));
                    print("wrong move");
                    Level2Manager_MS.instance.CheckGameOverORGameWin(false);
                }

            }
            else
            {
                transform.localPosition = startpos;
                line.SetPosition(1, new Vector3(-9.9f, 4.4f, 6.6f));
            }



        }



        private void Update()
        {

            if (ismove)
            {
                if (currentpos != null)
                {
                    transform.position = Vector2.MoveTowards(transform.position, currentpos, Time.deltaTime * 10);
                    if (Vector2.Distance(transform.position, currentpos) < .1f)
                    {
                        Level2Manager_MS.instance.orbit_point[orbit].GetChild(0).gameObject.SetActive(true);
                        Level2Manager_MS.instance.orbit_point[orbit].GetChild(0).GetComponentInChildren<Animator>().enabled = true;
                        ismove = false;
                        gameObject.SetActive(false);
                        Level2Manager_MS.instance.CheckGameOverORGameWin(true);
                    }
                }
            }
            if (IS_drag)
            {
                RaycastHit hit;
                Ray ray;
               
                     ray = Camera.main.ScreenPointToRay(Input.mousePosition);


                if (Physics.Raycast(ray, out hit, 1000))
                {
                    //    print(Vector3.Distance(transform.position, line.transform.position));
                    Debug.DrawLine(ray.origin, hit.point, Color.red);
                    // print(" hit.distance"+ hit.distance);
                    transform.position = new Vector3(hit.point.x, hit.point.y, hit.distance - 20f);
                    //  print(hit.transform.tag);
                    //LightDistance.text = Vector3.Distance(transform.position, line.transform.position).ToString("f2");
                    float distance = Vector3.Distance(transform.position, line.transform.position);
                    //print("distance  " + distance);
                    cube.transform.position = transform.position;
                    //LightDistance.text = (map(Vector3.Distance(transform.position, line.transform.position), 10, 22, 10, 250)).ToString("f0")+ " light minuts";
                    //LightDistance.text = (map(Vector3.Distance(transform.position, line.transform.position), 10, 22, 10, 250)).ToString("f0")+ " light minuts";
                    MapOrbit(distance);
                    line.SetPosition(1, (transform.position));
                    Vector3 diff = line.transform.position - LightDistance.transform.position;
                    diff.Normalize();

                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                    LightDistance.transform.rotation = Quaternion.Euler(0f, 0f, rot_z-180);
                    //LightDistance.transform.position = new Vector2(transform.position.x-2,transform.position.y);

                }
            }




        }



        float map(float s, float a1, float a2, float b1, float b2)
        {
            return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
        }

        void MapOrbit(float distance)
        {
            float lightminute = 0;
            if (distance > 0f && distance < 3.6f)
            {
                lightminute = map(distance, 0, 3.6f, 0, 3.2f);
            }

            else if (distance > 3.6f && distance < 5.4f)
            {
                lightminute = map(distance, 3.6f, 5.4f, 3.3f, 6.0f);
            }

            else if (distance > 5.4f && distance < 7.47f)
            {
                lightminute = map(distance, 5.4f, 7.47f, 6.0f, 8.3f);
            }

            else if (distance > 7.47f && distance < 9.6f)
            {
                lightminute = map(distance, 7.47f, 9.6f, 8.3f, 12.6f);
            }

            else if (distance > 9.6f && distance < 11.63f)
            {
                lightminute = map(distance, 9.6f, 11.63f, 12.6f, 43.2f);
            }

            else if (distance > 11.63f && distance < 14.34f)
            {
                lightminute = map(distance, 11.63f, 14.34f, 43.2f, 79.3f);
            }

            else if (distance > 14.34f && distance < 16.52f)
            {
                lightminute = map(distance, 14.34f, 16.52f, 79.3f, 159.6f);
            }

            else if (distance > 16.52f && distance < 18.22516f)
            {
                lightminute = map(distance, 16.52f, 18.22516f, 159.6f, 246f);
            }
            else if(distance > 18.22516f)
            {
                lightminute = map(distance, 20f, 50, 247, 400);
            }

            LightDistance.text = lightminute.ToString("f1") + "\n light minutes";
        }
    }
}
