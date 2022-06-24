using Context;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace AgendaEscolarApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MateriaController : ControllerBase
    {
        private readonly ILogger<MateriaController> _logger;
        private readonly AgendaEscolarDbContext _dbContext;

        public MateriaController(ILogger<MateriaController> logger, AgendaEscolarDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("buscar")]
        public IEnumerable<Materia> Get()
        {
            return _dbContext.Materias.ToList();
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public bool Delete([FromRoute] int id)
        {
            var materia = _dbContext.Materias.FirstOrDefault(x => x.Id == id);
            if(materia == null)
            {
                return false;
            }
            _dbContext.Materias.Remove(materia);
            _dbContext.SaveChanges();
            return true;
        }

        [HttpPost]
        [Route("adicionar")]
        public Materia Add([FromBody] Materia materia)
        {
            _dbContext.Materias.Add(materia);
            _dbContext.SaveChanges();
            return materia;
        }

        [HttpPut]
        [Route("editar")]
        public Materia Edit([FromBody] Materia materia)
        {
             _dbContext.Materias.Update(materia);
            _dbContext.SaveChanges();
            return materia;
        }
    }
}