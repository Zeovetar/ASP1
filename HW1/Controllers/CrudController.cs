using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HW1.Controllers
{
    [Route("api/crud")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly ValuesHolder _holder;
        public CrudController(ValuesHolder holder)
        {
            _holder = holder;
        }
        [HttpPost("create")]
        public IActionResult Create([FromQuery] string input)
        {
            _holder.Add(input);
            return Ok();
        }
        [HttpGet("read")]
        public IActionResult Read()
        {
            return Ok(_holder.Get());
        }
        [HttpPut("update")]
        public IActionResult Update([FromQuery] string stringsToUpdate,
        [FromQuery] string newValue)
        {
            for (int i = 0; i < _holder.Values.Count; i++)
            {
                if (_holder.Values[i] == stringsToUpdate)
                    _holder.Values[i] = newValue;
            }
            return Ok();
        }
        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] string stringsToDelete)
        {
            _holder.Values = _holder.Values.Where(w => w !=stringsToDelete).ToList();
            return Ok();
        }
    }
}
