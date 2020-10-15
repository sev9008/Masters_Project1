using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	private void Start()
	{
		paused = false;
		manual = false;

		structarr = new List<MyStruct>();
	}
	[Serializable]
	public class MyStruct
	{
		public List<int> oldarr;
		public int activeImage;
		public string steptxt;
		public string arrtxt;
		public int jval;
		public int maxstep;
	}

	public void Update()
	{
		speed = slider.value;
	}
	public void Display(List<int> arr2, int size, int j, int i)
	{
		//size = size - 1;
		Txt_Text.text = "";
		//int Tmpsize = size - 1;
		for (int n = 0; n < size; n++)
		{
			b[n].SetActive(true);
			//Debug.Log(l + " < " + n + " < "+ r);
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
		for (int n = 0; n < structarr.Count; n++)
		{
			structarr[n].oldarr.Clear();
			structarr[n].activeImage = -1;
			structarr[n].steptxt = "";
			structarr[n].arrtxt = "";
			structarr[n].jval = -20;
			structarr[n].maxstep = 100;
		}
		for (int k = 0; k < b.Length; k++)
		{
			b[k].SetActive(false);
		}
		structarr.Clear();
		structarr = new List<MyStruct>();
		currentstrucstep = -1;
		maxstrucstep = -1;
		m_selectionAni.ShowGraph(arr3);
		Display(arr3, arr3.Count, -20, 100);
		Step.text = "Start at the beggining fo the array and begin checking for swaps.";

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
		int i, j;
		for (i = 0; i < arr3.Count - 1; i++)
		{
			int maxsteparr = arr3.Count - i;
			itextstep1.text = "i = " + i;
			imageController(1);
			Display(arr3, arr3.Count, -20, maxsteparr);

			Step.text = "Increment j and resume checking for swaps.";
			currentstrucstep++;
			maxstrucstep++;
			var a = new MyStruct();
			structarr.Add(a);
			structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
			for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
			{
				structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
			}
			structarr[currentstrucstep].activeImage = 4;
			structarr[currentstrucstep].steptxt = Step.text;
			structarr[currentstrucstep].jval = -20;
			structarr[currentstrucstep].maxstep = maxsteparr;
			m_selectionAni.ShowGraph(arr3);
			Step.text = "this loop will iterate over j and j+1";
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

			for (j = 0; j < arr3.Count - i - 1; j++)
			{
				jtextstep1.text = "j = " + j;
				imageController(2);
				Display(arr3, arr3.Count, j, maxsteparr);

				Step.text = "Increment j and resume checking for swaps.";
				currentstrucstep++;
				maxstrucstep++;
				var b = new MyStruct();
				structarr.Add(b);
				structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
				for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
				{
					structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
				}
				structarr[currentstrucstep].activeImage = 4;
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
					Step.text = arr3[j] + " > " + arr3[j + 1] + " the algorithm will perform a swap.";

					int temp = arr3[j];
					arr3[j] = arr3[j + 1];
					arr3[j + 1] = temp;
					swapped = true;

					Display(arr3, arr3.Count, j, maxsteparr);
					imageController(3);
					currentstrucstep++;
					maxstrucstep++;
					var c = new MyStruct();
					structarr.Add(c);
					structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
					for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
					{
						structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
					}
					structarr[currentstrucstep].activeImage = 4;
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

			if (swapped == false)
			{
				Step.text = "No swaps occured.  The algorithm is complete";
				imageController(4);

				currentstrucstep++;
				maxstrucstep++;
				var d = new MyStruct();
				structarr.Add(d);
				structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
				for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
				{
					structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
				}
				structarr[currentstrucstep].activeImage = 4;
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
	}

	public IEnumerator changeStep()
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

	public void imageController(int k)
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