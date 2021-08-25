using DevStore.Clients.API.Application.Commands;
using DevStore.Clients.API.Models;
using DevStore.WebAPI.Core.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DevStore.WebAPI.Core.User;

namespace DevStore.Clients.API.Controllers
{
    [Route("clients")]
    public class ClientsController : MainController
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMediator _mediator;
        private readonly IAspNetUser _user;

        public ClientsController(IClienteRepository clienteRepository, IMediator mediator, IAspNetUser user)
        {
            _clienteRepository = clienteRepository;
            _mediator = mediator;
            _user = user;
        }

        [HttpGet("address")]
        public async Task<IActionResult> ObterEndereco()
        {
            var endereco = await _clienteRepository.GetAddressById(_user.GetUserId());

            return endereco == null ? NotFound() : CustomResponse(endereco);
        }

        [HttpPost("address")]
        public async Task<IActionResult> AdicionarEndereco(AddAddressCommand endereco)
        {
            endereco.ClientId = _user.GetUserId();
            return CustomResponse(await _mediator.Send(endereco));
        }
    }
}