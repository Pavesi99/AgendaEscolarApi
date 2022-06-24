using Context;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace AgendaEscolarApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgendamentoController : ControllerBase
    {
        private readonly ILogger<AgendamentoController> _logger;
        private readonly AgendaEscolarDbContext _dbContext;

        public AgendamentoController(ILogger<AgendamentoController> logger, AgendaEscolarDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("buscar")]
        public IEnumerable<Agendamento> Get()
        {
            return _dbContext.Agendamentos.ToList();
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public bool Delete([FromRoute] int id)
        {
            var agendamento = _dbContext.Agendamentos.FirstOrDefault(x => x.Id == id);
            if(agendamento == null)
            {
                return false;
            }
            _dbContext.Agendamentos.Remove(agendamento);
            _dbContext.SaveChanges();
            return true;
        }

        [HttpPost]
        [Route("adicionar")]
        public Agendamento Add([FromBody] Agendamento agendamento)
        {
            _dbContext.Agendamentos.Add(agendamento);
            _dbContext.SaveChanges();
            return agendamento;
        }

        [HttpPut]
        [Route("editar")]
        public Agendamento Edit([FromBody] Agendamento agendamento)
        {
            var AgendamentoDb = _dbContext.Agendamentos.Update(agendamento);
            _dbContext.SaveChanges();
            return agendamento;
        }
    }
}