using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers;

[Route("api/tarefa")]
[ApiController]
public class TarefaController : ControllerBase
{
    private readonly AppDataContext _context;

    public TarefaController(AppDataContext context) =>
        _context = context;

    // GET: api/tarefa/listar
    [HttpGet]
    [Route("listar")]
    public IActionResult Listar()
    {
        try
        {
            List<Tarefa> tarefas = _context.Tarefas.Include(x => x.Categoria).ToList();
            return Ok(tarefas);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // POST: api/tarefa/cadastrar
    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar([FromBody] Tarefa tarefa)
    {
        try
        {
            Categoria? categoria = _context.Categorias.Find(tarefa.CategoriaId);
            if (categoria == null)
            {
                return NotFound();
            }
            tarefa.Categoria = categoria;
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
            return Created("", tarefa);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPatch]
    [Route("alterar")]
    public IActionResult Atualizar(int id, [FromBody] Tarefa tarefaAtualizada)
    {
        try
        {
            Tarefa tarefaExistente = _context.Tarefas.Find(id) ?? throw new InvalidOperationException($"Tarefa com id {id} não encontrado");
            if (tarefaExistente != null)
            {
                tarefaExistente.StatusId = tarefaAtualizada.StatusId + 1;
                _context.SaveChanges();
                return Ok(tarefaExistente);
            }
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // [HttpGet]
    // [Route("listar")]
    // public IActionResult ListarConcluidas()
    // {
    //     try
    //     {
    //         if (tarefaId > 1){
    //         return Ok(tarefasPositivas);
                
    //         }
            
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }
    
    // [HttpGet]
    // [Route("listar")]
    // public IActionResult ListarNãoConcluidas()
    // {
    //     try
    //     {
    //         if (tarefaId > 2){
    //         return Ok(tarefasNegativas);
                
    //         }
            
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }
}
