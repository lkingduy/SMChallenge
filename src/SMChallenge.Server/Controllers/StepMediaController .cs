using SMChallenge.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace SMChallenge.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StepMediaController : ControllerBase
    {

        public StepMediaController()
        {
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]StepMediaModel model)
        {
            string result = string.Empty;
            var numbers = model.Input.ToList();
            numbers.Sort();

            var middleNumbers = new List<int>();
            var beginNumbers = new List<int>();
            var endNumbers = new List<int>();
            var remainNumbers = new List<int>();
            var len = numbers.Count;
            var remainLen = len - 30;
            for (var i = 0; i < 10; i++)
            {
                middleNumbers.Add(numbers[len-i-1]);
                beginNumbers.Add(numbers[len-i-11]);
                endNumbers.Add(numbers[len-i-21]);
            }

            for(var i = 0; i < remainLen/2; i++)
            {
                remainNumbers.Add(numbers[i]);
            }
            remainNumbers.AddRange(middleNumbers);
            for (var i = remainLen/2; i < remainLen; i++)
            {
                remainNumbers.Add(numbers[i]);
            }

            var resArr = new List<int>();
            resArr.AddRange(beginNumbers);
            resArr.AddRange(remainNumbers);
            resArr.AddRange(endNumbers);
            result = String.Join(",", resArr);
            StepMediaModelResult res = new StepMediaModelResult()
            {
                Response = result
            };
            return Ok(res);
        }
    }
}
