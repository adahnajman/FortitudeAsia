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
        [HttpGet("special-digit/{value}")]
        public ActionResult<int> GetSpecialDigit(string value)
        {
            try
            {
                int specialDigit = _service.CalculateSpecialDigit(value);
                return Ok(specialDigit);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("specialDigitOccurrence")]
        public ActionResult<string> GetSpecialDigitDistribution()
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
        [HttpGet("OrderByAlgorithm")]
        public ActionResult<string> OrderByAlgorithm(string input, int ordering)
        {
            try
            {
                var result = orderByAlgorithm(input.ToCharArray(), ordering);
                return new string(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        public static char[] orderByAlgorithm(char[] input, int ordering)
        {
            List<char> result = new List<char>();
            List<char> inputList = new List<char>(input);
            int currentIndex = 0;

            while (inputList.Count > 0)
            {
                currentIndex = (currentIndex + ordering - 1) % inputList.Count;
                result.Add(inputList[currentIndex]);
                inputList.RemoveAt(currentIndex);

                // Add '-' after every 3rd character in the result
                if (result.Count % 4 == 3 && inputList.Count > 0)
                {
                    result.Add('-');
                }
            }
            return result.ToArray();
        }
        #endregion


    }
}
