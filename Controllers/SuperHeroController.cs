using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>();


           
        private readonly DataContext dataContext;

        public SuperHeroController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await this.dataContext.SuperHeroes.ToListAsync()); //Accessing database via this dataContext property
        }

        [HttpGet("{id}")]
        public async Task <ActionResult<SuperHero>> Get(int id) //if we take parametar we must include it i hhtp body
        {
            var hero = await this.dataContext.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found");
            return Ok(hero);
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            this.dataContext.SuperHeroes.Add(hero);
            await this.dataContext.SaveChangesAsync();
            return Ok(await this.dataContext.SuperHeroes.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbHero = await this.dataContext.SuperHeroes.FindAsync(request.Id);
            if (dbHero == null)
                return BadRequest("Hero not found");
          
            dbHero.Name = request.Name;            //we override name of hero
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;
           
            await this.dataContext.SaveChangesAsync();
            return Ok(await this.dataContext.SuperHeroes.ToListAsync());
        }
        [HttpDelete]
       public async Task<ActionResult<List<SuperHero>>> Delete(int id) 
        {
            var dbHero = await this.dataContext.SuperHeroes.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Hero not found");
            
            this.dataContext.SuperHeroes.Remove(dbHero);
            await dataContext.SaveChangesAsync();
            return Ok(await this.dataContext.SuperHeroes.ToListAsync());
        }
    }
}
