using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            // IMPLEMENTADO.
            var ObterPorId = _context.Tarefas.Find(id);
            if(ObterPorId == null)

                return NotFound();

            return Ok(ObterPorId);

        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            // IMPLEMENTADO.
            var obterTodos = _context.Tarefas.ToList( );

            if(obterTodos == null)
                return NotFound();

            return Ok(obterTodos);
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            // IMPLEMENTADO.
             var obterPorTitulo = _context.Tarefas.Where(x => x.Titulo.Contains(titulo));

            if(obterPorTitulo == null)
                return NotFound();

            return Ok(obterPorTitulo);            
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            // IMPLEMENTADO.
             var obterPorStatus = _context.Tarefas.Where(x => x.Status == status);
              
              if(obterPorStatus == null)
                return NotFound();
            
            return Ok(obterPorStatus);
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // IMPLEMENTADO.
            _context.Add(tarefa);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // IMPLEMENTADO.
            
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Status = tarefa.Status;
            tarefaBanco.Titulo = tarefa.Titulo;

            _context.Update(tarefaBanco);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            // IMPLEMENTADO.
            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();
            
            return NoContent();
        }
    }
}
