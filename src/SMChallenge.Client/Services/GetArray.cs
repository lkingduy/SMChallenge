using SMChallenge.Client.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMChallenge.Client.Services
{
    public class GetArray
    {
        public int[] sortArr = new int[250];

        public int[] smallArr = new int[50];
        public int[] mediumArr = new int[150];
        public int[] bigArr = new int[250];

        public void GetRandomArray(IndexBase mainPage)
        {
            int min = 20;
            int max = 620;

            Random rnd = new Random();
            for (int i = 0; i < sortArr.Length; i++)
            {
                sortArr[i] = rnd.Next(min, max);
            }
            mainPage.UpdateUI();
        }

        public void GetSmallArray(IndexBase mainPage)
        {
            sortArr = smallArr;

            int min = 20;
            int max = 620;

            Random rnd = new Random();
            for (int i = 0; i < sortArr.Length; i++)
            {
                sortArr[i] = rnd.Next(min, max);
            }
            mainPage.UpdateUI();
        }
        public void GetMediumArray(IndexBase mainPage)
        {
            sortArr = mediumArr;

            int min = 20;
            int max = 620;

            Random rnd = new Random();
            for (int i = 0; i < sortArr.Length; i++)
            {
                sortArr[i] = rnd.Next(min, max);
            }
            mainPage.UpdateUI();
        }
        public void GetBigArray(IndexBase mainPage)
        {
            sortArr = bigArr;

            int min = 20;
            int max = 620;

            Random rnd = new Random();
            for (int i = 0; i < sortArr.Length; i++)
            {
                sortArr[i] = rnd.Next(min, max);
            }
            mainPage.UpdateUI();
        }
    }
}
