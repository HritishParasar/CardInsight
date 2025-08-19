using CardInsight.API.DTOs;
using CardInsight.API.Models;
using CardInsight.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardInsight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        private readonly ICreditCard creditCardRepository;
        private readonly ILogger<CreditCardController> logger;

        public CreditCardController(ICreditCard creditCardRepository, ILogger<CreditCardController> logger)
        {
            this.creditCardRepository = creditCardRepository;
            this.logger = logger;
        }
        [HttpPost("addCC")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddCreditCard(CreditCardDTO cardDTO)
        {
            logger.LogInformation("AddCreditCard Method called!");
            var cc = new CreditCard
            {
                Name = cardDTO.Name,
                ApplyLink = cardDTO.ApplyLink,
                Category = cardDTO.Category,
                //CreatedAt = cardDTO.CreatedAt,
                Features = cardDTO.Features,
                ImageUrl = cardDTO.ImageUrl
            };
            try
            {
                await creditCardRepository.AddCreditCardAsync(cc);
                logger.LogInformation("Credit Card added!");
                return Ok("Successfully Added!");
            }
            catch (Exception ex)
            {
                logger.LogError(ex,ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCreditCard()
        {
            logger.LogInformation("GetAllCreditCard Method called!");
            try
            {
                var CCs = await creditCardRepository.GetAllCreditCards();
                logger.LogInformation($"Credit Card : {CCs}");
                return Ok(CCs.ToList());    
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getbySearchorCategory")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCreditCard(string? search , string? category)
        {
            logger.LogInformation("GetCreditCard Method called!");
            try
            {
                var cc = await creditCardRepository.GetCreditCard(search, category);
                logger.LogInformation($"GetCreditCard : {cc}");
                return Ok(cc);
            }catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("updateCC/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCreditCard(int id, CreditCardDTO creditCardDto)
        {
            logger.LogInformation("UpdateCreditCard Method called!");
            var cc = await creditCardRepository.GetCreditCardByID(id);
            if (cc is null)
                return NotFound("No CreditCard found with given Id.");
            cc.Name = creditCardDto.Name;
            cc.Features = creditCardDto.Features;
            cc.ApplyLink = creditCardDto.ApplyLink;
            cc.Category = creditCardDto.Category;  
            cc.ImageUrl = creditCardDto.ImageUrl;
            try
            {
                await creditCardRepository.UpdateCreditCardAsync(cc);
                logger.LogInformation("Credit Card updated!");
                return Ok("Successfully Updated!");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("deleteCC/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCreditCard(int id)
        {
            logger.LogInformation("DeleteCreditCard Method called!");
            try
            {
                await creditCardRepository.DeleteCreditCardAsync(id);
                logger.LogInformation("Credit Card deleted!");
                return Ok("Successfully Deleted!");
            }
            catch(Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
