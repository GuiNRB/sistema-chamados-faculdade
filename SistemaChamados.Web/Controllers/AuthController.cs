using Microsoft.AspNetCore.Mvc;
using SistemaChamados.Web.Models;
using System.Text;
using System.Text.Json;

namespace SistemaChamados.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IHttpClientFactory httpClientFactory, ILogger<AuthController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var httpClient = _httpClientFactory.CreateClient("ApiClient");
                
                var loginData = new
                {
                    Email = model.Email,
                    Senha = model.Senha
                };

                var json = JsonSerializer.Serialize(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("api/usuarios/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonSerializer.Deserialize<LoginResponseDto>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (loginResponse != null)
                    {
                        // Armazenar o token na sessão
                        HttpContext.Session.SetString("AuthToken", loginResponse.Token);
                        HttpContext.Session.SetInt32("TipoUsuario", loginResponse.TipoUsuario);

                        // Determinar a URL de redirecionamento baseada no tipo de usuário
                        string redirectUrl = loginResponse.TipoUsuario switch
                        {
                            3 => "/Home/AdminDashboard",      // Admin
                            2 => "/Home/TecnicoDashboard",    // Técnico
                            _ => "/Home/UserDashboard"        // Usuário comum (1 ou outros)
                        };

                        return Ok(new { success = true, redirectUrl = redirectUrl, tipoUsuario = loginResponse.TipoUsuario });
                    }
                }

                // Se chegou aqui, houve erro na autenticação
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogWarning("Erro na autenticação: {StatusCode} - {Content}", response.StatusCode, errorContent);
                
                return BadRequest(new { success = false, message = "E-mail ou senha incorretos." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante o processo de login");
                return StatusCode(500, new { success = false, message = "Erro interno do servidor." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var httpClient = _httpClientFactory.CreateClient("ApiClient");
                
                var registerData = new
                {
                    NomeCompleto = model.NomeCompleto,
                    Email = model.Email,
                    Senha = model.Senha
                };

                var json = JsonSerializer.Serialize(registerData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("api/usuarios/registrar", content);

                if (response.IsSuccessStatusCode)
                {
                    return Ok(new { success = true, message = "Conta criada com sucesso! Por favor, faça o login." });
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogWarning("Erro no registro: {StatusCode} - {Content}", response.StatusCode, errorContent);
                
                return BadRequest(new { success = false, message = "Erro ao criar conta. Verifique os dados informados." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante o processo de registro");
                return StatusCode(500, new { success = false, message = "Erro interno do servidor." });
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}