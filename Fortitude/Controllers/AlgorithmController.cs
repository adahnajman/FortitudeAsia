using Fortitude.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fortitude.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlgorithmController : ControllerBase
    {
        private readonly AlgorithmServices _service;
        public AlgorithmController() 
        {
            _service = new AlgorithmServices();
        }
        #region Task 2
        [HttpGet("specialdigit")]
        public IActionResult specialdigit(string specialValue)
        {
            try
            {
                if (specialValue != null)
                {
                    int specialDigit = _service.CalculateSpecialDigit(specialValue);
                    return Ok(specialDigit);
                }
                else
                {
                    return BadRequest("Invalid value");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("specialDigitOccurrence")]
        public IActionResult specialDigitOccurrence()
        {
            try
            {
                string distribution = _service.CalculateSpecialDigitDistribution(201, 999);
                return Ok(distribution);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Task 3
        [HttpPost("OrderByAlgorithm")]
        public IActionResult OrderByAlgorithm(string input, int ordering)
        {
            try
            {
                if(input != null && ordering !=0)
                {
                    var result = orderByAlgorithm(input.ToCharArray(), ordering);
                    return Ok(new string(result));
                }
                else
                {
                    return BadRequest("Invalid value");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        public static char[] orderByAlgorithm(char[] input, int ordering)
        {
            List<char> NewInput = new List<char>();
            List<char> Remaining = new List<char>(input);
            int Index = 0;

            while (Remaining.Count > 0)
            {
                Index = (Index + ordering - 1) % Remaining.Count;
                NewInput.Add(Remaining[Index]);
                Remaining.RemoveAt(Index);

                if (NewInput.Count % 4 == 3 && Remaining.Count > 0)
                {
                    NewInput.Add('-'); //add - after 3rd char
                }
            }
            return NewInput.ToArray();
        }
        #endregion


    }
}
