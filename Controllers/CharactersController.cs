using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firefly.Data;
using Firefly.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AngularMix2017.Controllers
{
    [Route("api/[controller]")]
   public class CharactersController : Controller { 
       CharacterContext _context;
       public CharactersController (CharacterContext context) {
       _context = context;
       }
       // GET api/characters
       [HttpGet]
       public IActionResult Get () {
         return new ObjectResult(_context.Characters.ToList ());
       }
   
       // GET api/characters/5
       [HttpGet("{id}",Name="GetChar")]
       public async Task<IActionResult> Get (int id) {
         return new ObjectResult(await _context.Characters.FindAsync (id));
       }
   
       // POST api/characters
       [HttpPost]
       public async Task<IActionResult> Post ([FromBody] string name) {
         var character = new Character (name);
         _context.Characters.Add (character);
         await _context.SaveChangesAsync();
         return CreatedAtRoute ("GetChar", new { id = character.Id }, character);
       }
   
       // PUT api/characters/5
       [HttpPut ("{id}")]
       public async Task<IActionResult> Put (int id, [FromBody] string value) {
            var character = _context.Characters.Find(id);
            // update character from passed data here
            await _context.SaveChangesAsync();
            return new ObjectResult(character);
        }
   
       // DELETE api/characters/5
       [HttpDelete ("{id}")]
       public async Task<IActionResult> Delete (int id) {

         var character = _context.Characters.Find (id);
         _context.Characters.Remove (character);
         await _context.SaveChangesAsync ();

         return new NoContentResult();  // 204 Status code
       }
     }
}
