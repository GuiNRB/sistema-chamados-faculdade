using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaChamados.Web.Models;

namespace SistemaChamados.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Cadastro()
    {
        ViewData["Title"] = "Criar Conta";
        return View();
    }

    public IActionResult EsqueciSenha()
    {
        ViewData["Title"] = "Recuperar Senha";
        return View();
    }

    public IActionResult ResetarSenha()
    {
        ViewData["Title"] = "Definir Nova Senha";
        return View();
    }

    // --- DASHBOARDS ---
    public IActionResult UserDashboard()
    {
        ViewData["Title"] = "Meus Chamados";
        return View();
    }

    public IActionResult TecnicoDashboard()
    {
        ViewData["Title"] = "Painel do Técnico";
        return View();
    }

    public IActionResult AdminDashboard()
    {
        ViewData["Title"] = "Painel Administrativo";
        return View();
    }

    // --- PÁGINAS INTERNAS ---
    public IActionResult NovoTicket()
    {
        ViewData["Title"] = "Novo Chamado";
        return View();
    }

    public IActionResult TicketDetalhes()
    {
        ViewData["Title"] = "Detalhes do Chamado";
        return View();
    }

    public IActionResult TecnicoDetalhes()
    {
        ViewData["Title"] = "Detalhes do Chamado";
        return View();
    }

    public IActionResult AdminTickets()
    {
        ViewData["Title"] = "Gerenciar Chamados";
        return View();
    }

    public IActionResult AdminCadastrarTecnico()
    {
        ViewData["Title"] = "Cadastrar Técnico";
        return View();
    }

    public IActionResult Config()
    {
        ViewData["Title"] = "Configurações";
        return View();
    }

    public IActionResult TecnicoConfig()
    {
        ViewData["Title"] = "Configurações";
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
