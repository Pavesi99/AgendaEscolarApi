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
            var Agendamento = _dbContext.Agendamentos.FirstOrDefault(x => x.Id == id);
            if(Agendamento == null)
            {
                return false;
            }
            _dbContext.Agendamentos.Remove(Agendamento);
            _dbContext.SaveChanges();
            return true;
        }

        [HttpPost]
        [Route("adicionar")]
        public Agendamento Add([FromBody] Agendamento Agendamento)
        {
            _dbContext.Agendamentos.Add(Agendamento);
            _dbContext.SaveChanges();
            return Agendamento;
        }

        [HttpPut]
        [Route("editar")]
        public Agendamento Edit([FromBody] Agendamento Agendamento)
        {
            var AgendamentoDb = _dbContext.Agendamentos.FirstOrDefault(x => x.Id == Agendamento.Id);
            AgendamentoDb = Agendamento;
            _dbContext.SaveChanges();
            return Agendamento;
        }
    }
}