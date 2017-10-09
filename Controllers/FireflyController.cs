   using System;                            
   using System.Collections.Generic;
   using System.Linq;
   using System.Threading.Tasks;
   using Firefly.Data;
   using Firefly.Domain;
   using Microsoft.AspNetCore.Http;
   using Microsoft.AspNetCore.Mvc;
   
   namespace AngularMix2017.Controllers {
     [Route ("api/[controller]")]
     public class FireflyController : Controller {

    //**remember to add IoC to startup!!**/
       FireflyService _service;
       public FireflyController (FireflyService service) {
         _service = service;
       }
   
       [HttpGet]
       public IEnumerable<String[]> Get () {
         return _service.GetListOfCharacterNamesAndIds ();  
       }
   
       [HttpGet ("{id}", Name = "GetDetails")]
       public Character Get (int id) {
         return _service.GetFullCharacterDetails (id);
       }
   
       [HttpPost ("Create")]
       public string CreateCharacter (string name) {
         var character = _service.CreateNewCharacter (name);
         return character.Id.ToString ();
       }
   
       [HttpPost ("CreateWithEntrance")]
       public Character CreateCharacterWithEntrance (string characterName, int movieMinute, string scene, string description) {
         return _service.CreateNewCharacter (characterName, movieMinute, scene, description);
       }
   
    [HttpPost ("AddQuote")]
       public  int AddQuote (int characterId, string quoteText) {
         var result =  _service.AddQuoteToCharacter (characterId, quoteText);
         if (result == 1) { return StatusCodes.Status201Created; } 
         else { return StatusCodes.Status412PreconditionFailed; }
       }

       [HttpPost ("AddQuoteAsync")]
       public async Task<int> AddQuoteAsync (int characterId, string quoteText) {
         var result = await _service.AddQuoteToCharacterAsync (characterId, quoteText);
         if (result == 1) { return StatusCodes.Status201Created; } 
         else { return StatusCodes.Status412PreconditionFailed; }
       }

       [HttpPost ("AddEntrance")]
       public int AddEntrance (int characterId, int movieMinute, string scene, string description) {
         var result= _service.AddEntranceToCharacter (characterId, movieMinute, scene, description);
          if (result == 1) { return StatusCodes.Status201Created; } else { return StatusCodes.Status412PreconditionFailed; }
       }
       [HttpPost ("ReplaceEntrance")]
       public int ReplaceEntrance (int characterId, int movieMinute, string scene, string description) {
         var result= _service.ReplaceEntranceForCharacter(characterId, movieMinute, scene, description);
          if (result == 1) { return StatusCodes.Status201Created; } else { return StatusCodes.Status412PreconditionFailed; }
       }
     }
   }