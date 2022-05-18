using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SMChallenge.Client.Services;
using SMChallenge.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SMChallenge.Client.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject] public IStepMediaService StepMediaService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public GetArray GetArray { get; set; }
        [Inject]
        public IJSRuntime jSRuntime { get; set; }

        public StepMediaModel Model = new StepMediaModel();
        public bool ShowError = false;
        public CancellationTokenSource cts;
        public bool isPressed = false;

        public bool isBubble = false;
        public bool isMerge = false;
        public bool isQuick = false;
        public bool isSmall = false;
        public bool isMedium = false;
        public bool isBig = false;
        public string mergeId = "";
        public string bubbleId = "";
        public string quickId = "";
        public string smallArrId = "";
        public string mediumArrId = "";
        public string bigArrId = "";
        public string barColor = "lawngreen";
        public string inputText = "";
        public string result { get; set; }

        protected override void OnInitialized()
        {
            CallSmallArray();
            GetInputText();
        }

        public void GetInputText()
        {
            StringBuilder sb = new StringBuilder();
            for(var i=0;i< GetArray.sortArr.Length;i++)
            {
                sb.Append(GetArray.sortArr[i] + (i == GetArray.sortArr.Length - 1 ? "" : ","));
            }
            inputText = sb.ToString();
            result = String.Empty;
        }

        public void CallUpdateArray()
        {
            if (isPressed == false)
                GetArray.GetRandomArray(this);
            GetInputText();
        }

        public void CallSmallArray()
        {
            isSmall = true;
            isMedium = false;
            isBig = false;
            if (isPressed == false)
                GetArray.GetSmallArray(this);
            GetInputText();
        }

        public void CallMediumArray()
        {
            isSmall = false;
            isMedium = true;
            isBig = false;
            if (isPressed == false)
                GetArray.GetMediumArray(this);
            GetInputText();
        }

        public void CallBigArray()
        {
            isSmall = false;
            isMedium = false;
            isBig = true;
            if (isPressed == false)
                GetArray.GetBigArray(this);
            GetInputText();
        }

        public async Task StartSort()
        {
            try
            {
                isPressed = true;
                cts = new CancellationTokenSource();
                await Quicksort(GetArray.sortArr, 0, GetArray.sortArr.Length - 1, this, cts.Token);
                var numbers = GetArray.sortArr.ToList();
                var middleNumbers = new List<int>();
                var beginNumbers = new List<int>();
                var endNumbers = new List<int>();
                var remainNumbers = new List<int>();
                var len = numbers.Count;
                var remainLen = len - 30;
                for (var i = 0; i < 10; i++)
                {
                    middleNumbers.Add(numbers[len - i - 1]);
                    beginNumbers.Add(numbers[len - i - 11]);
                    endNumbers.Add(numbers[len - i - 21]);
                }

                for (var i = 0; i < remainLen / 2; i++)
                {
                    remainNumbers.Add(numbers[i]);
                }
                remainNumbers.AddRange(middleNumbers);
                for (var i = remainLen / 2; i < remainLen; i++)
                {
                    remainNumbers.Add(numbers[i]);
                }
                var resArr = new List<int>();
                resArr.AddRange(beginNumbers);
                resArr.AddRange(remainNumbers);
                resArr.AddRange(endNumbers);
                GetArray.sortArr = resArr.ToArray();
                this.UpdateUI();
                await CallApi();
            }
            catch (Exception)
            {
                cts.Dispose();
            }
        }

        public async Task Quicksort(int[] numbers, int left, int right, IndexBase mainPage, CancellationToken ct)
        {
            int i = left;
            int j = right;

            var pivot = numbers[(left + right) / 2];

            while (i <= j)
            {
                while (numbers[i] < pivot)
                    i++;
                while (numbers[j] > pivot)
                    j--;

                if (ct.IsCancellationRequested)
                    ct.ThrowIfCancellationRequested();
                if (i <= j)
                {
                    var tmp = numbers[i];
                    numbers[i] = numbers[j];
                    numbers[j] = tmp;

                    i++;
                    j--;
                }
                mainPage.UpdateUI();
                await Task.Delay(20);
            }

            if (IsSorted(numbers))
                mainPage.isPressed = false;

            if (left < j)
                await Quicksort(numbers, left, j, mainPage, ct);
            if (i < right)
                await Quicksort(numbers, i, right, mainPage, ct);
        }

        bool IsSorted(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i - 1] > arr[i])
                {
                    return false;
                }
            }
            return true;
        }

        public void CancelSort()
        {
            if (isPressed == true)
                cts.Cancel();

            isPressed = false;
        }

        public void UpdateUI()
        {
            this.StateHasChanged();
        }

        public async Task CallApi()
        {
            Model.Input = isSmall ? GetArray.smallArr : isMedium ? GetArray.mediumArr : GetArray.bigArr;
            var res = await StepMediaService.Arrrange(Model);
            result = res.Response;
            StateHasChanged();
        }
    }
}
