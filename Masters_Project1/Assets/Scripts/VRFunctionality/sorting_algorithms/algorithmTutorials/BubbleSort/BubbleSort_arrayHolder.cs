using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleSort_arrayHolder : MonoBehaviour
{
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
	private void Start()
	{
		paused = false;
		structarr = new List<MyStruct>();
	}
	[Serializable]
	public class MyStruct
	{
		public List<int> oldarr;
		public int activeImage;
		public string steptxt;
		public string arrtxt;
	}

	public void Update()
	{
		speed = slider.value;
	}
	public void Display(List<int> arr2, int size)
	{
		Txt_Text.text = "";
		int Tmpsize = size - 1;
		for (int i = 0; i < size; i++)
		{
			if (i == 0)
			{
				Txt_Text.text += "a[" + Tmpsize + "] = " + arr2[i].ToString();
			}
			else if (i > 0)
			{
				Txt_Text.text += ", " + arr2[i].ToString();
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
		}

		structarr.Clear();
		structarr = new List<MyStruct>();
		currentstrucstep = -1;
		maxstrucstep = -1;
		m_selectionAni.ShowGraph(arr3);

		int i, j;
		for (i = 0; i < arr3.Count - 1; i++)
		{
			imageController(1);

			Step.text = "This loop will run n times until the sholea rray ahs been processed";
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
			m_selectionAni.ShowGraph(arr3);
			yield return new WaitForSeconds(speed);
			if (next || previous)
			{
				yield return StartCoroutine(changeStep());
			}
			while (paused)
			{
				yield return null;
			}

			for (j = 0; j < arr3.Count - i - 1; j++)
			{
				imageController(2);

				Step.text = "this loop will iterate over j and j+1";
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
				m_selectionAni.ShowGraph(arr3);
				yield return new WaitForSeconds(speed);
				if (next || previous)
				{
					yield return StartCoroutine(changeStep());
				}
				while (paused)
				{
					yield return null;
				}

				if (arr3[j] > arr3[j + 1])
				{
					Step.text = arr3[j] + " > " + arr3[j + 1] + " the algorithm will perform a swap";

					int temp = arr3[j];
					arr3[j] = arr3[j + 1];
					arr3[j + 1] = temp;
					swapped = true;

					Display(arr3, arr3.Count);
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
					m_selectionAni.ShowGraph(arr3);
					yield return new WaitForSeconds(speed);
					if (next || previous)
					{
						yield return StartCoroutine(changeStep());
					}
					while (paused)
					{
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
				m_selectionAni.ShowGraph(arr3);
				yield return new WaitForSeconds(speed);
				if (next || previous)
				{
					yield return StartCoroutine(changeStep());
				}
				while (paused)
				{
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

		Display(structarr[currentstrucstep].oldarr, structarr[currentstrucstep].oldarr.Count);
		imageController(structarr[currentstrucstep].activeImage);
		Step.text = structarr[currentstrucstep].steptxt;
		m_selectionAni.ShowGraph(structarr[currentstrucstep].oldarr);

		yield return new WaitForSeconds(speed);
		if (currentstrucstep < maxstrucstep)
		{
			goto resume;
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