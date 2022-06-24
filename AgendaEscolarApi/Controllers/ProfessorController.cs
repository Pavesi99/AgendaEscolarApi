using Context;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace AgendaEscolarApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly ILogger<ProfessorController> _logger;
        private readonly AgendaEscolarDbContext _dbContext;

        public ProfessorController(ILogger<ProfessorController> logger, AgendaEscolarDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("buscar")]
        public IEnumerable<Professor> Get()
        {
            return _dbContext.Professores.ToList();
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public bool Delete([FromRoute] int id)
        {
            var professor = _dbContext.Professores.FirstOrDefault(x => x.Id == id);
            if(professor == null)
            {
                return false;
            }
            _dbContext.Professores.Remove(professor);
            _dbContext.SaveChanges();
            return true;
        }

        [HttpPost]
        [Route("adicionar")]
        public Professor Add([FromBody] Professor professor)
        {
            _dbContext.Professores.Add(professor);
            _dbContext.SaveChanges();
            return professor;
        }

        [HttpPut]
        [Route("editar")]
        public Professor Edit([FromBody] Professor professor)
        {
            _dbContext.Professores.Update(professor);
            _dbContext.SaveChanges();
            return professor;
        }
    }
}