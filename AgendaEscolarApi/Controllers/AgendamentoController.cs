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
            var Materia = _dbContext.Materias.FirstOrDefault(x => x.Id == id);
            if(Materia == null)
            {
                return false;
            }
            _dbContext.Materias.Remove(Materia);
            _dbContext.SaveChanges();
            return true;
        }

        [HttpPost]
        [Route("adicionar")]
        public Materia Add([FromBody] Materia Materia)
        {
            _dbContext.Materias.Add(Materia);
            _dbContext.SaveChanges();
            return Materia;
        }

        [HttpPut]
        [Route("editar")]
        public Materia Edit([FromBody] Materia Materia)
        {
            var MateriaDb = _dbContext.Materias.FirstOrDefault(x => x.Id == Materia.Id);
            MateriaDb = Materia;
            _dbContext.SaveChanges();
            return Materia;
        }
    }
}