﻿using System.Text.Json;
using System.Xml.Linq;
using garbageDetetionApi.Context;
using garbageDetetionApi.Helper;
using garbageDetetionApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace garbageDetetionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GarbageController(GarbageDbContext context, IConfiguration configuration) : ControllerBase
    {
        // GET: api/garbage
        [HttpGet]
        public async Task<ActionResult<Garbage>> GetAll()
        {
            // Check if the API key is valid
            var apiKey = Request.Headers["x-api-key"].ToString();
            if (ApiKeyHelper.CheckApiKeyType(apiKey, context) != ApiKeyType.Read)
            {
                return Unauthorized("Invalid API key or insufficient permissions.");
            }
            
            try
            {
                var garbages = await context.Garbages.ToListAsync();
                if (!garbages.Any())
                {
                    return NotFound("No garbage records found.");
                }
                return Ok(garbages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //  GET: api/garbage/time/{timeStamp}
        [HttpGet("time/{timeStamp}")]
        public async Task<ActionResult<Garbage>> GetByTimeToNow(DateTime timeStamp)
        {
            // Check if the API key is valid
            var apiKey = Request.Headers["x-api-key"].ToString();
            if (ApiKeyHelper.CheckApiKeyType(apiKey, context) != ApiKeyType.Read)
            {
                return Unauthorized("Invalid API key or insufficient permissions.");
            }
            
            try
            {
                var garbages = await context.Garbages
                    .Where(g => g.TimeStamp >= timeStamp)
                    .ToListAsync();

                if (!garbages.Any())
                {
                    return NotFound($"No garbage records found after {timeStamp}.");
                }
                return Ok(garbages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        //  GET: api/garbage/count/{amount}
        [HttpGet("count/{amount}")]
        public async Task<ActionResult<Garbage>> GetByAmount(int amount)
        {
            // Check if the API key is valid
            var apiKey = Request.Headers["x-api-key"].ToString();
            if (ApiKeyHelper.CheckApiKeyType(apiKey, context) != ApiKeyType.Read)
            {
                return Unauthorized("Invalid API key or insufficient permissions.");
            }
            
            try
            {
                if (amount <= 0)
                {
                    return BadRequest("Amount must be greater than zero.");
                }

                var garbages = await context.Garbages
                    .OrderByDescending(g => g.TimeStamp)
                    .Take(amount)
                    .ToListAsync();

                if (!garbages.Any())
                {
                    return NotFound($"No garbage records found for the last {amount} entries.");
                }
                return Ok(garbages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/garbage/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Garbage>> GetById(int id)
        {
            // Check if the API key is valid
            var apiKey = Request.Headers["x-api-key"].ToString();
            if (ApiKeyHelper.CheckApiKeyType(apiKey, context) != ApiKeyType.Read)
            {
                return Unauthorized("Invalid API key or insufficient permissions.");
            }
            
            try
            {
                var garbages = await context.Garbages
                    .Where(g => g.Id >= id)
                    .OrderBy(g => g.Id)
                    .ToListAsync();

                if (!garbages.Any())
                {
                    return NotFound($"No garbage records found with Id >= {id}.");
                }
                return Ok(garbages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/garbage
        [HttpPost]
        public async Task<IActionResult> PostGarbage(Garbage garbage)
        {
            // Check if the API key is valid
            var apiKey = Request.Headers["x-api-key"].ToString();
            if (ApiKeyHelper.CheckApiKeyType(apiKey, context) != ApiKeyType.Write)
            {
                return Unauthorized("Invalid API key or insufficient permissions.");
            }
            
            string apiUrl = configuration["apiUrl"];
            
            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Failed to retrieve weather data.");
                }

                var json = await response.Content.ReadAsStringAsync();
                var weatherResponse = JsonDocument.Parse(json).RootElement;

                garbage.DetectedObject = garbage.DetectedObject;
                garbage.ImageName = garbage.ImageName;
                garbage.ConfidenceScore = garbage.ConfidenceScore;
                garbage.CameraId = garbage.CameraId;
                garbage.Longitude = garbage.Longitude;
                garbage.Latitude = garbage.Latitude;
                garbage.Weather = weatherResponse.GetProperty("weather")[0].GetProperty("main").GetString();
                garbage.Temp = Convert.ToDecimal(weatherResponse.GetProperty("main").GetProperty("temp").GetDouble());
                garbage.Humidity = Convert.ToDecimal(weatherResponse.GetProperty("main").GetProperty("humidity").GetInt32());
                garbage.WindSpeed = Convert.ToDecimal(weatherResponse.GetProperty("wind").GetProperty("speed").GetDouble());
                garbage.TimeStamp = DateTime.UtcNow;

                context.Garbages.Add(garbage);
                await context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = garbage.Id }, garbage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/garbage/no2
        [HttpPost("no2")]
        public async Task<IActionResult> PostGarbageWithNO2(Garbage garbage)
        {
            // Check if the API key is valid
            var apiKey = Request.Headers["x-api-key"].ToString();
            if (ApiKeyHelper.CheckApiKeyType(apiKey, context) != ApiKeyType.Write)
            {
                return Unauthorized("Invalid API key or insufficient permissions.");
            }

            string apiUrl = configuration["apiUrl"];
            string no2ApiUrl = configuration["no2ApiUrl"];
            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(apiUrl);
                var responseNo2 = await httpClient.GetAsync(no2ApiUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Failed to retrieve weather data.");
                }
                if (!responseNo2.IsSuccessStatusCode)
                {
                    return StatusCode((int)responseNo2.StatusCode, "Failed to retrieve NO2 data.");
  
                }

                var json = await response.Content.ReadAsStringAsync();
                var weatherResponse = JsonDocument.Parse(json).RootElement;

                garbage.DetectedObject = garbage.DetectedObject;
                garbage.ImageName = garbage.ImageName;
                garbage.ConfidenceScore = garbage.ConfidenceScore;
                garbage.CameraId = garbage.CameraId;
                garbage.Longitude = garbage.Longitude;
                garbage.Latitude = garbage.Latitude;
                garbage.Weather = weatherResponse.GetProperty("weather")[0].GetProperty("main").GetString();
                garbage.Temp = Convert.ToDecimal(weatherResponse.GetProperty("main").GetProperty("temp").GetDouble());
                garbage.Humidity = Convert.ToDecimal(weatherResponse.GetProperty("main").GetProperty("humidity").GetInt32());
                garbage.WindSpeed = Convert.ToDecimal(weatherResponse.GetProperty("wind").GetProperty("speed").GetDouble());
                garbage.TimeStamp = DateTime.UtcNow;

                var no2Json = await responseNo2.Content.ReadAsStringAsync();
                var no2Response = JsonDocument.Parse(no2Json).RootElement;
                var dataArray = no2Response.GetProperty("data").EnumerateArray();
                decimal? no2Value = null;
                foreach (var item in dataArray)
                {
                    if (item.GetProperty("formula").GetString() == "NO2") 
                    {
                        no2Value = Convert.ToDecimal(item.GetProperty("value").GetDouble());
                        break;
                    }
                }
                garbage.NO2 = no2Value;

                context.Garbages.Add(garbage);
                await context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = garbage.Id }, garbage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
