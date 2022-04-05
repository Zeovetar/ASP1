using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HW1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ValuesHolder _holder;
        public WeatherForecastController(ValuesHolder holder)
        {
            _holder = holder;
        }

        public string Get()
        {
            return "hello!";
        }


        [HttpPost("create")]
        public IActionResult Create([FromQuery] DateTime date, [FromQuery] int tempC, [FromQuery] string summary)
        {

            WeatherForecast _input = new WeatherForecast();
            _input.Date = date;
            _input.TemperatureC = tempC;
            _input.Summary = summary;
            _holder.Add(_input);
            return Ok();
        }
        [HttpGet("read")]
        public IActionResult Read([FromBody] DelData summary)
        {
            List<int> temperature = new();

            foreach (WeatherForecast iter in _holder.Values)
            {
                if (iter.Date >= summary.firstDate && iter.Date <= summary.secondDate)
                {
                    temperature.Add(iter.TemperatureC);
                }
            }

            return Ok(temperature);
        }
        [HttpPut("update")]
        public IActionResult Update([FromBody] AllData summary)
        {
            foreach (WeatherForecast iter in _holder.Values)
            {
                if (iter.Date == summary.dateToUpdate)
                {
                    iter.TemperatureC = summary.newTemperature;
                }
            }
            return Ok();
        }
        [HttpDelete("delete")]
        public IActionResult Delete([FromBody] DelData summary)
        {
            foreach (WeatherForecast iter in _holder.Values)
            {
                if (iter.Date >= summary.firstDate && iter.Date <= summary.secondDate)
                {
                    iter.TemperatureC = int.MaxValue;
                }
            }
            return Ok();
        }
    }
}

