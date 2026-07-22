using DockerComposeApp.Data;
using DockerComposeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DockerComposeApp.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    // Tela Principal (Index) - Lista os registros
    public async Task<IActionResult> Index()
    {
        var produtos = await _context.Produtos.ToListAsync();
        return View(produtos);
    }

    // Ação para salvar um novo registro
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar(Produto produto)
    {
        if (ModelState.IsValid)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}