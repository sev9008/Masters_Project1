using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is the never ending source of confusion.
/// prepare because this script is a wall of code, but the code works
/// 
/// this script performs the main sort algorithm.  It also keeps track of every step and move.
/// </summary>
public class BubbleSort_arrayHolder : MonoBehaviour
{
	public GameObject[] b;

	public List<int> arr3;
	public Text Txt_Text;
	public float waittime;
	public Text Step;
	public float speed;

	public Slider slider;

	public GameObject[] image;
	public bool paused;
	private int pi;
	public List<MyStruct> structarr;

	public int currentstrucstep;
	public int maxstrucstep;

	public bool previous;
	public bool next;
	public bool running;
	public SelectionAni m_selectionAni;
	public bool swapped;
	public bool manual;

	public Material Sortedmat;
	public Material Normalmat;
	public Material checkmat;

	public Text itextstep1;
	public Text jtextstep1;
	public Text ifstep1;

	[Serializable]
	public class MyStruct//this struct holds all of the previous steps.  Its really not very good for memory, but it works out weel for what we need.
	{
		public List<int> oldarr;
		public int activeImage;
		public string steptxt;
		public string arrtxt;
		public int jval;
		public int maxstep;
	}

	private void Start()
	{
		paused = false;
		manual = false;

		structarr = new List<MyStruct>();//init the struct
	}

	public void Update()
	{
		speed = slider.value;//update the slider value
	}
	public void Display(List<int> arr2, int size, int j, int i)
	{
		Txt_Text.text = "";
		for (int n = 0; n < size; n++)
		{
			b[n].SetActive(true);
			if (n == j || n == j+1)
			{
				b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
				b[n].GetComponentInChildren<MeshRenderer>().material = checkmat;
			}
			else if (n >= i)
			{
				b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
				b[n].GetComponentInChildren<MeshRenderer>().material = Sortedmat;
			}
			else
			{
				b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
				b[n].GetComponentInChildren<MeshRenderer>().material = Normalmat;
			}
		}
	}

	public IEnumerator Bubble()
	{
		running = true;
		for (int n = 0; n < structarr.Count; n++)//reset all the data in the struct
		{
			structarr[n].oldarr.Clear();
			structarr[n].activeImage = -1;
			structarr[n].steptxt = "";
			structarr[n].arrtxt = "";
			structarr[n].jval = -20;
			structarr[n].maxstep = 100;
		}
		for (int k = 0; k < b.Length; k++)//set all blocks to false
		{
			b[k].SetActive(false);
		}
		structarr.Clear();//seems redundant.  dont actually know if the above is needed
		structarr = new List<MyStruct>();
		currentstrucstep = -1;
		maxstrucstep = -1;
		m_selectionAni.ShowGraph(arr3);//display the current array on our graph
		Display(arr3, arr3.Count, -20, 100);//activate the blocks and set their mats
		Step.text = "Start by setting j to 0 and i to 0.  We will increment j and check index j and j+1.  If j > j+1 then swap the indexes";

		if (!manual)//you will see these next three case statements alot.  thses control whether the animation is paused or unpaused.  then you can resume, press next, or press previous
		{
			yield return new WaitForSeconds(speed);
		}
		if (manual)
		{
			paused = true;
		}
		while (paused && manual)
		{
			if (next && currentstrucstep >= maxstrucstep)//if the current step is the the last step we ahve saved in ours truct then just unpause and proceed
			{
				next = false;
				paused = false;
			}
			else if (next || previous)//if sturct arr has moves stored and we are not on the last step in out sturct, then transition to change step.
			{
				yield return StartCoroutine(changeStep());
			}
			yield return null;
		}

		int i, j;
		for (i = 0; i < arr3.Count - 1; i++)
		{
			int maxsteparr = arr3.Count - i;
			itextstep1.text = "i = " + i;
			imageController(0);
			Display(arr3, arr3.Count, -20, maxsteparr);

			Step.text = "We will increment j and begin checking ig j > j+1";
			currentstrucstep++;//this will add the current step to the array.  its a wall of code but you will see it alot
			maxstrucstep++;
			var a = new MyStruct();
			structarr.Add(a);
			structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
			for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
			{
				structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
			}
			structarr[currentstrucstep].activeImage = 0;
			structarr[currentstrucstep].steptxt = Step.text;
			structarr[currentstrucstep].jval = -20;
			structarr[currentstrucstep].maxstep = maxsteparr;
			m_selectionAni.ShowGraph(arr3);
			if (!manual)//descsibed above
			{
				yield return new WaitForSeconds(speed);
			}
			if (manual)
			{
				paused = true;
			}
			while (paused && manual)
			{
				if (next && currentstrucstep >= maxstrucstep)
				{
					next = false;
					paused = false;
				}
				else if (next || previous)
				{
					yield return StartCoroutine(changeStep());
				}
				yield return null;
			}

			for (j = 0; j < arr3.Count - i - 1; j++)
			{
				jtextstep1.text = "j = " + j;
				imageController(1);
				Display(arr3, arr3.Count, j, maxsteparr);

				Step.text = "We will increment j and check if j > j+1";
				currentstrucstep++;//same stuff we have seen basically every time there is a step or change in the array we are gonna have to run these lines.  Thats why you seem them repeated so much.  It also difficult to make a function out of it.
				maxstrucstep++;
				var b = new MyStruct();
				structarr.Add(b);
				structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
				for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
				{
					structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
				}
				structarr[currentstrucstep].activeImage = 1;
				structarr[currentstrucstep].steptxt = Step.text;
				structarr[currentstrucstep].jval = j;
				structarr[currentstrucstep].maxstep = maxsteparr;
				m_selectionAni.ShowGraph(arr3);
				if (!manual)
				{
					yield return new WaitForSeconds(speed);
				}
				if (manual)
				{
					paused = true;
				}
				while (paused && manual)
				{
					if (next && currentstrucstep >= maxstrucstep)
					{
						next = false;
						paused = false;
					}
					else if (next || previous)
					{
						yield return StartCoroutine(changeStep());
					}
					yield return null;
				}
				ifstep1.text = arr3[j] + " > " + arr3[j + 1];
				if (arr3[j] > arr3[j + 1])
				{
					Step.text = "Since " + arr3[j] + " > " + arr3[j + 1] + " the algorithm will perform a swap.";

					int temp = arr3[j];
					arr3[j] = arr3[j + 1];
					arr3[j + 1] = temp;
					swapped = true;

					Display(arr3, arr3.Count, j, maxsteparr);//same stuff we have seen
					imageController(2);
					currentstrucstep++;
					maxstrucstep++;
					var d = new MyStruct();
					structarr.Add(d);
					structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
					for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
					{
						structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
					}
					structarr[currentstrucstep].activeImage = 2;
					structarr[currentstrucstep].steptxt = Step.text;
					structarr[currentstrucstep].jval = j;
					structarr[currentstrucstep].maxstep = maxsteparr;
					m_selectionAni.ShowGraph(arr3);
					if (!manual)
					{
						yield return new WaitForSeconds(speed);
					}
					if (manual)
					{
						paused = true;
					}
					while (paused && manual)
					{
						if (next && currentstrucstep >= maxstrucstep)
						{
							next = false;
							paused = false;
						}
						else if (next || previous)
						{
							yield return StartCoroutine(changeStep());
						}
						yield return null;
					}
				}
			}

			Step.text = "We will increment i and lock element " + (arr3.Count - i - 1);

			Display(arr3, arr3.Count, j, maxsteparr);//same stuff we have seen
			imageController(2);
			currentstrucstep++;
			maxstrucstep++;
			var c = new MyStruct();
			structarr.Add(c);
			structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
			for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
			{
				structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
			}
			structarr[currentstrucstep].activeImage = 2;
			structarr[currentstrucstep].steptxt = Step.text;
			structarr[currentstrucstep].jval = j;
			structarr[currentstrucstep].maxstep = maxsteparr;
			m_selectionAni.ShowGraph(arr3);
			if (!manual)
			{
				yield return new WaitForSeconds(speed);
			}
			if (manual)
			{
				paused = true;
			}
			while (paused && manual)
			{
				if (next && currentstrucstep >= maxstrucstep)
				{
					next = false;
					paused = false;
				}
				else if (next || previous)
				{
					yield return StartCoroutine(changeStep());
				}
				yield return null;
			}

			if (swapped == false)
			{
				Step.text = "No swaps occured.  The algorithm is complete";
				imageController(3);

				currentstrucstep++;//same stuff we have seen
				maxstrucstep++;
				var d = new MyStruct();
				structarr.Add(d);
				structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
				for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
				{
					structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
				}
				structarr[currentstrucstep].activeImage = 3;
				structarr[currentstrucstep].steptxt = Step.text;
				structarr[currentstrucstep].maxstep = maxsteparr;
				m_selectionAni.ShowGraph(arr3);
				if (!manual)
				{
					yield return new WaitForSeconds(speed);
				}
				if (manual)
				{
					paused = true;
				}
				while (paused && manual)
				{
					if (next && currentstrucstep >= maxstrucstep)
					{
						next = false;
						paused = false;
					}
					else if (next || previous)
					{
						yield return StartCoroutine(changeStep());
					}
					yield return null;
				}

				break;
			}
		}
		imageController(-1);
		Step.text = "Finished";
		for (int n = 0; n < arr3.Count; n++)
		{
			b[n].GetComponentInChildren<MeshRenderer>().material = Sortedmat;
		}
	}

	public IEnumerator changeStep()//if next or previous is pressed then this algorithm takes over.  it will display the current step and attempt to continue the animation 
	{
		resume:
		if (previous && currentstrucstep > 0)
		{
			currentstrucstep--;
			previous = false;
		}
		else if (next && currentstrucstep != maxstrucstep || currentstrucstep != maxstrucstep)
		{
			currentstrucstep++;
			next = false;
		}
		Display(structarr[currentstrucstep].oldarr, structarr[currentstrucstep].oldarr.Count, structarr[currentstrucstep].jval, structarr[currentstrucstep].maxstep);
		imageController(structarr[currentstrucstep].activeImage);
		Step.text = structarr[currentstrucstep].steptxt;
		m_selectionAni.ShowGraph(structarr[currentstrucstep].oldarr);

		m_selectionAni.ShowGraph(structarr[currentstrucstep].oldarr);
		if (manual)
		{
			paused = true;
		}
		while (paused && manual)
		{
			if (next && currentstrucstep >= maxstrucstep)
			{
				next = false;
				paused = false;
			}
			else if (previous && currentstrucstep > 0)
			{
				goto resume;
			}
			else if (next && currentstrucstep < maxstrucstep)
			{
				goto resume;
			}
			yield return null;
		}
		if (!manual)
		{
			yield return new WaitForSeconds(speed);
			if (currentstrucstep >= maxstrucstep)
			{
				paused = false;
			}
			else if (currentstrucstep >= 0 && currentstrucstep < maxstrucstep)
			{
				goto resume;
			}
		}
	}

	public void imageController(int k)//control the active image
	{
		for (int i = 0; i < image.Length; i++)
		{
			if (i == k)
			{
				image[i].SetActive(true);
			}
			else
			{
				image[i].SetActive(false);
			}
		}
	}
}