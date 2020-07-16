using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleSort_arrayHolder : MonoBehaviour
{
	public List<int> arr;
	public Text Txt_Text;
	public int size;
	public float waittime;
	public Text Step;
	public float speed;

	public Slider slider;

	public GameObject image1;
	public GameObject image1_1;
	public GameObject image2;
	public GameObject image3;
	public bool paused;

	private void Start()
	{
		paused = false;
	}

	public void Update()
	{
		speed = slider.value;
	}
	public void Display()
	{
		Txt_Text.text = "";
		int Tmpsize = size - 1;
		for (int i = 0; i < size; i++)
		{
			if (i == 0)
			{
				Txt_Text.text += "a[" + Tmpsize + "] = " + arr[i].ToString();
			}
			else if (i > 0)
			{
				Txt_Text.text += ", " + arr[i].ToString();
			}
		}
	}

	public IEnumerator Bubble(List<int> arr)
	{
		int n = size + 1;
		bool swapped = true;
		while (swapped)
		{
			swapped = false;

			image1.SetActive(true);
			image1_1.SetActive(false);
			image2.SetActive(false);
			image3.SetActive(false);
			Step.text = "Start while loop with our n elements";
			while (paused)
			{
				yield return null;
			}
			yield return new WaitForSeconds(speed);
			for (int i = 1; i < n - 1; i++)
			{

				image1.SetActive(false);
				image1_1.SetActive(false);
				image2.SetActive(true);
				image3.SetActive(false);
				Step.text = "increment i = " + i;
				while (paused)
				{
					yield return null;
				}
				yield return new WaitForSeconds(speed);

				if (arr[i - 1] > arr[i])
				{
					Step.text = "Swap arr[i-1] = " + arr[i - 1] + " with arr[i] = " + arr[i];

					int temp = arr[i - 1];
					arr[i - 1] = arr[i];
					arr[i] = temp;
					swapped = true;

					image1.SetActive(false);
					image1_1.SetActive(false);
					image2.SetActive(false);
					image3.SetActive(true);
					Display();
					while (paused)
					{
						yield return null;
					}
					yield return new WaitForSeconds(speed);
				}
			}
			n--;
			image1.SetActive(false);
			image1_1.SetActive(true);
			image2.SetActive(false);
			image3.SetActive(false);
			Step.text = "Decrement n = " + n;
			while (paused)
			{
				yield return null;
			}
			yield return new WaitForSeconds(speed);
		}
		Step.text = "Finished";
		image1.SetActive(false);
		image1_1.SetActive(false);
		image2.SetActive(false);
		image3.SetActive(false);
		Display();
	}
}
