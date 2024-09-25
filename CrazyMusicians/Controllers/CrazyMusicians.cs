using CrazyMusicians.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CrazyMusicians.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrazyMusiciansController : ControllerBase
    {
        // Static data list
        public static List<CrazyMusician> _crazyMusicians = new List<CrazyMusician>()
        {
            new CrazyMusician { Id = 1, Name = "Ahmet Çalgı", Profession = "Ünlü Çalgı Çalar", Funfeature = "Her zaman yanlış nota çalar, ama çok eğlenceli" },
            new CrazyMusician { Id = 2, Name = "Zeynep Melodi", Profession = "Popüler Melodi Yazarı", Funfeature = "Şarkıları yanlış anlaşılır ama çok popüler" },
            new CrazyMusician { Id = 3, Name = "Cemil Akor", Profession = "Çılgın Akorist", Funfeature = "Akorları sık değiştirir, ama şaşırtıcı derecede yetenekli" },
            new CrazyMusician { Id = 4, Name = "Fatma Nota", Profession = "Sürpriz Nota Üreticisi", Funfeature = "Nota üretirken sürekli sürprizler hazırlar" },
            new CrazyMusician { Id = 5, Name = "Hasan Ritim", Profession = "Ritim Canavarı", Funfeature = "Her ritmi kendi tarzında yapar, hiç uymaz ama komiktir" },
            new CrazyMusician { Id = 6, Name = "Elif Armoni", Profession = "Armoni Ustası", Funfeature = "Armonilerini bazen yanlış çalar, ama çok yaratıcıdır" },
            new CrazyMusician { Id = 7, Name = "Ali Perde", Profession = "Perde Uygulayıcı", Funfeature = "Her perdeyi farklı şekilde çalar, her zaman sürprizlidir" },
            new CrazyMusician { Id = 8, Name = "Ayşe Rezonans", Profession = "Rezonans Uzmanı", Funfeature = "Rezonans konusunda uzman, ama bazen çok gürültü çıkarır" },
            new CrazyMusician { Id = 9, Name = "Murat Ton", Profession = "Tonlama Meraklısı", Funfeature = "Tonlamalarındaki farklılıklar bazen komik, ama oldukça ilginç" },
            new CrazyMusician { Id = 10, Name = "Selin Akor", Profession = "Akor Sihirbazı", Funfeature = "Akorları değiştirdiğinde bazen sihirli bir hava yaratır" }
        };

        // GET all musicians
        [HttpGet]
        public IEnumerable<CrazyMusician> GetAll()
        {
            return _crazyMusicians;
        }

        // GET musician by ID
        [HttpGet("{id:int:min(1)}")]
        public ActionResult<CrazyMusician> GetMusician(int id)
        {
            var musician = _crazyMusicians.FirstOrDefault(x => x.Id == id);
            if (musician == null)
            {
                return NotFound($"Musician with ID {id} not found.");
            }
            return Ok(musician);
        }

        // GET musicians by search query
        [HttpGet("search")]
        public ActionResult<IEnumerable<CrazyMusician>> SearchMusicians([FromQuery] string name)
        {
            var results = _crazyMusicians.Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!results.Any())
            {
                return NotFound($"No musicians found with name containing '{name}'.");
            }
            return Ok(results);
        }

        // POST - Create a new musician
        [HttpPost]
        public ActionResult<CrazyMusician> Create([FromBody] CrazyMusician musician)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _crazyMusicians.Max(x => x.Id) + 1;
            musician.Id = id;
            _crazyMusicians.Add(musician);

            return CreatedAtAction(nameof(GetMusician), new { id = musician.Id }, musician);
        }

        // PATCH - Update profession and patch other fields
        [HttpPatch("reprofession/{id:int:min(1)}/{newProfession}/")]
        public IActionResult ReprofessionMusician(int id, string newProfession, [FromBody] JsonPatchDocument<CrazyMusician> patchDocument)
        {
            var musician = _crazyMusicians.FirstOrDefault(m => m.Id == id);
            if (musician == null)
            {
                return NotFound($"Musician with ID {id} not found.");
            }

            if (patchDocument == null)
            {
                return BadRequest("Patch document cannot be null.");
            }

            // Profession update
            musician.Profession = newProfession;

            // Apply patch document
            patchDocument.ApplyTo(musician, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }


        // PUT - Update entire musician
        [HttpPut("{id:int:min(1)}")]
        public IActionResult UpdateMusician(int id, [FromBody] CrazyMusician updatedMusician)
        {
            var musician = _crazyMusicians.FirstOrDefault(m => m.Id == id);
            if (musician == null)
            {
                return NotFound($"Musician with ID {id} not found.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            musician.Name = updatedMusician.Name;
            musician.Profession = updatedMusician.Profession;
            musician.Funfeature = updatedMusician.Funfeature;

            return NoContent();
        }

        // DELETE - Delete a musician
        [HttpDelete("{id:int:min(1)}")]
        public IActionResult DeleteMusician(int id)
        {
            var musician = _crazyMusicians.FirstOrDefault(m => m.Id == id);
            if (musician == null)
            {
                return NotFound($"Musician with ID {id} not found.");
            }

            _crazyMusicians.Remove(musician);
            return NoContent();
        }
    }
}
