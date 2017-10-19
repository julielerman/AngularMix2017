   using System.Collections.Generic;
   using System.Linq;
   using System.Threading.Tasks;
   using System;
   using Firefly.Data;
   using Firefly.Domain;
   using Microsoft.AspNetCore.Http;
   using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AngularMix2017.Controllers {
     [Route ("api/[controller]")]
     public class FireflyController : Controller {

       //**remember to add IoC to startup!!**/
       FireflyService _service;
       public FireflyController (FireflyService service) {
         _service = service;
       }

       [HttpGet]
       public IActionResult Get () {
         var characterList = new ObjectResult(_service.GetListOfCharacterNamesAndIds());
         return new ObjectResult(characterList);
       }

       [HttpGet ("{id}", Name = "GetDetails")]
       public IActionResult Get (int id) {
         var character= _service.GetFullCharacterDetails (id);
         return new ObjectResult(character);
        }

       [HttpPost ("Create")]
       public IActionResult CreateCharacter (string name) {
         return new ObjectResult(_service.CreateNewCharacter (name));
        
       }

       [HttpPost ("CreateWithEntrance")]
       public IActionResult CreateCharacterWithEntrance (string characterName, int movieMinute, string scene, string description) {
         return new ObjectResult(_service.CreateNewCharacter (characterName, movieMinute, scene, description));
       }

      [HttpPost ("CreateFromJson")]
       public IActionResult CreateCharacterFromJson2([FromBody]JObject characterJson){
         return new ObjectResult(_service.InsertCharacterGraph(characterJson));
         
       }

       [HttpPost ("AddQuote")]
       public IActionResult AddQuote (int characterId, string quoteText) {
         var result = _service.AddQuoteToCharacter (characterId, quoteText);
         if (result == 1) {
           return new ContentResult { Content = "Quote Added", StatusCode = StatusCodes.Status201Created };
         } else {
           return new ContentResult {
             Content = "Quote Not Added. Possible reason: Character no longer exists",
               StatusCode = StatusCodes.Status400BadRequest
           };
         }
       }

       [HttpPost ("AddQuoteAsync")]
       public async Task<IActionResult> AddQuoteAsync (int characterId, string quoteText) {
         var result = await _service.AddQuoteToCharacterAsync (characterId, quoteText);
            if (result == 1) { return new StatusCodeResult(201); } else { return new StatusCodeResult(400); }
       }

       [HttpPost ("AddEntrance")]
       public IActionResult AddEntrance (int characterId, int movieMinute, string scene, string description) {
           var result = _service.AddEntranceToCharacter (characterId, movieMinute, scene, description);
           if (result == 1) { return new StatusCodeResult(201); } else { return new StatusCodeResult(400); }
         }
         [HttpPost ("ReplaceEntrance")]
       public IActionResult ReplaceEntrance (int characterId, int movieMinute, string scene, string description) {
         var result = _service.ReplaceEntranceForCharacter (characterId, movieMinute, scene, description);
         if (result == 1) { return new StatusCodeResult(201); } else { return new StatusCodeResult(400); }
       }
     }
   }